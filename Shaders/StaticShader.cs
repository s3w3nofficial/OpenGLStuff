using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Shaders
{
    class StaticShader : ShaderProgram
    {
        public StaticShader() : base("Shaders/vertexShader.glsl", "Shaders/fragmentShader.glsl")
        {

        }
        protected override void BindAttributes()
        {
            this.BindAttribute(0, "position");
        }
    }
}
