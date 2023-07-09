﻿namespace Shared.Entities
{


    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {
        }

        public List<string> Messages { get; set; } = new List<string>();

        public bool Succeeded { get; set; }

        public static CommandResult Fail()
        {
            return new CommandResult { Succeeded = false };
        }

        public static CommandResult Fail(string message)
        {
            return new CommandResult { Succeeded = false, Messages = new List<string> { message } };
        }

        public static CommandResult Fail(List<string> messages)
        {
            return new CommandResult { Succeeded = false, Messages = messages };
        }

        public static Task<CommandResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<CommandResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<CommandResult> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static CommandResult Success()
        {
            return new CommandResult { Succeeded = true };
        }

        public static CommandResult Success(string message)
        {
            return new CommandResult { Succeeded = true, Messages = new List<string> { message } };
        }

        public static Task<CommandResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<CommandResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class CommandResult<T> : CommandResult, ICommandResult<T>
    {
        public CommandResult()
        {
        }


        public T Data { get; set; }

        public new static CommandResult<T> Fail()
        {
            return new CommandResult<T> { Succeeded = false };
        }

        public new static CommandResult<T> Fail(string message)
        {
            return new CommandResult<T> { Succeeded = false, Messages = new List<string> { message } };
        }
        public new static CommandResult<T> Fail(T data)
        {
            return new CommandResult<T> { Succeeded = false, Data = data };
        }
        public new static CommandResult<T> Fail(T data, string message)
        {
            return new CommandResult<T> { Data = data, Succeeded = false, Messages = new List<string> { message } };
        }
        public new static CommandResult<T> Fail(List<string> messages)
        {
            return new CommandResult<T> { Succeeded = false, Messages = messages };
        }
        public new static CommandResult<T> Fail(T data, List<string> messages)
        {
            return new CommandResult<T> { Data = data, Succeeded = false, Messages = messages };
        }

        public new static Task<CommandResult<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public new static Task<CommandResult<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public new static Task<CommandResult<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }
        public new static Task<CommandResult<T>> FailAsync(T data, List<string> messages)
        {
            return Task.FromResult(Fail(data, messages));
        }
        public new static Task<CommandResult<T>> FailAsync(T data, string message)
        {
            return Task.FromResult(Fail(data, message));
        }

        public new static CommandResult<T> Success()
        {
            return new CommandResult<T> { Succeeded = true };
        }

        public new static CommandResult<T> Success(string message)
        {
            return new CommandResult<T> { Succeeded = true, Messages = new List<string> { message } };
        }

        public static CommandResult<T> Success(T data)
        {
            return new CommandResult<T> { Succeeded = true, Data = data };
        }

        public static CommandResult<T> Success(T data, string message)
        {
            return new CommandResult<T> { Succeeded = true, Data = data, Messages = new List<string> { message } };
        }

        public static CommandResult<T> Success(T data, List<string> messages)
        {
            return new CommandResult<T> { Succeeded = true, Data = data, Messages = messages };
        }

        public new static Task<CommandResult<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public new static Task<CommandResult<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<CommandResult<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<CommandResult<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
    }
}
