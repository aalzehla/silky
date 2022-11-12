using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    /// deal with .Skip().Take() expression problem
    /// </summary>
    internal class SqlServer2008OffsetToRowNumberConvertVisitor : ExpressionVisitor
    {
        /// <summary>
        /// Filter column accessors
        /// </summary>
        private static readonly Func<SelectExpression, SqlExpression, string, ColumnExpression>
            GenerateOuterColumnAccessor;

        /// <summary>
        /// expression root node
        /// </summary>
        private readonly Expression root;

        /// <summary>
        /// Sql expression factory
        /// </summary>
        private readonly ISqlExpressionFactory sqlExpressionFactory;

        /// <summary>
        /// static constructor
        /// </summary>
        static SqlServer2008OffsetToRowNumberConvertVisitor()
        {
            var method = typeof(SelectExpression).GetMethod("GenerateOuterColumn",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null,
                new Type[] { typeof(SqlExpression), typeof(string) }, null);

            if (method?.ReturnType != typeof(ColumnExpression))
                throw new InvalidOperationException("SelectExpression.GenerateOuterColumn() is not found.");

            GenerateOuterColumnAccessor =
                (Func<SelectExpression, SqlExpression, string, ColumnExpression>)method.CreateDelegate(
                    typeof(Func<SelectExpression, SqlExpression, string, ColumnExpression>));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sqlExpressionFactory"></param>
        public SqlServer2008OffsetToRowNumberConvertVisitor(Expression root, ISqlExpressionFactory sqlExpressionFactory)
        {
            this.root = root;
            this.sqlExpressionFactory = sqlExpressionFactory;
        }

        /// <summary>
        /// replace expression
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitExtension(Expression node)
        {
            if (node is ShapedQueryExpression shapedQueryExpression)
            {
                return shapedQueryExpression.Update(Visit(shapedQueryExpression.QueryExpression),
                    shapedQueryExpression.ShaperExpression);
            }

            if (node is SelectExpression se)
                node = VisitSelect(se);
            return base.VisitExtension(node);
        }

        /// <summary>
        /// renew Select statement
        /// </summary>
        /// <param name="selectExpression"></param>
        /// <returns></returns>
        private Expression VisitSelect(SelectExpression selectExpression)
        {
            var oldOffset = selectExpression.Offset;
            if (oldOffset == null)
                return selectExpression;

            var oldLimit = selectExpression.Limit;
            var oldOrderings = selectExpression.Orderings;

            // in subquery OrderBy must write Top quantity
            var newOrderings = oldOrderings.Count > 0 && (oldLimit != null || selectExpression == root)
                ? oldOrderings.ToList()
                : new List<OrderingExpression>();

            // renew表达式
            selectExpression = selectExpression.Update(selectExpression.Projection.ToList(),
                selectExpression.Tables.ToList(),
                selectExpression.Predicate,
                selectExpression.GroupBy.ToList(),
                selectExpression.Having,
                orderings: newOrderings,
                limit: null,
                offset: null);
            var rowOrderings = oldOrderings.Count != 0
                ? oldOrderings
                : new[] { new OrderingExpression(new SqlFragmentExpression("(SELECT 1)"), true) };

#if NET5_0
            _ = selectExpression.PushdownIntoSubquery(); // .NET 6 This method has no return value
#else
            selectExpression.PushdownIntoSubquery();
#endif

            var subQuery = (SelectExpression)selectExpression.Tables[0];
            var projection = new RowNumberExpression(Array.Empty<SqlExpression>(), rowOrderings, oldOffset.TypeMapping);
            var left = GenerateOuterColumnAccessor(subQuery, projection, "row");

            selectExpression.ApplyPredicate(sqlExpressionFactory.GreaterThan(left, oldOffset));

            if (oldLimit != null)
            {
                if (oldOrderings.Count == 0)
                {
                    selectExpression.ApplyPredicate(
                        sqlExpressionFactory.LessThanOrEqual(left, sqlExpressionFactory.Add(oldOffset, oldLimit)));
                }
                else
                {
                    // Subqueries are not supported here OrderBy operate
                    selectExpression.ApplyLimit(oldLimit);
                }
            }

            return selectExpression;
        }
    }
}