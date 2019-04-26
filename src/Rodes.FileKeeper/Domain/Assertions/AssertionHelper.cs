using System;
using System.Collections.Generic;
using System.Linq;

namespace Rodes.FileKeeper.Domain.Assertions
{
    public class AssertionHelper
    {
        public static void AssertNotNull(object parameterValue, string parameterName, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue == null)
            {
                var exception = new ArgumentNullException(parameterName, string.Format(exceptionMessage, exceptionMessageArgs));

                throw exception;
            }
        }

        public static void AssertNotNull(object parameterValue, string parameterName)
        {
            if (parameterValue == null)
            {
                var exception = new ArgumentNullException(parameterName);

                throw exception;
            }
        }

        public static void AssertNotWhiteSpace(string parameterValue, string parameterName, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue.Trim() == "")
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertNotEmpty(Guid parameterValue, string parameterName, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue == Guid.Empty)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertNotEmpty<T>(IEnumerable<T> parameterValue, string parameterName, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue.Count() == 0)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertMinLength(string parameterValue, int minLength, string parameterName, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue != null && parameterValue.Trim().Length < minLength)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertMaxLength(string parameterValue, string parameterName, int maxLength, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue != null && parameterValue.Length > maxLength)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertIsGreaterThan(double parameterValue, string parameterName, double minimumValue, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue <= minimumValue)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertIsGreaterOrEqualThan(double parameterValue, string parameterName, double minimumValue, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue < minimumValue)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertIsLowerOrEqualThan(double parameterValue, string parameterName, double maximumValue, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (parameterValue > maximumValue)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertIsInRange(double parameterValue, string parameterName, double minimumValue, double maximumValue, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            double minValue = minimumValue;
            double maxValue = maximumValue;

            if (maximumValue < minimumValue)
            {
                minValue = maximumValue;
                maxValue = minimumValue;
            }

            if (parameterValue < minValue || parameterValue > maxValue)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertIsValidGuidIdentity(string parameterValue, string parameterName, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (!Guid.TryParse(parameterValue, out Guid result) || result == Guid.Empty)
            {
                var exception = new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);

                throw exception;
            }
        }

        public static void AssertIsInEnum(int parameterValue, string parameterName, Type enumType, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (!Enum.IsDefined(enumType, parameterValue))
            {
                throw new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);
            }
        }

        public static void AssertIsInEnum<T>(object parameterValue, string parameterName, string exceptionMessage, params string[] exceptionMessageArgs)
        {
            if (!Enum.IsDefined(typeof(T), parameterValue))
            {
                throw new ArgumentException(string.Format(exceptionMessage, exceptionMessageArgs), parameterName);
            }
        }
    }
}
