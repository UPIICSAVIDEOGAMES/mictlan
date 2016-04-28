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
    class Cierra : Enemigo
    {
        //variables de la cirra

        Animacion_Textura animacionCierra;
        Leer_Textura cierraGirando;
        Vector2 velocity;
        bool lanzar = true;
        bool hasJumped = true;
        int caso;
        Texture2D cierraText;
        int FACTOR_DANO = 40;
        Vector2 cierraPos;
        // Velociadad de la cierra en el piso
        public int velocidadPiso;

        public Cierra()
        {
            velocidadPiso = 1;
            base.enemigo_factor_daño = FACTOR_DANO;

        }
        public void Load(ContentManager Content)
        {
            cierraGirando = new Leer_Textura(Content.Load<Texture2D>("Acciones/cierra"), 62, 0.25f, true);
            cierraText = Content.Load<Texture2D>("Rectangles/RectangleCierra");

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            //spriteBatch.Draw(cierraText, cierraPos, Color.White);
            animacionCierra.Draw(gameTime, spriteBatch, enemPosicion, flip);
        }
        //Esta funcion se encarga de sumar o restar una posicion aleatoria inicial a las cierras que caen hacia la derecha o izquirda
        //Esta funcion se encarga de sumar o restar una posicion aleatoria inicial a las cierras que caen hacia la derecha o izquirda
        public void setPisicionInicial(Player player, bool movIzq)
        {


            if (movIzq)
            {
                caso = 1;// Izquierda
                enemPosicion.X = player.posicion.X - 100;
            }
            else
            {
                caso = 2;//Derecha
                enemPosicion.X = player.posicion.X + 100;

            }

        }
        public void Update(Player player)
        {
            enemigoRect = new Rectangle((int)cierraPos.X, (int)cierraPos.Y, cierraText.Width, cierraText.Height);
            cierraPos.X = enemPosicion.X - 28;
            cierraPos.Y = enemPosicion.Y - 65;
            if (lanzar)
            {


                lanzar = false;
                //if (enemPosicion.X == 0)
                //setPisicionInicial(player, player.bool_herido);

            }
            switch (caso)
            {
                case 1:
                    if (velocity.Y == 0)
                        enemPosicion.X += velocidadPiso;
                    else
                        enemPosicion.X += 1;

                    break;
                case 2:
                    if (velocity.Y == 0)
                        enemPosicion.X -= velocidadPiso;
                    else
                        enemPosicion.X -= 1;

                    break;
            }


            enemPosicion += velocity;


            if (hasJumped == true)
            {

                velocity.Y += 1f;
            }

            if (enemPosicion.Y >= 1190)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;



            animacionCierra.PlayAnimation(cierraGirando);




        }

    }

}
