using System.Threading.Tasks;

namespace AirTickets.Activation
{
    // For more information on understanding and extending activation flow see
    // https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/activation.md
    internal abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);

        public abstract Task HandleAsync(object args);
    }

    // Extend this class to implement new ActivationHandlers
    internal abstract class ActivationHandler<T> : ActivationHandler where T : class
    {
        // Override this method to add the activation logic in your activation handler
        protected abstract Task HandleInternalAsync(object args);

        public override async Task HandleAsync(object args)
        {
            await HandleInternalAsync(args);
        }

        public override bool CanHandle(object args) => args is T && CanHandleInternal(args as T);

        /// <summary>
        /// You can override this method to add extra valigation on activation args
        /// to determinate if you ActivationHandler should handle this activation args
        /// </summary>
        protected virtual bool CanHandleInternal(T args) => true;
    }
}