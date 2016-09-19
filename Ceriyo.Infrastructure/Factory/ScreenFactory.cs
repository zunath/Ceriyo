using Autofac;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Infrastructure.Factory
{
    public class ScreenFactory: IScreenFactory
    {
        private readonly IComponentContext _context;

        public ScreenFactory(IComponentContext context)
        {
            _context = context;
        }

        public IScreen Create<T>() where T : IScreen
        {
            return _context.ResolveNamed<IScreen>(typeof (T).ToString());
        }
    }
}
