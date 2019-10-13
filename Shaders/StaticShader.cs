using MainApp.Entities;
using MainApp.Helpres;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Shaders
{
    class StaticShader : ShaderProgram
    {
        private int location_projectionMatrix;
        private int location_viewMatrix;
        private int location_transformationMatrix;
        public StaticShader() : base("Shaders/vertexShader.glsl", "Shaders/fragmentShader.glsl")
        {

        }
        protected override void BindAttributes()
        {
            this.BindAttribute(0, "position");
            this.BindAttribute(1, "textureCoords");
        }

        protected override void GetAllUniformLocations()
        {
            this.location_projectionMatrix = this.GetUniformLocation("projectionMatrix");
            this.location_viewMatrix = this.GetUniformLocation("viewMatrix");
            this.location_transformationMatrix = this.GetUniformLocation("transformationMatrix");
        }

        public void LoadTransformationMatrix(ref Matrix4 matrix)
        {
            this.LoadMatrix(location_transformationMatrix, ref matrix);
        }

        public void LoadProjectionMatrix(ref Matrix4 matrix)
        {
            this.LoadMatrix(location_projectionMatrix, ref matrix);
        }

        public void LoadViewMatrix(Camera camera)
        {
            Matrix4 viewMatrix = Maths.CreateViewMatrix(camera);
            this.LoadMatrix(location_viewMatrix, ref viewMatrix);
        }
    }
}
