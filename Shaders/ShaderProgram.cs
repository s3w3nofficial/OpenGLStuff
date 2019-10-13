using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MainApp.Shaders
{
    abstract class ShaderProgram
    {
        private int programID;
        private int vertexShaderID;
        private int fragmentShaderID;

        public ShaderProgram(string vertexfile, string fragmentfile)
        {
            this.vertexShaderID = LoadShader(vertexfile, ShaderType.VertexShader);
            this.fragmentShaderID = LoadShader(fragmentfile, ShaderType.FragmentShader);
            this.programID = GL.CreateProgram();

            GL.AttachShader(this.programID, this.vertexShaderID);
            GL.AttachShader(this.programID, this.fragmentShaderID);
            GL.LinkProgram(this.programID);
            this.BindAttributes();
            this.GetAllUniformLocations();
        }

        protected abstract void GetAllUniformLocations();

        protected int GetUniformLocation(string uniformName) 
            => GL.GetUniformLocation(this.programID, uniformName);

        public void Use() => GL.UseProgram(this.programID);
        public void UnUse() => GL.UseProgram(0);

        protected abstract void BindAttributes();
        protected void BindAttribute(int attribute, string variableName) 
            => GL.BindAttribLocation(this.programID, attribute, variableName);

        protected void LoadFloat(int location, float value) => GL.Uniform1(location, value);
        protected void LoadVector(int location, Vector3 value) => GL.Uniform3(location, value);
        protected void LoadBool(int location, bool value) => GL.Uniform1(location, value ? 1 : 0);
        protected void LoadMatrix(int location, ref Matrix4 matrix) => GL.UniformMatrix4(location, false, ref matrix);

        private static int LoadShader(string file, ShaderType type)
        {
            using StreamReader sr = new StreamReader(file);

            int shaderID = GL.CreateShader(type);

            GL.ShaderSource(shaderID, sr.ReadToEnd());
            GL.CompileShader(shaderID);

            return shaderID;
        }
    }
}
