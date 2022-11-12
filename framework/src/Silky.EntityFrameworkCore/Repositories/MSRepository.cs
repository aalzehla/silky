using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Silky.Core;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Extensions.DatabaseProvider;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Default master repository master slave repository
    /// </summary>
    public partial class MSRepository : MSRepository<MasterDbContextLocator>, IMSRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    public partial class MSRepository<TMasterDbContextLocator> : IMSRepository<TMasterDbContextLocator>
        where TMasterDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取main library仓储
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TMasterDbContextLocator>();
        }

        /// <summary>
        /// Dynamically fetch from the library（random）
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IPrivateReadableRepository<TEntity> Slave<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            // 判断数据库main library是否注册
            var isRegister =
                Penetrates.DbContextWithLocatorCached.TryGetValue(typeof(TMasterDbContextLocator),
                    out var dbContextType);
            if (!isRegister)
                throw new InvalidCastException($" The locator `{typeof(TMasterDbContextLocator).Name}` is not bind.");

            // 获取main library贴的特性
            var appDbContextAttribute = DbProvider.GetAppDbContextAttribute(dbContextType);

            // Get the list of slave libraries
            var slaveDbContextLocators = appDbContextAttribute.SlaveDbContextLocators;

            // If no slave locator is defined，then an exception is thrown
            if (slaveDbContextLocators == null || slaveDbContextLocators.Length == 0)
                throw new InvalidOperationException("Not found slave locators.");

            // If only one slave library is configured，return directly
            if (slaveDbContextLocators.Length == 1) return Slave<TEntity>(() => slaveDbContextLocators[0]);

            // 获取randomfrom the library索引
            var index = RandomNumberGenerator.GetInt32(0, slaveDbContextLocators.Length);

            // 返回randomfrom the library
            return Slave<TEntity>(() => slaveDbContextLocators[index]);
        }

        /// <summary>
        /// Dynamically fetch from the library（customize）
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IPrivateReadableRepository<TEntity> Slave<TEntity>(Func<Type> locatorHandle)
            where TEntity : class, IPrivateEntity, new()
        {
            if (locatorHandle == null) throw new ArgumentNullException(nameof(locatorHandle));

            // Get locator type
            var dbContextLocatorType = locatorHandle();
            if (!typeof(IDbContextLocator).IsAssignableFrom(dbContextLocatorType))
                throw new InvalidCastException(
                    $"{dbContextLocatorType.Name} is not assignable from {nameof(IDbContextLocator)}.");

            // Determine whether the slave library locator is bound
            var isRegister = Penetrates.DbContextWithLocatorCached.TryGetValue(dbContextLocatorType, out _);
            if (!isRegister)
                throw new InvalidCastException($" The slave locator `{dbContextLocatorType.Name}` is not bind.");

            // Resolve from library locator
            var repository =
                EngineContext.Current.Resolve(
                        typeof(IRepository<,>).MakeGenericType(typeof(TEntity), dbContextLocatorType)) as
                    IPrivateRepository<TEntity>;

            // Return to the repository from the library
            return repository.Constraint<IPrivateReadableRepository<TEntity>>();
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    public partial class
        MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1> : IMSRepository<TMasterDbContextLocator,
            TSlaveDbContextLocator1>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取main library仓储
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TMasterDbContextLocator>();
        }

        /// <summary>
        /// 获取from the library仓储
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator1> Slave1<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator1>()
                .Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator1>>();
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
            , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取from the library仓储2
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator2> Slave2<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator2>()
                .Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator2>>();
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
            , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取from the library仓储3
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator3> Slave3<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator3>()
                .Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator3>>();
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
                TSlaveDbContextLocator3>
            , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取from the library仓储4
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator4> Slave4<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator4>()
                .Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator4>>();
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">from the library</typeparam>
    public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
                TSlaveDbContextLocator3, TSlaveDbContextLocator4>
            , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取from the library仓储5
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator5> Slave5<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator5>()
                .Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator5>>();
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator6">from the library</typeparam>
    public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
                TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
            , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
        where TSlaveDbContextLocator6 : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取from the library仓储6
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator6> Slave6<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator6>()
                .Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator6>>();
        }
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator6">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator7">from the library</typeparam>
    public partial class MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6,
            TSlaveDbContextLocator7>
        : MSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
                TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
            , IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2, TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6, TSlaveDbContextLocator7>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
        where TSlaveDbContextLocator6 : class, IDbContextLocator
        where TSlaveDbContextLocator7 : class, IDbContextLocator
    {
        /// <summary>
        /// non-generic warehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">non-generic warehousing</param>
        public MSRepository(IRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取from the library仓储7
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        public virtual IReadableRepository<TEntity, TSlaveDbContextLocator7> Slave7<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TEntity, TSlaveDbContextLocator7>()
                .Constraint<IReadableRepository<TEntity, TSlaveDbContextLocator7>>();
        }
    }
}