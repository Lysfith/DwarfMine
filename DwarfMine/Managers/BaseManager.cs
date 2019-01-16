using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwarfMine.Managers
{
    public abstract class BaseManager
    {
        public BaseManager()
        {
            
        }

        public abstract void Load(ContentManager content);
    }
}
