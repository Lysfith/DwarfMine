using DwarfMine.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.States
{
    public interface IScene
    {
        void Start();
        void End();
        void Pause();
        void Resume();
        void Update(GameTime time, OrthographicCamera camera);
        void Draw(GameTime time, CustomSpriteBatch spritebatch, OrthographicCamera camera);
    }
}
