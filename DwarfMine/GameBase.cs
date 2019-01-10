using Autofac;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine
{
    public abstract class GameBase : Game
    {
        // ReSharper disable once NotAccessedField.Local
        protected GraphicsDeviceManager GraphicsDeviceManager { get; }
        protected IContainer Container { get; private set; }

        public int Width { get; }
        public int Height { get; }

        protected GameBase(int width = 1440, int height = 900)
        {
            Width = width;
            Height = height;
            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height
            };
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromMilliseconds(16.7);
        }

        protected override void Initialize()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterDependencies(containerBuilder);
            Container = containerBuilder.Build();

            base.Initialize();
        }

        protected abstract void RegisterDependencies(ContainerBuilder builder);
    }
}
