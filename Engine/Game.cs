using MainApp.Loaders;
using MainApp.Models;
using MainApp.Render;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using MainApp.Shaders;
using OpenTK;
using MainApp.Helpres;
using MainApp.Textures;
using MainApp.Entities;

namespace MainApp.Engine
{
    class Game
    {
        float[] vertices = {
                -0.5f, 0.5f, 0f,//v0
				-0.5f, -0.5f, 0f,//v1
				0.5f, -0.5f, 0f,//v2
				0.5f, 0.5f, 0f,//v3
		};

        int[] indices = {
                0,1,3,//top left triangle (v0, v1, v3)
				3,1,2//bottom right triangle (v3, v1, v2)
		};

        float[] textureCoords =
        {
            0f,0f,
            0f,1f,
            1f,1f,
            1f,0f
        };


        public DisplayManager Display { get; private set; }
        public Loader Loader { get; private set; }
        public Renderer Renderer { get; private set; }
        public StaticShader StaticShader { get; private set; }
        private RawModel TestModel;
        private ModelTexture texture;
        private TexturedModel texturedModel;
        private Entity entity;
        private Camera camera;
        float time = 0f;
        public Game()
        {
            this.Display = new DisplayManager(1280, 720);
            this.Loader = new Loader();
            this.StaticShader = new StaticShader();
            this.Renderer = new Renderer(this.StaticShader);

            this.TestModel = this.Loader.LoadToVAO(vertices, textureCoords, indices);
            this.texture = new ModelTexture(this.Loader.LoadTexture("texture"));
            this.texturedModel = new TexturedModel(this.TestModel, this.texture);
            this.entity = new Entity(this.texturedModel, new Vector3(0, 0, -2f), 0, 0, 0, 1);
            this.camera = new Camera();

            this.Init();
        }

        private void Init()
        {
            this.Display.UpdateFrame += UpdateFrame;
            this.Display.RenderFrame += RenderFrame;
            this.Display.KeyDown += Display_KeyDown;
            this.Display.Resize += Display_Resize;
        }

        private void RenderFrame(object sender, OpenTK.FrameEventArgs e)
        {
            time = (float)e.Time;

            //entity.IncreasePosition(new Vector3(0f, 0f, -0.002f));
            //entity.IncreaseRotation(0.002f, 0.002f, 0f);

            this.Renderer.Prepare();

            this.StaticShader.Use();

            this.StaticShader.LoadViewMatrix(this.camera);

            this.Renderer.Render(this.entity, this.StaticShader);

            this.StaticShader.UnUse();

            this.Display.SwapBuffers();
        }

        private void UpdateFrame(object sender, OpenTK.FrameEventArgs e)
        {

        }

        private void Display_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, 1280, 720);
        }

        private void Display_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            camera.Move(e);
        }

        public void Run(int frameRate)
        {
            this.Display.Run(frameRate);
        }
    }
}