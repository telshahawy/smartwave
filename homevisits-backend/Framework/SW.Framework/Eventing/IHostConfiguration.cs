using System;

namespace SW.Framework.Eventing
{
    /// <summary>
    ///     Defines the interface for a fluent class structure to configure a messaging host.
    /// </summary>
    public interface IHostConfiguration
    {
        /// <summary>
        ///     Configures the subscription for a particular event type and queue.
        /// </summary>
        /// <typeparam name="TSystemMessage">The type of the system message.</typeparam>
        /// <param name="queueName">Name of the receiver queue.</param>
        /// <returns>The current instance of <see cref="IHostConfiguration" />.</returns>
        IHostConfiguration Subscribe<TSystemMessage>(string queueName)
            where TSystemMessage : class;

        /// <summary>
        ///     Configures the subscription for a particular event type and queue.
        /// </summary>
        /// <typeparam name="TSystemMessage">The type of the system message.</typeparam>
        /// <param name="queueName">Name of the receiver queue.</param>
        /// <param name="subscriptionConfigurator">An action to configure the subscription.</param>
        /// <returns>The current instance of <see cref="IHostConfiguration" />.</returns>
        IHostConfiguration Subscribe<TSystemMessage>(string queueName,
            Action<ISystemMessageSubscriptionConfiguration<TSystemMessage>> subscriptionConfigurator)
            where TSystemMessage : class;

        /// <summary>
        ///     Configures the subscription for a particular request response.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <param name="queueName">Name of the receiver queue.</param>
        /// <returns>The current instance of <see cref="IHostConfiguration" />.</returns>
        IHostConfiguration Subscribe<TRequest, TResponse>(string queueName)
            where TRequest : class
            where TResponse : class;

        /// <summary>
        ///     Configures the subscription for a particular request response.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <param name="queueName">Name of the receiver queue.</param>
        /// <param name="subscriptionConfigurator">An action to configure the subscription.</param>
        /// <returns>The current instance of <see cref="IHostConfiguration" />.</returns>
        IHostConfiguration Subscribe<TRequest, TResponse>(string queueName,
            Action<IRequestResponseSubscriptionConfiguration<TRequest, TResponse>> subscriptionConfigurator)
            where TRequest : class
            where TResponse : class;
    }
}