using MainApp.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MainApp.Loaders
{
    static class OBJLoader
    {
        public static RawModel LoadModelOBJ(string fileName, Loader loader)
        {
            if (!File.Exists($"assets/models/{fileName}.obj")) throw new FileNotFoundException();

            string[] lines = File.ReadAllLines($"assets/models/{fileName}.obj");

            int vertexCount = lines.Where(t => t.StartsWith("v ")).Count();

            List<Vector3> vertexBuffer = new List<Vector3>();
            List<Vector3> normalBuffer = new List<Vector3>();
            List<Vector2> textureCoordBuffer = new List<Vector2>();

            float[] textureCoords = null;
            float[] normals = null;
            List<int> indices = new List<int>();

            foreach (string line in lines)
            {
                string[] split = line.Split(' ');
                switch (split[0])
                {
                    case "v":
                        vertexBuffer.Add(new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                        break;
                    case "vt":
                        textureCoordBuffer.Add(new Vector2(float.Parse(split[1]), float.Parse(split[2])));
                        break;
                    case "vn":
                        normalBuffer.Add(new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                        break;
                    case "f":
                        for (int i = 1; i < split.Length; i++)
                        {
                            if (textureCoords == null) 
                                textureCoords = new float[vertexCount * 2];
                            if (normals == null) normals = new float[vertexCount * 3];
                            
                            string[] data = split[i].Split('/');

                            int index = int.Parse(data[0]) - 1;

                            indices.Add(index);

                            Vector2 textureCoord = textureCoordBuffer[int.Parse(data[1]) - 1];
                            textureCoords[index * 2] = textureCoord.X;
                            textureCoords[index * 2 + 1] = 1 - textureCoord.Y;

                            Vector3 normal = normalBuffer[int.Parse(data[2]) - 1];
                            normals[index * 3] = normal.X;
                            normals[index * 3 + 1] = normal.Y;
                            normals[index * 3 + 2] = normal.Z;
                        }
                        break;
                }
            }

            float[] vertices = new float[vertexBuffer.Count * 3];

            int vertexIndex = 0;
            foreach (Vector3 vertex in vertexBuffer)
            {
                vertices[vertexIndex++] = vertex.X;
                vertices[vertexIndex++] = vertex.Y;
                vertices[vertexIndex++] = vertex.Z;
            }

            return loader.LoadToVAO(vertices, textureCoords, normals, indices.ToArray());
        }
    }
}
