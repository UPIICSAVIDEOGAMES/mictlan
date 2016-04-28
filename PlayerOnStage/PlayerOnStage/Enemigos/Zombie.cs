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
    class Zombie : Enemigo
    {
        Animacion_Textura animacionZombie;
        Leer_Textura zombie;
        int FACTOR_DANO = 25;
        //      POSICION DE INICIO Y VARIABLE DE VELOCIDAD DEL PLAYER

        public Vector2 velocity;
        //La vida del zombie
        HudEnemigo helth;

        Texture2D zombieText;

        public Vector2 zombiePos;
        bool caminar;
        public bool siguiendo;
        bool atack;

        float playerDistanceX;
        float playerDistanceY;
        //      EFECTO DE SPRITE; EN ESTE CASO GIRAR DERECHA O IZQUIERDA
        public bool flipeado = false;

        public Zombie()
        {
            base.enemigo_factor_daño = FACTOR_DANO;
            enemPosicion = new Vector2(12160, 1220);

            helth = new HudEnemigo(this);

        }

        public void Load(ContentManager Content)
        {
            zombie = new Leer_Textura(Content.Load<Texture2D>("Acciones/zombieDer"), 61, 0.12f, true);
            zombieText = Content.Load<Texture2D>("Rectangles/RectangleZombie");
        }

        public void Update(Player player)
        {

            enemigoRect = new Rectangle((int)zombiePos.X, (int)zombiePos.Y, zombieText.Width, zombieText.Height);

            zombiePos.X = enemPosicion.X - 28;
            zombiePos.Y = enemPosicion.Y - 100;

            enemPosicion += velocity;
            playerDistanceX = player.posicion.X - enemPosicion.X;
            playerDistanceY = player.posicion.Y - enemPosicion.Y;
            siguiendo = true;
            caminar = true;

            helth.Update(player);

            acciones(player);



        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            SpriteEffects flip = SpriteEffects.None;
            if (flipeado == false)
                flip = SpriteEffects.None;
            else if (flipeado == true)
                flip = SpriteEffects.FlipHorizontally;
           // spriteBatch.Draw(zombieText, enemigoRect, Color.White);
            animacionZombie.Draw(gameTime, spriteBatch, enemPosicion, flip);
        }
        public void acciones(Player player)
        {
            // ** ACCIÓN DE CAMINAR ** //
            if (caminar == true)
            {
                animacionZombie.PlayAnimation(zombie);
            }
            // *** SEGUIR AL ENEMIGO (JUGADOR RITCHER) *** //

            if (siguiendo == true)
            {


                if (playerDistanceX >= -900 && playerDistanceX <= 900 && playerDistanceY >= -900 && playerDistanceY <= 900)
                {
                    if (playerDistanceX < 10 && playerDistanceY < 10)
                    {

                        flipeado = true;
                        velocity.X = -0.3f;
                        //velocity.Y = -1f;
                    }
                    else if (playerDistanceX < 10 && playerDistanceY > 10)
                    {
                        flipeado = true;
                        velocity.X = -0.3f;
                        //velocity.Y = 1f;
                    }
                    else if (playerDistanceX > 10 && playerDistanceY > 10)
                    {
                        flipeado = false;
                        velocity.X = 0.3f;
                        //velocity.Y = 1f;
                    }
                    else if (playerDistanceX > 10 && playerDistanceY < 10)
                    {
                        flipeado = false;
                        velocity.X = 0.3f;
                        //velocity.Y = -1f;
                    }

                    //atacar cuando estés cerca del enemigo
                    else if (playerDistanceX < 0.2f)
                    {
                        atack = true;
                    }

                }
                siguiendo = false;

            }
            else if (siguiendo == false) { velocity.X = 0f; }
        }



        public void setposicion(Vector2 posicion)
        {
            this.enemPosicion = posicion;
        }
        public Vector2 getposicion()
        {
            return this.enemPosicion;
        }

    }
}
