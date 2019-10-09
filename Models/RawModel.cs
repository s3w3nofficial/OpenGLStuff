using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Models
{
    class RawModel
    {
        public int vaoID { get; private set; }
        public int vertexCount { get; private set; }
        public RawModel(int vaoID, int vertexCount)
        {
            this.vaoID = vaoID;
            this.vertexCount = vertexCount;
        }
    }
}
