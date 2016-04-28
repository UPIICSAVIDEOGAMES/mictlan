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
        Animacion_Textura animacionPunta;
        Leer_Textura PuntaViento;
        Vector2 velocity;
        int FACTOR_DANO = 10;
        Texture2D PuntaText;
        public Vector2 PuntaPos;

        public PuntaPedernal()
        {

            PuntaPos = new Vector2(12160, 1220);

            base.enemigo_factor_daño = FACTOR_DANO;
        }
        public void Load(ContentManager Content)
        {
            PuntaViento = new Leer_Textura(Content.Load<Texture2D>("Acciones/cierra"), 62, 0.25f, true);
            PuntaText = Content.Load<Texture2D>("Rectangles/RectangleCierra");
        }
        public void Update(GameTime gameTime)
        {


            PuntaPos.X -= 20;

            enemigoRect = new Rectangle((int)PuntaPos.X, (int)PuntaPos.Y, PuntaText.Width, PuntaText.Height);
            animacionPunta.PlayAnimation(PuntaViento);

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            spriteBatch.Draw(PuntaText, PuntaPos, Color.White);
            animacionPunta.Draw(gameTime, spriteBatch, PuntaPos, flip);

        }
    }
}
