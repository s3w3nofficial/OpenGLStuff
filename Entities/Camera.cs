using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Entities
{
    class Camera
    {
        public Vector3 Position { get; private set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }
        public float Roll { get; private set; }
        public Camera()
        {
        }

        public void Move(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.W)
                Position = new Vector3(Position.X, Position.Y, Position.Z - 0.2f);
            if (e.Key == Key.S)
                Position = new Vector3(Position.X, Position.Y, Position.Z + 0.2f);
            if (e.Key == Key.A)
                Position = new Vector3(Position.X - 0.2f, Position.Y, Position.Z);
            if (e.Key == Key.D)
                Position = new Vector3(Position.X + 0.2f, Position.Y, Position.Z);
            if (e.Key == Key.Space)
                Position = new Vector3(Position.X, Position.Y + 0.2f, Position.Z);
            if (e.Key == Key.LShift)
                Position = new Vector3(Position.X, Position.Y - 0.2f, Position.Z);
        }
    }
}
