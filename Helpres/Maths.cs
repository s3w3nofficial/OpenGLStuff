using MainApp.Entities;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Helpres
{
    static class Maths
    {
        public static float AngleToRadians(float angle) =>
            (float)((Math.PI / 180) * angle);

        public static Matrix4 CreateTransformationMatrix(Vector3 position, 
            float rx, 
            float ry, 
            float rz,
            float scale)
        {
            Matrix4 transofrmationMatrix = Matrix4.Identity;
            Matrix4 translation;
            Matrix4.CreateTranslation(ref position, out translation);
            Matrix4 rotationX;
            Matrix4.CreateRotationX(rx, out rotationX);
            Matrix4 rotationY;
            Matrix4.CreateRotationY(ry, out rotationY);
            Matrix4 rotationZ;
            Matrix4.CreateRotationZ(rz, out rotationZ);
            Matrix4 scaleMatrix;
            Matrix4.CreateScale(scale, out scaleMatrix);

            transofrmationMatrix *= translation;

            transofrmationMatrix *= rotationX;
            transofrmationMatrix *= rotationY;
            transofrmationMatrix *= rotationZ;

            transofrmationMatrix *= scaleMatrix;

            return transofrmationMatrix;

        }
        public static Matrix4 CreateProjectionMatrix(
            int width, 
            int height,
            float fov,
            float near,
            float far) 
        {
            float aspectRation = (float)width / (float)height;
            var test = AngleToRadians(fov);
            float y_scale = (float)((1f / Math.Tan(AngleToRadians(fov) / 2f)) * aspectRation);
            float x_scale = y_scale / aspectRation;
            float flustrum_length = far - near;

            Matrix4 projectionMatrix = Matrix4.Identity;
            projectionMatrix[0, 0] = x_scale;
            projectionMatrix[1, 1] = y_scale;
            projectionMatrix[2, 2] = -((far + near) / flustrum_length);
            projectionMatrix[2, 3] = -1;
            projectionMatrix[3, 2] = -((2 * near * far) / flustrum_length);
            projectionMatrix[3, 3] = 0;

            return projectionMatrix;
        }

        public static Matrix4 CreateViewMatrix(Camera camera)
        {
            Matrix4 viewMatrix = Matrix4.Identity;
            Matrix4 rotationX;
            Matrix4.CreateRotationX(camera.Pitch, out rotationX);
            Matrix4 rotationY;
            Matrix4.CreateRotationY(camera.Yaw, out rotationY);
            //Matrix4 rotationZ;
            //Matrix4.CreateRotationZ(camera.Roll, out rotationZ);
            Matrix4 translation;
            Vector3 negativePosition = new Vector3(
                -camera.Position.X,
                -camera.Position.Y,
                -camera.Position.Z);
            Matrix4.CreateTranslation(ref negativePosition, out translation);

            viewMatrix = viewMatrix * rotationX;
            viewMatrix = viewMatrix * rotationY;
            viewMatrix = viewMatrix * translation;

            return viewMatrix;
        }
    }
}
