using MainApp.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Entities
{
    class Entity
    {
        public TexturedModel TexturedModel { get; set; }
        public Vector3 Position { get; set; }
        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float RotationZ { get; set; }
        public float Scale { get; private set; }

        public Entity(
            TexturedModel texturedModel, 
            Vector3 position,
            float rx,
            float ry,
            float rz,
            float scale)
        {
            this.TexturedModel = texturedModel;
            this.Position = position;
            this.RotationX = rx;
            this.RotationY = ry;
            this.RotationZ = rz;
            this.Scale = scale;
        }

        public void IncreasePosition(Vector3 position) => this.Position += position;
        public void IncreaseRotation(float rx, float ry, float rz)
        {
            this.RotationX += rx;
            this.RotationY += ry;
            this.RotationZ += rz;
        }
    }
}
