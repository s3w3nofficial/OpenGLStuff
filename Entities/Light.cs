using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Entities
{
    class Light
    {
        public Vector3 Position { get; private set; }
        public Vector3 Colour { get; set; }
        public Light(Vector3 position, Vector3 colour)
        {
            this.Position = position;
            this.Colour = colour;
        }
    }
}
