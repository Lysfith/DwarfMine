using DwarfMine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.States.StateImplementation.Game.Components.RegionLayers
{
    public abstract class RegionLayer
    {
        public enum LayerType
        {
            FLOOR,
            GRID
        }

        public readonly LayerType Type;
        public readonly Rectangle Rectangle;
        public bool IsEnabled { get; private set; } = true;
        public bool IsDirty { get; protected set; } = true;

        public RegionLayer(LayerType type, Rectangle rectangle)
        {
            Type = type;
            Rectangle = rectangle;

        }

        public abstract void Update(GameTime time);
        public abstract void Draw(GameTime time, CustomSpriteBatch spriteBatch);
        public abstract void DisposeResources();

        public void Dirty()
        {
            IsDirty = true;
        }

        public void Enable()
        {
            IsEnabled = true;
            Dirty();
        }

        public void Disable()
        {
            IsEnabled = false;
            DisposeResources();
        }
    }
}
