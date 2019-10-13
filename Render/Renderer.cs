using MainApp.Entities;
using MainApp.Helpres;
using MainApp.Models;
using MainApp.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Render
{
    class Renderer
    {
        Matrix4 projectionMatrix;
        public Renderer(StaticShader shader)
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                (float)((Math.PI / 180) * 70f), 
                (float)(1280f / 720f), 
                0.1f, 
                1000f);
            shader.Use();
            shader.LoadProjectionMatrix(ref projectionMatrix);
            shader.UnUse();
        }

        public void Prepare()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(1f, 0f, 0f, 1f);
        }

        public void Render(Entity entity, StaticShader shader)
        {
            TexturedModel texturedModel = entity.TexturedModel;
            RawModel model = texturedModel.RawModel;
            GL.BindVertexArray(model.vaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            Matrix4 transformationMatrix = Maths.CreateTransformationMatrix(
                entity.Position, 
                entity.RotationX, 
                entity.RotationY,
                entity.RotationZ, 
                entity.Scale);

            shader.LoadTransformationMatrix(ref transformationMatrix);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texturedModel.Texture.TextureID);
            GL.DrawElements(PrimitiveType.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }
    }
}