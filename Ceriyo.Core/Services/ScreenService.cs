using System;
using Artemis;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Services
{
    public class ScreenService: IScreenService
    {
        private IScreen _screen;
        private readonly EntityWorld _world;

        public ScreenService(EntityWorld world)
        {
            _world = world;
        }

        public void ChangeScreen<T>()
            where T : IScreen
        {
            IScreen screen = Activator.CreateInstance<T>();

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
