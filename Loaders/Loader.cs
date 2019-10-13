using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using MainApp.Models;
using OpenTK.Graphics.OpenGL4;

namespace MainApp.Loaders
{
    class Loader
    {
        public RawModel LoadToVAO(float[] positions, float[] textureCoords, int[] indicies)
        {
            int vaoID = createVAO();
            BindIndiciesBuffer(indicies);
            StoreDataInAtrributeList(0, 3, positions);
            StoreDataInAtrributeList(1, 2, textureCoords);
            UnBindVAO();
            return new RawModel(vaoID, indicies.Length);
        }

        public int LoadTexture(string fileName)
        {
            string file = $"assets/textures/{fileName}.png";
            using FileStream fs = File.OpenRead(file);
            Bitmap img = (Bitmap)Image.FromStream(fs);

            int id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);

            BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            img.UnlockBits(data);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return id;
        }

        private int createVAO()
        {
            int vaoID = GL.GenVertexArray();
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        private void StoreDataInAtrributeList(int attributeNumber, int coordSize, float[] data)
        {
            int vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeNumber, coordSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UnBindVAO() => GL.BindVertexArray(0);

        private void BindIndiciesBuffer(int[] indicies)
        {
            int vboID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indicies.Length * sizeof(int), indicies, BufferUsageHint.StaticDraw);
        }
    }
}
