using System.Threading.Tasks;

namespace AirTickets.Activation
{
    /// <summary>
    /// For mare information on understanding and extending flow see:
    /// <a href="https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/activation.md">here</a>.
    /// </summary>
    internal abstract class ActivationHandler
    {
        /// <summary>
        /// CanHandle cheks the args is of type you have configured
        /// </summary>
        public abstract bool CanHandle(object args);

        /// <summary>
        /// Do activation stuff
        /// </summary>
        public abstract Task HandleAsync(object args);
    }

    /// <summary>
    /// Extend this class to implement new ActivationHandlers
    /// </summary>
    internal abstract class ActivationHandler<T> : ActivationHandler where T : class
    {
        public override async Task HandleAsync(object args) => await HandleInternalAsync(args as T);

        /// <summary>
        /// Override this method to add the activation logic in your activation handler
        /// </summary>
        protected abstract Task HandleInternalAsync(T args);

        /// <summary>
        /// CanHandle cheks the args is of type you have configured
        /// </summary>
        public override bool CanHandle(object args) => args is T && CanHandleInternal(args as T);

        /// <summary>
        /// You can override this method to add extra validation on activation args
        /// <para>to determinate if your ActivationHandler shoul handle this activation args.</para>
        /// </summary>
        protected virtual bool CanHandleInternal(T args) => true;
    }
}