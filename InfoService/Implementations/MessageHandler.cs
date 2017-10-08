﻿using InfoService.Interfaces;
using InfoService.Models;
using System.Collections.Generic;

namespace InfoService.Implementations
{
    public class MessageHandler : IMessageHandler
    {
        protected List<ServiceCommand> _commands = new List<ServiceCommand>();
        protected ISerializer _converter;

        public MessageHandler(ISerializer converter)
        {
            _converter = converter;
        }

        public void AddCommand(ServiceCommand command)
        {
            _commands.Add(command);
        }

        public string GetErrorMessage(string error)
        {
            var outputMessage = new OutputMessage
            {
                Result = false,
                Message = error ?? string.Empty
            };
            return _converter?.Serialize(outputMessage);
        }

        public virtual string ProcessMessage(string message)
        {
            var result = _converter?.Deserialize<InputMessage>(message);
            return ProcessMessage(result);
        }

        public virtual string ProcessMessage(InputMessage message)
        {
            if (message == null)
                return GetErrorMessage("Message is null");

            OutputMessage outputMessage = null;
            foreach (var command in _commands)
            {
                outputMessage = command?.Execute(message);
                if (outputMessage != null)
                    break;
            }

            if (outputMessage == null)
                return GetErrorMessage("Unknown command");

            return _converter?.Serialize(outputMessage);
        }
    }
}