using MainApp.Loaders;
using MainApp.Models;
using MainApp.Render;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace MainApp.Engine
{
    class Game
    {
        float[] verticies =
        {
            -0.5f, 0.5f, 0f,
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
            0.5f, 0.5f, 0f,

        };

        int[] indicies =
        {
            0,1,3,
            3,1,2
        };

        public DisplayManager Display { get; private set; }
        public Loader Loader { get; private set; }
        public Renderer Renderer { get; private set; }
        private RawModel TestModel;
        public Game()
        {
            this.Display = new DisplayManager(1280, 720);
            this.Loader = new Loader();
            this.Renderer = new Renderer();

            TestModel = this.Loader.LoadToVAO(verticies, indicies);

            GL.Viewport(0, 0, 1280, 720);

            this.Init();
        }

        private void Init()
        {
            this.Display.UpdateFrame += UpdateFrame;
            this.Display.RenderFrame += RenderFrame;
        }

        private void RenderFrame(object sender, OpenTK.FrameEventArgs e)
        {
            this.Renderer.Prepare();
            this.Renderer.Render(this.TestModel);

            this.Display.SwapBuffers();
        }

        private void UpdateFrame(object sender, OpenTK.FrameEventArgs e)
        {

        }

        public void Run(int frameRate)
        {
            this.Display.Run(frameRate);
        }
    }
}
