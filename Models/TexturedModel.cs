using MainApp.Textures;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Models
{
    class TexturedModel
    {
        public RawModel RawModel { get; private set; }
        public ModelTexture Texture { get; private set; }
        public TexturedModel(RawModel model, ModelTexture texture)
        {
            this.RawModel = model;
            this.Texture = texture;
        }
    }
}
