using System;

namespace SW.Framework.Eventing
{
    /// <summary>
    ///     Defines the interface for a fluent class structure to configure system message subscriptions.
    /// </summary>
    public interface ISystemMessageSubscriptionConfiguration<TSystemMessage>
        where TSystemMessage : class
    {
        /// <summary>
        ///     Create an immediate retry policy with the specified number of retries, with no delay between attempts.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <returns>The current instance of <see cref="ISystemMessageSubscriptionConfiguration{TSystemEvent}" />.</returns>
        ISystemMessageSubscriptionConfiguration<TSystemMessage> RetryImmediate(int retryCount);

        /// <summary>
        ///     Create an interval retry policy with the specified number of retries at a fixed interval.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="interval">The interval between each retry attempt.</param>
        /// <returns>The current instance of <see cref="ISystemMessageSubscriptionConfiguration{TSystemEvent}" />.</returns>
        ISystemMessageSubscriptionConfiguration<TSystemMessage> RetryInterval(int retryCount, TimeSpan interval);

        /// <summary>
        ///     Create an incremental retry policy with the specified number of retry attempts
        ///     with an incrementing interval between retries.
        /// </summary>
        /// <param name="retryLimit">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial retry interval.</param>
        /// <param name="intervalIncrement">The interval to add to the retry interval with each subsequent retry.</param>
        /// <returns>The current instance of <see cref="ISystemMessageSubscriptionConfiguration{TSystemEvent}" />.</returns>
        ISystemMessageSubscriptionConfiguration<TSystemMessage> RetryIncremental(int retryLimit,
            TimeSpan initialInterval, TimeSpan intervalIncrement);

        /// <summary>
        ///     Create an exponential retry policy with the specified number of retries at exponential interval.
        /// </summary>
        /// <param name="retryLimit">The number of retry attempts.</param>
        /// <param name="minInterval">The minimum interval.</param>
        /// <param name="maxInterval">The maximum interval.</param>
        /// <param name="intervalDelta">The interval delta.</param>
        /// <returns>The current instance of <see cref="ISystemMessageSubscriptionConfiguration{TSystemEvent}" />.</returns>
        ISystemMessageSubscriptionConfiguration<TSystemMessage> RetryExponential(int retryLimit, TimeSpan minInterval,
            TimeSpan maxInterval, TimeSpan intervalDelta);

        /// <summary>
        ///     Puts a circuit breaker in the pipe, which can automatically prevent the flow
        ///     of messages to the consumer when the circuit breaker is opened.
        /// </summary>
        /// <param name="activeThreshold">
        ///     The number of attempts that must occur before the circuit breaker becomes active. Until
        ///     the breaker activates, it will not open on failure
        /// </param>
        /// <param name="tripThreshold">
        ///     The percentage of attempts that must fail before the circuit breaker trips into an open
        ///     state.
        /// </param>
        /// <param name="trackingPeriod">The period after which the attempt/failure counts are reset.</param>
        /// <returns>The current instance of <see cref="ISystemMessageSubscriptionConfiguration{TSystemMessage}" />.</returns>
        ISystemMessageSubscriptionConfiguration<TSystemMessage> WithCircuitBreaker(int activeThreshold,
            int tripThreshold, TimeSpan trackingPeriod);

        /// <summary>
        ///     Specify a rate limit for message processing, so that only the specified number
        ///     of messages are allowed per interval.
        /// </summary>
        /// <param name="rateLimit">The number of messages allowed per interval</param>
        /// <param name="interval">The reset interval for each set of messages</param>
        /// <returns>The current instance of <see cref="ISystemMessageSubscriptionConfiguration{TSystemMessage}" />.</returns>
        ISystemMessageSubscriptionConfiguration<TSystemMessage> WithRateLimit(int rateLimit, TimeSpan interval);

        /// <summary>
        ///     Specify a concurrency limit for tasks executing through the filter. No more than
        ///     the specified number of tasks will be allowed to execute concurrently.
        /// </summary>
        /// <param name="concurrencyLimit">The concurrency limit for the subsequent filters in the pipeline</param>
        /// <returns>The current instance of <see cref="ISystemMessageSubscriptionConfiguration{TSystemMessage}" />.</returns>
        ISystemMessageSubscriptionConfiguration<TSystemMessage> WithConcurrencyLimit(int concurrencyLimit);
    }
}