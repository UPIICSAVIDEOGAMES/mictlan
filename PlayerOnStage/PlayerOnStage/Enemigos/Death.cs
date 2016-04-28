using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlayerOnStage
{
    class Death
    {
        
        Animacion_Textura animacionDeath; 
        Leer_Textura reaper;
        public Vector2 posicion;

        Texture2D reaperText;
        public Rectangle reaperRect;
        public Vector2 reaperPos;

        public Death()
        {

        }

        public void Load(ContentManager Content)
        {
            reaper = new Leer_Textura(Content.Load<Texture2D>("Acciones/zombieDer"), 61, 0.12f, true);
            reaper = new Leer_Textura(Content.Load<Texture2D>("Acciones/zombieIzq"), 61, 0.12f, true);

            reaperText = Content.Load<Texture2D>("Rectangles/RectangleZombie");
        }

        public void Update(GameTime gameTime, Player player)
        {
            reaperRect = new Rectangle((int)reaperPos.X, (int)reaperPos.Y, reaperText.Width, reaperText.Height);

            reaperPos.X = posicion.X - 28;
            reaperPos.Y = posicion.Y - 28;


            posicion.X = Mouse.GetState().X;
            posicion.Y = Mouse.GetState().Y + 33;

            animacionDeath.PlayAnimation(reaper);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            SpriteEffects flip = SpriteEffects.None;
            spriteBatch.Draw(reaperText, reaperRect, Color.White);
            animacionDeath.Draw(gameTime, spriteBatch, posicion, flip);
        }
    }
}
