using Autofac;
using System;

namespace DwarfMine.Core
{
    public class GameCore
    {
        private static GameCore _instance;

        public static GameCore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameCore();
                }

                return _instance;
            }
        }

        public IContainer Container { get; private set; }

        private ContainerBuilder _containerBuilder { get; set; }

        public GameCore()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public void RegisterServices(Action<ContainerBuilder> action)
        {
            action?.Invoke(_containerBuilder);
            Container = _containerBuilder.Build();
        }

        public ILifetimeScope CreateScope()
        {
            return Container.BeginLifetimeScope();
        }
    }
}
