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
    class Estrellas : Enemigo
    {

        //      CLASE QUE EJECUTA LOS FOTOGRAMAS DEL SPRITE
        public Animacion_Textura animacionSinCab;

        //      RICHTER: SPRITES QUE SERAN USADOS EN EL JUEGO
        Leer_Textura atacar;
        Leer_Textura caminar;
        Leer_Textura dies; //muere
        Leer_Textura stand; //parado
        Leer_Textura hurts;
        Leer_Textura saltar;

        //      RECTANGULOS DE COLISION DE RICHTER (atacado, pararse en plataforma)
        Texture2D starhitText; //tronco

        //La vida del zombie
        HudEnemigo health;

        //      BOOLEANOS DE LAS ACCIONES A EJECUTAR;
        public bool salto = false;
        public bool atack = false;
        public bool gready = false;
        public bool caminando = false;
        public bool siguiendo = false;
        public bool piso = false;
        public bool flip = false;

        float playerDistance;

        //      EFECTO DE SPRITE; EN ESTE CASO GIRAR DERECHA O IZQUIERDA
        private bool flipeado;

        //      VALOR DE LA VELOCIDAD DE TRASLACION APLICADO EN Acciones()
        float velValor = 0.15f;

        float timeCounter = 0f;


        //      POSICION DE INICIO Y VARIABLE DE VELOCIDAD DEL PLAYER
        public Vector2 velocity;

        Random random = new Random();
        float randomX, randomY;
        int RANGO_X = 280;
        int RANGO_Y = 300;
        float VALOR_SUMATORIA_RANDOM = 0.25F;
        float VALOR_GOLPEADO = 26F;
        int PISO_STAR = 1028;
        int APROX_APARECER_Y = 650;

        int FACTOR_DANO = 20;


        //      ATRIBUTOS POR DEFECTO AL CREAR UN PLAYER
        public Estrellas(Player player): base()
        {
            base.enemigo_factor_daño = FACTOR_DANO;

            health = new HudEnemigo(this);

            randomX = random.Next((int)player.posicion.X - RANGO_X, (int)player.posicion.X + RANGO_X);
            randomY = random.Next(RANGO_Y, APROX_APARECER_Y);

            enemPosicion = new Vector2(randomX, randomY);

            if (enemPosicion.X < player.posicion.X)
            {
                flipeado = false;
            }

            else if (enemPosicion.X > player.posicion.X)
            {
                flipeado = true;
            }

            salto = true;
            flip = true;
            atack = false;
            gready = false;
            siguiendo = false;
            caminando = false;
            piso = false;
            enemGetHit = false;
            enemDie = false;

        }



        //+++++++++++++++++++++++++    METODOS OBLIGADOS DEL JUEGO    ++++++++++++++++++++++++++++++++++++++
        public void Load(ContentManager Content)
        {
            caminar = new Leer_Textura(Content.Load<Texture2D>("Acciones/Caminar"), 61, 0.12f, true);
            stand = new Leer_Textura(Content.Load<Texture2D>("Acciones/Stand"), 80, 0.10f, false);
            dies = new Leer_Textura(Content.Load<Texture2D>("Acciones/Die"), 80, 0.10f, false);
            hurts = new Leer_Textura(Content.Load<Texture2D>("Acciones/Herir"), 60, 0.10f, true);
            atacar = new Leer_Textura(Content.Load<Texture2D>("Acciones/SimAtk"), 80, 0.12f, false);
            saltar = new Leer_Textura(Content.Load<Texture2D>("Acciones/Saltar"), 61, 0.12f, false);

            //      RECTANGULOS UTILIZADOS PARA LAS COLISIONES
            starhitText = Content.Load<Texture2D>("Rectangles/RectangleHit");


        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;

            if (flipeado == false)
                flip = SpriteEffects.None;
            else if (flipeado == true)
                flip = SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(starhitText, enemigoRect, Color.White);
            animacionSinCab.Draw(gameTime, spriteBatch, enemPosicion, flip);


        }



        public void Update(Player player)
        {

            //animacionPlayer.PlayAnimation(caminar);
            enemPosicion += velocity;

            // DISTANCIA ENTRE ENEMIGO(RITCHER) Y LOBITO
            playerDistance = player.posicion.X - enemPosicion.X;

            //
            siguiendo = false;
            piso = false;
            caminando = false;


            enemigoRect = new Rectangle((int)enemPosicion.X - 20, (int)enemPosicion.Y - 80, starhitText.Width, starhitText.Height);


            acciones(player);

            health.Update(player);
        }




        // ++++++++++++++++++++++++++++    ACCIONES A REALIZAR    ++++++++++++++++++++++++++++++++++++++++  //
        public void acciones(Player player)
        {
            //      ***SELECCION ACCIONES***      //

            #region "Entorno"

            // CUANDO YA ESTÁS SOBRE EL PISO "Y" NO SE MUEVE
            if (enemPosicion.Y >= PISO_STAR)
            {
                velocity.Y = 0f;
                piso = true;
                salto = false;
            }

            // AL NO ESTAR EN EL PISO
            else
            {
                velocity.Y = 4f;
            }

            #endregion

            #region "Salto Normal"

            if (salto)
            {
                float i = 2f;
                velocity.Y += velValor * i;
            }

            if (salto == false)
            {
                velocity.Y = 0f;
            }

            #endregion

            #region "Flip"

            if (flip)
            {
                animacionSinCab.PlayAnimation(saltar);
                flip = false;
            }

            #endregion

            #region "Muerte"

            if (enemDie)
            {
                animacionSinCab.PlayAnimation(dies);
                velocity.X = 0;
            }

            #endregion

            #region "Ataque"

            if (atack)
            {
                timeCounter += VALOR_SUMATORIA_RANDOM;
                //velocity.X = 0f;

                velocity.Y = -0.001f;
                velocity.X = 0f;

                animacionSinCab.PlayAnimation(atacar);
                //atack = false;

                if (timeCounter >= 11)
                {
                    atack = false;
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Ser Atacado"

            if (enemGetHit)
            {
                atack = false;
                //backflip = false;

                timeCounter += VALOR_SUMATORIA_RANDOM;

                enemPosicion.Y -= 21f;
                velocity.Y = -0.85f;

                animacionSinCab.PlayAnimation(hurts);
                salto = true;

                if (flipeado == false)
                {
                    enemPosicion.X -= VALOR_GOLPEADO;
                }

                else if (flipeado == true)
                {
                    enemPosicion.X += VALOR_GOLPEADO;
                }

                if (timeCounter >= 1)
                {
                    enemGetHit = false;
                    timeCounter = 0;
                }

            }

            #endregion


            #region "Buscar Posición del Jugador(Ritcher)"

            if (piso && !enemDie && !atack)
            {
                siguiendo = true; caminando = true;


                //flip para saber tu orientacion
                if (enemPosicion.X < player.posicion.X)
                {
                    flipeado = false;
                    //  velocity.X = 1.5f;
                }

                else if (enemPosicion.X > player.posicion.X)
                {
                    flipeado = true;
                    //  velocity.X = -1.5f;
                }
            }


            #endregion

            // ** ACCIÓN DE CAMINAR ** //
            if (caminando)
            {
                animacionSinCab.PlayAnimation(caminar);
                caminando = false;
            }

            // *** SEGUIR AL ENEMIGO (JUGADOR RITCHER) *** //
            if (siguiendo)
            {
                if (playerDistance >= -900 && playerDistance <= 900)
                {
                    if (playerDistance < -18)
                    {
                        velocity.X = -1f;
                    }
                    else if (playerDistance > 18)
                    {
                        velocity.X = 1f;
                    }
                    //atacar cuando estés cerca del enemigo
                    else if (playerDistance < 3)
                    {
                        atack = true;
                    }

                }
                siguiendo = false;
            }
        }

    }
}
