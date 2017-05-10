using Autofac;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Infrastructure.Factory
{
    public class UIViewModelFactory: IUIViewModelFactory
    {
        private readonly IComponentContext _context;

        public UIViewModelFactory(IComponentContext context)
        {
            _context = context;
        }

        public T Create<T>() where T : IUIViewModel
        {
            return (T)_context.ResolveNamed<IUIViewModel>(typeof(T).ToString());
        }
    }
}
