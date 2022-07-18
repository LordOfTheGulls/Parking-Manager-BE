using PM_Common.DTO;
using PM_CQRS.Commands;
using PM_CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Dispatcher
{
    public interface IDispatcher
    {
        public TResult Dispatch<TResult>(IQuery<TResult> query);
        public OperationResult Dispatch(ICommand command);
    }
}
