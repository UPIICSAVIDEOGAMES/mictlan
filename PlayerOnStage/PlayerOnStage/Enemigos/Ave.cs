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
    class Ave : Enemigo
    {

        int Ataca = 1;
        int FACTOR_DANO = 10;
        Texture2D AveText;
        public Vector2 AvePos;
        HudEnemigo health;


        public Ave()
        {
            base.enemigo_factor_daño = FACTOR_DANO;
            AvePos = new Vector2(8000, 700);
            health = new HudEnemigo(this);
        }
        public void Load(ContentManager Content)
        {

            AveText = Content.Load<Texture2D>("Rectangles/RectangleCierra");
        }
        public void Update(GameTime gameTime, Player player)
        {

            enemigoRect = new Rectangle((int)AvePos.X, (int)AvePos.Y, AveText.Width, AveText.Height);

            switch (Ataca)
            {
                case 1:

                    if (player.posicion.X < AvePos.X)
                    {

                        AvePos.X -= 5;
                    }
                    else if (player.posicion.X > AvePos.X)
                    {

                        AvePos.X += 5;
                    }
                   if (player.posicion.X - AvePos.X < 10 && player.posicion.X - AvePos.X>-10 )
                    {
                        if (player.posicion.Y - 100 < AvePos.Y)
                        {

                            AvePos.Y -= 10;
                        }
                        else if (player.posicion.Y - 100 >= AvePos.Y)
                        {

                            AvePos.Y += 10;
                        }
                        if (player.rectangulo_cuerpo.Intersects(enemigoRect))
                        {
                            Ataca = 2;
                        }

                    }
                   
                    break;
                case 2:
                    AvePos.X += 5;
                    AvePos.Y -= 3;


                    break;

            }
            health.Update(player);

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(AveText, AvePos, Color.White);


        }
    }
}

