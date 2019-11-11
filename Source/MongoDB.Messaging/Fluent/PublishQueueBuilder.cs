using System;
using System.Collections.Generic;
using MongoDB.Messaging.Configuration;

namespace MongoDB.Messaging.Fluent
{
    /// <summary>
    /// A fluent builder to help publish a message to a queue
    /// </summary>
    public class PublishQueueBuilder : QueueManagerBase
    {
        private readonly IList<Message> _messages;
        private IQueueContainer _queueContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishQueueBuilder" /> class.
        /// </summary>
        /// <param name="manager">The queue manager.</param>
        /// <param name="message">The message to update.</param>
        public PublishQueueBuilder(IQueueManager manager, IList<Message> messages) 
            : base(manager)
        {
            _messages = messages;
        }

        public PublishQueueBuilder(IQueueManager manager, Message message)
            : this(manager, new List<Message> { message})
        {
        }

        /// <summary>
        /// Gets the message being built.
        /// </summary>
        /// <value>
        /// The message being build.
        /// </value>
        public IList<Message> Messages => _messages;

        /// <summary>
        /// Gets the queue instance.
        /// </summary>
        /// <value>
        /// The queue instance.
        /// </value>
        public IQueueContainer Container => _queueContainer;

        /// <summary>
        /// Start building a message to a queue with the specified name.
        /// </summary>
        /// <param name="name">The name of the queue.</param>
        /// <returns>A fluent interface to build the queue message.</returns>
        public BatchBuilder Queue(string name)
        {
            // load queue, apply defaults to message
            _queueContainer = Manager.Load(name);

            return new BatchBuilder(_messages, _queueContainer);
        }
    }
}