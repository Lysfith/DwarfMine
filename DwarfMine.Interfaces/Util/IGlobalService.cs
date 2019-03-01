using DwarfMine.Core.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Util
{
    public interface IGlobalService
    {
        GraphicsDevice GetGraphicsDevice();
        CustomSpriteBatch GetSpriteBatch();
        OrthographicCamera GetCamera();
        ContentManager GetContentManager();
    }
}
