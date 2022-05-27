using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SeleniumCore.Helpers.Utilities
{
    public static class Wait
    {
        public static void Until(Func<bool> condition, int timeoutInSeconds = 10, int retryRateDelay = 100, string exceptionMessage = "Timeout exceeded.", bool shouldThrowException = true)
        {
            var start = DateTime.Now;

            while (!condition())
            {
                var now = DateTime.Now;
                var totalSeconds = (now - start).TotalSeconds;

                if ((totalSeconds >= timeoutInSeconds))
                {
                    if (shouldThrowException)
                    {
                        throw new TimeoutException(exceptionMessage);
                    }

                    break;
                }

                Thread.Sleep(retryRateDelay);
            }
        }

        public static void Until(List<Func<bool>> conditions, int timeoutInSeconds = 10, int retryRateDelay = 100, string exceptionMessage = "Timeout exceeded.", bool shouldThrowException = true)
        {
            var start = DateTime.Now;

            while (!conditions.All(x => !x.Invoke()))
            {
                var now = DateTime.Now;
                var totalSeconds = (now - start).TotalSeconds;

                if ((totalSeconds >= timeoutInSeconds))
                {
                    if (shouldThrowException)
                    {
                        throw new TimeoutException(exceptionMessage);
                    }

                    break;
                }

                Thread.Sleep(retryRateDelay);
            }
        }

        public static void Until(Action action, Func<bool> condition, int timeoutInSeconds = 10, int retryRateDelay = 100, string exceptionMessage = "Timeout exceeded.", bool shouldThrowException = true)
        {
            var start = DateTime.Now;

            do
            {
                action();

                var now = DateTime.Now;
                var totalSeconds = (now - start).TotalSeconds;

                if ((totalSeconds >= timeoutInSeconds))
                {
                    if (shouldThrowException)
                    {
                        throw new TimeoutException(exceptionMessage);
                    }

                    break;
                }

                Thread.Sleep(retryRateDelay);

            } while (!condition());
        }

        public static void Until<T>(Func<T, bool> condition, T expectedIntParam, int timeoutInSeconds = 10, int retryRateDelay = 50, bool shouldThrowException = true)
        {
            var start = DateTime.Now;
            while (!condition(expectedIntParam))
            {
                var now = DateTime.Now;
                var totalSeconds = (now - start).TotalSeconds;

                if (totalSeconds >= timeoutInSeconds)
                {
                    if (shouldThrowException)
                    {
                        throw new TimeoutException("Timeout exceeded.");
                    }

                    break;
                }

                Thread.Sleep(retryRateDelay);
            }
        }

        public static void Until(Func<int, Guid, bool> condition, int expectedIntParam, Guid expectedStrParam, int timeoutInSeconds = 10, bool shouldThrowException = true)
        {
            var start = DateTime.Now;
            while (!condition(expectedIntParam, expectedStrParam))
            {
                var now = DateTime.Now;
                var totalSeconds = (now - start).TotalSeconds;

                if (totalSeconds >= timeoutInSeconds)
                {
                    if (shouldThrowException)
                    {
                        throw new TimeoutException("Timeout exceeded.");
                    }

                    break;
                }

                Thread.Sleep(50);
            }
        }

        public static void Until(Func<bool> condition, TimeSpan timeout, TimeSpan retryRateDelay, string exceptionMessage, bool shouldThrowException = true)
        {
            var start = DateTime.Now;

            while (!condition())
            {
                var now = DateTime.Now;
                var elapsedTime = now - start;

                if (elapsedTime >= timeout)
                {
                    if (shouldThrowException)
                    {
                        throw new TimeoutException(exceptionMessage);
                    }

                    break;
                }

                Thread.Sleep(retryRateDelay);
            }
        }

        public static void ForConditionUntilTimeout(Func<bool> condition, int totalRunTimeoutMilliseconds = 5000, Action onTimeout = null, int sleepTimeMilliseconds = 2000)
        {
            var startTime = DateTime.UtcNow;
            var timeout = startTime + TimeSpan.FromMilliseconds(totalRunTimeoutMilliseconds);

            while (true)
            {
                var conditionFinished = condition();

                if (conditionFinished)
                {
                    break;
                }

                if (DateTime.UtcNow >= timeout)
                {
                    onTimeout?.Invoke();

                    break;
                }

                Thread.Sleep(sleepTimeMilliseconds);
            }
        }
    }
}
