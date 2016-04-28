using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PlayerOnStage
{
    class Leer_Textura
    {
        Texture2D textura;

        public Texture2D Textura
        {
            get { return textura; }
        }

        public int FrameWidth;

        public int FrameHeight
        {
            get { return textura.Height; }
        }

        float frameTime;
        public float FrameTime
        {
            get { return frameTime; }
        }

        public int FrameCount;

        bool isLooping;
        public bool IsLooping
        {
            get { return isLooping; }
        }

        public Leer_Textura(Texture2D newTextura, int newFrameWidth, float newFrameTime, bool newIsLooping)
        {
            textura = newTextura;
            FrameWidth = newFrameWidth;
            frameTime = newFrameTime;
            isLooping = newIsLooping;
            FrameCount = textura.Width / FrameWidth;
        }
    }
}
