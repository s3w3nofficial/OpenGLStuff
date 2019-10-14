using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Textures
{
    class ModelTexture
    {
        public int TextureID { get; private set; }

        public float ShineDamper { get; set; } = 1;
        public float Reflectivity { get; set; } = 0;
        
        public ModelTexture(int id)
        {
            this.TextureID = id;
        }
    }
}
