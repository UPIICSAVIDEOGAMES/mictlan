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
    class FuegoHuitzi : Enemigo
    {
       // Animacion_Textura animacionFuegoHuitzi;
       // Leer_Textura FuegoH;
        Texture2D fuegoHuitText;
        bool quemar = false;
        float timeCountFire=0;
        int quemarDir = 1;
        float CONTADOR_SUMATORIA = 0.25F;

        int FACTOR_DANO = 500;

        public FuegoHuitzi() : base()
        {
            base.enemigo_factor_daño = FACTOR_DANO;
            enemPosicion = new Vector2(14700,1200);
            quemar = true;

        }
        public void Load(ContentManager Content)
        {
            // ObsBall = new Leer_Textura(Content.Load<Texture2D>("Rectangles/ObsBall"), 50, 0.12f, false);
           // FuegoH = new Leer_Textura(Content.Load<Texture2D>("Rectangles/fire"), 100, 0.12f, false);
            fuegoHuitText = Content.Load<Texture2D>("Rectangles/fire");
        }
        public void Update(Player player)
        {
            //animacionFuegoHuitzi.PlayAnimation(FuegoH);
            if (quemar)
            {
               

                switch (quemarDir)
                {
                    case 1:
                        timeCountFire += CONTADOR_SUMATORIA;
                            enemPosicion.Y-=1;
                        if(timeCountFire >= 120){

                            timeCountFire = 0;
                            quemarDir = 2;
                        }
                        break;
                    case 2:
                        timeCountFire += CONTADOR_SUMATORIA;
                            enemPosicion.Y+=1;
                        if(timeCountFire >= 120){

                            timeCountFire = 0;
                            quemar = false;
                            quemarDir = 3;
                        }

                        break;

                    case 3:
                        break;

                }
            }
            
            enemigoRect = new Rectangle((int)enemPosicion.X, (int)enemPosicion.Y, fuegoHuitText.Width, fuegoHuitText.Height);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //animacionFuegoHuitzi.PlayAnimation(FuegoH);
           // SpriteEffects flip = SpriteEffects.None;
           // animacionFuegoHuitzi.Draw(gameTime, spriteBatch, enemPosicion, flip);
            if(enemPosicion.Y<1058)
            spriteBatch.Draw(fuegoHuitText, enemPosicion, Color.White);
        }



    }
}
