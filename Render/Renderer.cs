﻿using MainApp.Models;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Render
{
    class Renderer
    {
        public void Prepare()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(1f, 0f, 0f, 1f);
        }

        public void Render(RawModel model)
        {
            GL.BindVertexArray(model.vaoID);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, model.vertexCount);
            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }
    }
}