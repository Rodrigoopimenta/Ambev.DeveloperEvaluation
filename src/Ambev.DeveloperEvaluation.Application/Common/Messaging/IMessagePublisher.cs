﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(string queueName, T message);
    }
}
