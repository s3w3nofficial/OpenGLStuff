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
        private int location_lightPosition;
        private int location_lightColour;
        private int location_shineDamper;
        private int location_reflectivity;
        public StaticShader() : base("Shaders/vertexShader.glsl", "Shaders/fragmentShader.glsl")
        {

        }
        protected override void BindAttributes()
        {
            this.BindAttribute(0, "position");
            this.BindAttribute(1, "textureCoords");
            this.BindAttribute(2, "normal");
        }

        protected override void GetAllUniformLocations()
        {
            this.location_projectionMatrix = this.GetUniformLocation("projectionMatrix");
            this.location_viewMatrix = this.GetUniformLocation("viewMatrix");
            this.location_transformationMatrix = this.GetUniformLocation("transformationMatrix");
            this.location_lightPosition = this.GetUniformLocation("lightPosition");
            this.location_lightColour = this.GetUniformLocation("lightColour");
            this.location_lightColour = this.GetUniformLocation("shineDamper");
            this.location_lightColour = this.GetUniformLocation("reflectivity");
        }

        public void LoadShineVariables(float damper, float reflectivity)
        {
            this.LoadFloat(location_shineDamper, damper);
            this.LoadFloat(location_reflectivity, reflectivity);
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

        public void LoadLight(Light light)
        {
            this.LoadVector(location_lightPosition, light.Position);
            this.LoadVector(location_lightColour, light.Colour);
        }
    }
}
