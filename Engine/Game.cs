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
                -0.5f,0.5f,-0.5f,
                -0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,-0.5f,
                0.5f,0.5f,-0.5f,

                -0.5f,0.5f,0.5f,
                -0.5f,-0.5f,0.5f,
                0.5f,-0.5f,0.5f,
                0.5f,0.5f,0.5f,

                0.5f,0.5f,-0.5f,
                0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,0.5f,
                0.5f,0.5f,0.5f,

                -0.5f,0.5f,-0.5f,
                -0.5f,-0.5f,-0.5f,
                -0.5f,-0.5f,0.5f,
                -0.5f,0.5f,0.5f,

                -0.5f,0.5f,0.5f,
                -0.5f,0.5f,-0.5f,
                0.5f,0.5f,-0.5f,
                0.5f,0.5f,0.5f,

                -0.5f,-0.5f,0.5f,
                -0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,-0.5f,
                0.5f,-0.5f,0.5f

        };

        float[] textureCoords = {

                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0,
                0,0,
                0,1,
                1,1,
                1,0


        };

        int[] indices = {
                0,1,3,
                3,1,2,
                4,5,7,
                7,5,6,
                8,9,11,
                11,9,10,
                12,13,15,
                15,13,14,
                16,17,19,
                19,17,18,
                20,21,23,
                23,21,22

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

            //this.TestModel = this.Loader.LoadToVAO(vertices, textureCoords, indices);
            this.TestModel = OBJLoader.LoadModelOBJ("dragon", this.Loader);
            this.texture = new ModelTexture(this.Loader.LoadTexture("texture"));
            this.texturedModel = new TexturedModel(this.TestModel, this.texture);
            this.entity = new Entity(this.texturedModel, new Vector3(0, 0, -25f), 0, 0, 0, 1f);
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

            //entity.IncreasePosition(new Vector3(0f, 0f, 0f)); 
            entity.IncreaseRotation(0f, 0.02f, 0f);

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