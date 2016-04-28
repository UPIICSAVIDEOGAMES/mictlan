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
    class Pua : Enemigo
    {
        Texture2D PuaText;
        public Vector2 PuaPos;
        int FACTOR_DANO = 20;

        public Pua()
        {
            PuaPos.X = 600;
            PuaPos.Y = 1150;

            base.enemigo_factor_daño = FACTOR_DANO;
        }
        public void Load(ContentManager Content)
        {

            PuaText = Content.Load<Texture2D>("Acciones/cierra");
        }
        public void Update()
        {

            enemigoRect = new Rectangle((int)PuaPos.X, (int)PuaPos.Y, PuaText.Width, PuaText.Height);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(PuaText, PuaPos, Color.White);



        }


    }
}
