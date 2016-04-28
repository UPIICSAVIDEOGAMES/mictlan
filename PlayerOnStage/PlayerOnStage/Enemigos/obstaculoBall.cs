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
    class obstaculoBall : Enemigo
    {
        Animacion_Textura animacionObsBall;
        public Vector2 position;
        Leer_Textura ObsBall;

        public Texture2D obsBallText;

         public obstaculoBall() : base()
        {
            position = new Vector2(800,600);//aqui es donde iniciara
           
        }

         public void Load(ContentManager Content)
         {
             ObsBall = new Leer_Textura(Content.Load<Texture2D>("Rectangles/ObsBall"), 50, 0.12f, false);
             obsBallText = Content.Load<Texture2D>("Rectangles/RectangleDeath");

         }

         public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
         {
             SpriteEffects flip = SpriteEffects.None;
             spriteBatch.Draw(obsBallText, enemigoRect, Color.White);
             animacionObsBall.Draw(gameTime, spriteBatch, position, flip);

             // spriteBatch.Draw(Cabeza, new Rectangle((int)position.X, (int)position.Y, Cabeza.Width, Cabeza.Height), Color.White);

         }

         public void Update()
         {
             animacionObsBall.PlayAnimation(ObsBall);
            enemigoRect = new Rectangle((int)position.X - 30, (int)position.Y - 60,obsBallText.Width,obsBallText.Height);

         }
    }
}
