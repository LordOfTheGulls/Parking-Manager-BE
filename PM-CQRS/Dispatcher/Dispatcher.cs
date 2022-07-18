using PM_Common.DTO;
using PM_CQRS.Commands;
using PM_CQRS.Queries;
using System.Reflection;

namespace PM_CQRS.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private object GetService(Type serviceType)
        {
            return serviceProvider.GetService(serviceType);
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            Type queryType = query.GetType();

            // used for when OperationResult<object> was used
            Type operationResultTrueReturnType = typeof(TResult);

            if (operationResultTrueReturnType == typeof(object))
            {
                operationResultTrueReturnType = queryType.GetInterface(typeof(IQuery<>).Name).GenericTypeArguments[0];
            }

            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), operationResultTrueReturnType);

            return ExecuteHandler<TResult>(handlerType, query, queryType);
        }

        public OperationResult Dispatch(ICommand command)
        {
            Type commandType = command.GetType();

            return null;
           /* var baseTypeAttribute = (CommandBaseTypeAttribute)commandType.GetCustomAttributes(typeof(CommandBaseTypeAttribute), false).FirstOrDefault();

            if (baseTypeAttribute != null)
                commandType = baseTypeAttribute.BaseType;

            try
            {
                Type handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
                return ExecuteHandler<OperationResult>(handlerType, command, commandType);
            }
            catch (InvalidOperationException ex)
            {
                return new OperationResult(OperationResultStatus.Failure, ex.Message);
            }*/
        }

        private void ExecuteHandler(Type handlerType, object argument, Type argumentType)
        {
            object handler = GetService(handlerType);

            if (handler == null)
                throw new Exception("Handler not registered for type " + argumentType.Name);

            try
            {
                MethodInfo handleMethod = handlerType.GetMethod("HandleAsync", new[] { argumentType });

                handleMethod.Invoke(handler, new[] { argument });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
        }

        private TReturnValue ExecuteHandler<TReturnValue>(Type handlerType, object argument, Type argumentType)
        {
            object handler = GetService(handlerType);

            if (handler == null)
                throw new Exception("Handler not registered for type " + argumentType.Name);

            try
            {
                MethodInfo handleMethod = handlerType.GetMethod("HandleAsync", new[] { argumentType });

                return (TReturnValue)handleMethod.Invoke(handler, new[] { argument });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
        }
    }
}