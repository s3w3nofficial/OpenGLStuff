using System;
using System.Collections.Generic;
using System.Text;
using MainApp.Models;
using OpenTK.Graphics.OpenGL4;

namespace MainApp.Loaders
{
    class Loader
    {
        public RawModel LoadToVAO(float[] positions, int[] indicies)
        {
            int vaoID = createVAO();
            BindIndiciesBuffer(indicies);
            StoreDataInAtrributeList(0, positions);
            UnBindVAO();
            return new RawModel(vaoID, indicies.Length);
        }

        private int createVAO()
        {
            int vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        private void StoreDataInAtrributeList(int attributeNumber, float[] data)
        {
            int vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeNumber, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UnBindVAO() => GL.BindVertexArray(0);

        private void BindIndiciesBuffer(int[] indicies)
        {
            int vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indicies.Length * sizeof(int), indicies, BufferUsageHint.StaticDraw);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
