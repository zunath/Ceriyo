using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    public class ScreenService: IScreenService
    {
        private IScreen _screen;
        private readonly IScreenFactory _screenFactory;
        private readonly EntityWorld _world;

        public ScreenService(EntityWorld world,
            IScreenFactory screenFactory)
        {
            _world = world;
            _screenFactory = screenFactory;
        }

        public void ChangeScreen<T>()
            where T : IScreen
        {
            IScreen screen = _screenFactory.Create(typeof(T));
            
            _screen?.Close();
            _world.Clear();

            _screen = screen;
            _screen.Initialize();
        }

        public void Update()
        {
            _screen.Update();
        }

        public void Draw()
        {
            _screen.Draw();
        }
    }
}
