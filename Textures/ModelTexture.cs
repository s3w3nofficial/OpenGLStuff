using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Textures
{
    class ModelTexture
    {
        public int TextureID { get; private set; }

        public ModelTexture(int id)
        {
            this.TextureID = id;
        }
    }
}
