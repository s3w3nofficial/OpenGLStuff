using MainApp.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MainApp.Loaders
{
    static class OBJLoader
    {
        public static RawModel LoadModelOBJ(string fileName, Loader loader)
        {
            using StreamReader sr = new StreamReader($"assets/models/{fileName}.obj");

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> textures = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indices = new List<int>();
            float[] verticesArray = null;
            float[] normalsArray = null;
            float[] textureArray = null;
            int[] indicesArray = null;

            string line = null;
            while(true)
            {
                line = sr.ReadLine();
                string[] currentLine = line.Split(' ');
                if (line.StartsWith("v "))
                {
                    Vector3 vertex = new Vector3(
                        float.Parse(currentLine[1]),
                        float.Parse(currentLine[2]),
                        float.Parse(currentLine[3]));
                    vertices.Add(vertex);
                }
                else if (line.StartsWith("vt "))
                {
                    Vector2 texture = new Vector2(
                        float.Parse(currentLine[1]),
                        float.Parse(currentLine[2]));
                    textures.Add(texture);
                } 
                else if (line.StartsWith("vn "))
                {
                    Vector3 normal = new Vector3(
                        float.Parse(currentLine[1]),
                        float.Parse(currentLine[2]),
                        float.Parse(currentLine[3]));
                    normals.Add(normal);
                }
                else if (line.StartsWith("f "))
                {
                    textureArray = new float[vertices.Count * 2];
                    normalsArray = new float[vertices.Count * 3];
                    break;
                }
            }

            while(line != null)
            {
                if(!line.StartsWith("f "))
                {
                    line = sr.ReadLine();
                    continue;
                }

                string[] currentLine = line.Split(" ");
                string[] vertex1 = currentLine[1].Split("/");
                string[] vertex2 = currentLine[2].Split("/");
                string[] vertex3 = currentLine[3].Split("/");

                processVertex(vertex1, indices, textures, normals, textureArray, normalsArray);
                processVertex(vertex2, indices, textures, normals, textureArray, normalsArray);
                processVertex(vertex3, indices, textures, normals, textureArray, normalsArray);

                line = sr.ReadLine();
            }

            verticesArray = new float[vertices.Count * 3];
            indicesArray = new int[indices.Count];

            int vertexPointer = 0;
            foreach (var vertex in vertices)
            {
                verticesArray[vertexPointer++] = vertex.X;
                verticesArray[vertexPointer++] = vertex.Y;
                verticesArray[vertexPointer++] = vertex.Z;
            }

            for (int i = 0; i < indices.Count; i++)
                indicesArray[i] = indices[i];

            return loader.LoadToVAO(verticesArray, textureArray, indicesArray);
        }

        private static void processVertex(
            string[] vertexData, 
            List<int> indicies,
            List<Vector2> textures,
            List<Vector3> normals,
            float[] textureArray,
            float[] normalsArray)
        {
            int currentVertexPointer = int.Parse(vertexData[0]) - 1;
            indicies.Add(currentVertexPointer);
            Vector2 currentTexture = textures[int.Parse(vertexData[1]) - 1];
            textureArray[currentVertexPointer * 2] = currentTexture.X;
            textureArray[currentVertexPointer * 2 + 1] = 1 - currentTexture.Y;
            Vector3 currentNorm = normals[int.Parse(vertexData[2]) - 1];
            normalsArray[currentVertexPointer * 3] = currentNorm.X;
            normalsArray[currentVertexPointer * 3 + 1] = currentNorm.Y;
            normalsArray[currentVertexPointer * 3 + 2] = currentNorm.Z;
        }
    }
}
