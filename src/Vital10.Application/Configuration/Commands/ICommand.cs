using System;
using MediatR;

namespace Vital10.Application.Configuration.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}