using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Silky.Core.Extensions;
using Silky.Core.Extensions.Collections.Generic;

namespace Silky.Core
{
    [DebuggerStepThrough]
    public static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(
            T value,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(
            T value,
            [InvokerParameterName] [NotNull] string parameterName,
            string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNull(
            string value,
            [InvokerParameterName] [NotNull] string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (value == null)
            {
                throw new ArgumentException($"{parameterName} Empty is not allowed!", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or less than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} Length must be equal to or greater than {minLength}!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNullOrWhiteSpace(
            string value,
            [InvokerParameterName] [NotNull] string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{parameterName} Empty is not allowed或是空字符串", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or less than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} Length must be equal to or greater than {minLength}!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNullOrEmpty(
            string value,
            [InvokerParameterName] [NotNull] string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException($"{parameterName} Empty is not allowed!", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or less than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} Length must be equal to or greater than {minLength}!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(parameterName + " Empty is not allowed!", parameterName);
            }

            return value;
        }

        public static string Length(
            [CanBeNull] string value,
            [InvokerParameterName] [NotNull] string parameterName,
            int maxLength,
            int minLength = 0)
        {
            if (minLength > 0)
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
                }

                if (value.Length < minLength)
                {
                    throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!",
                        parameterName);
                }
            }

            if (value != null && value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!",
                    parameterName);
            }

            return value;
        }
    }
}