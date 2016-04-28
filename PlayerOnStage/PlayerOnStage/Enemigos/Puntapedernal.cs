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
    class PuntaPedernal : Enemigo
    {
       
        Leer_Textura PuntaViento;
        
        int FACTOR_DANO = 6;
        Texture2D PuntaText;
        public Vector2 PuntaPos;

        public PuntaPedernal()
        {

            PuntaPos = new Vector2(12160, 0);

            base.enemigo_factor_daño = FACTOR_DANO;
        }
        public void Load(ContentManager Content)
        {

            PuntaText = Content.Load<Texture2D>("Enemigos/PuntaPedernal/puntaPedernal2");
        }
        public void Update(GameTime gameTime)
        {


            PuntaPos.X -= 20;

            enemigoRect = new Rectangle((int)PuntaPos.X, (int)PuntaPos.Y, PuntaText.Width, PuntaText.Height);
           

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
         
            spriteBatch.Draw(PuntaText, PuntaPos, Color.White);
           

        }
    }
}
