using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PlayerOnStage
{
    class Proyectil
    {
        Texture2D Derecha;
        Texture2D Izquierda;
        public Rectangle rectangulo_flecha;
        Vector2 posicion;
        Vector2 velocity;
        bool flipeado;

        public bool Flipeado
        {
            get { return flipeado; }
            set { flipeado = value; }
        }

        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Proyectil()
        {

        }

        public void Load(ContentManager Content)
        {
            Derecha = Content.Load<Texture2D>("Proyectiles/RectangleFlechaD");
            Izquierda = Content.Load<Texture2D>("Proyectiles/RectangleFlechaI");
        }

        public void Update()
        {
            posicion += velocity;
            rectangulo_flecha = new Rectangle((int)posicion.X, (int)posicion.Y, Derecha.Width, Derecha.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (flipeado == false)
                spriteBatch.Draw(Derecha, rectangulo_flecha, Color.White);
            else
                spriteBatch.Draw(Izquierda, rectangulo_flecha, Color.White);
        }
    }
}
