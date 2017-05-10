using Autofac;
using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Factory
{
    public class ServerActionFactory: IServerActionFactory
    {
        private readonly IComponentContext _context;

        public ServerActionFactory(IComponentContext context)
        {
            _context = context;
        }

        public T Create<T>() where T : IServerAction
        {
            return (T) _context.ResolveNamed<IServerAction>(typeof(T).ToString());
        }
    }
}
