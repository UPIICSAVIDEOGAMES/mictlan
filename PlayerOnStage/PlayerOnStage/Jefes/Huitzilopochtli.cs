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
    class Huitzilopochtli : Jefe
    {
        //      CLASE QUE EJECUTA LOS FOTOGRAMAS DEL SPRITE
        public Animacion_Textura animacHuitzi;

        //      RICHTER: SPRITES QUE SERAN USADOS EN EL JUEGO
        Leer_Textura atacar;
        Leer_Textura caminar;
        Leer_Textura cAtk;
        Leer_Textura dies; //muere
        Leer_Textura stand; //parado
        Leer_Textura saltar;
        Leer_Textura hurts;
        Leer_Textura bflip; //aventarse

        //      RECTANGULOS DE COLISION DE RICHTER (atacado, pararse en plataforma)
        Texture2D hitText; //tronco     

        //lista de enemigos

        List<Cabezas> cabezas;
        // obstaculoBall ball;
        FuegoHuitzi fuego;

        ContentManager Content;

        ///////////////////////////////////////////////////
        ///////////////////////////////////////////////////


        //      BOOLEANOS DE LAS ACCIONES A EJECUTAR;
        public bool salto = false;
        public bool backflip = false;
        public bool flip = false;
        public bool icrush = false;
        public bool siguiendo = false;
        public bool piso = false;
        public bool atack = false;
        public bool cattack = false;
        public bool shoot = false;
        public bool gready = false;
        public bool caminando = false;

        public bool ataqueFuerte = false;
        public bool ataqueGolpes = false;
        public bool devastador = false;
        bool repetir = false;
        int ataqueDir;
        int Golpes = 0;
        int subirHuit;

        public bool lanzarHeads = true;

        float timeCountRandom = 0;
        float timeCounter = 0;
        float timeHeads = 0;
        float timeGolpes = 0;
        float timeVuelta = 0;
        float timeEsperaCabeza = 0;

        Random r = new Random();
        double aleat;
        //aleat = r.NextDouble(); 

        float playerDistance;

        //      EFECTO DE SPRITE; EN ESTE CASO GIRAR DERECHA O IZQUIERDA
        bool flipeado = false;

        //      VALOR DE LA VELOCIDAD DE TRASLACION APLICADO EN Acciones()
        float velValor = 0.15f;

        //      POSICION DE INICIO Y VARIABLE DE VELOCIDAD DEL PLAYER
        public Vector2 posiHuitzi;
        public Vector2 velocity;

        float CONTADOR_SUMATORIA = 0.25F;
        float RESTSUMA_BACKFLIP = 18F;
        float RESTARX_DAÑO = 27F;
        int PISO_HUIT = 1028;

        bool prueba = false;
        bool prueba2 = false;
        private int FACTOR_DANO = 100;

        //      ATRIBUTOS POR DEFECTO AL CREAR UN PLAYER
        public Huitzilopochtli(): base()
        {
            //health = new HudJefe(this);
            base.jefe_factor_daño = FACTOR_DANO;

            posiHuitzi = new Vector2(15782, 710);

            salto = false;
            flip = true;
            backflip = false;
            icrush = false;
            gready = true;
            siguiendo = false;
            caminando = false;
            piso = false;
            cattack = false;
            jefeGetHit = false;//attbs de jefe
            jefeDie = false;//attbs de jefe

            flipeado = true;

            ataqueFuerte = false;
            ataqueGolpes = false;
            devastador = false;
            ataqueDir = 1;

            prueba = true;

            cabezas = new List<Cabezas>();

            fuego = new FuegoHuitzi();
            //ball = new obstaculoBall();
        }



        //+++++++++++++++++++++++++    METODOS OBLIGADOS DEL JUEGO    ++++++++++++++++++++++++++++++++++++++
        public void Load(ContentManager Content)
        {
            caminar = new Leer_Textura(Content.Load<Texture2D>("Acciones/Caminar"), 61, 0.12f, true);
            saltar = new Leer_Textura(Content.Load<Texture2D>("Acciones/Saltar"), 61, 0.12f, false);
            stand = new Leer_Textura(Content.Load<Texture2D>("Acciones/Stand"), 80, 0.10f, false);
            dies = new Leer_Textura(Content.Load<Texture2D>("Acciones/Die"), 80, 0.10f, false);
            atacar = new Leer_Textura(Content.Load<Texture2D>("Acciones/SimAtk"), 80, 0.12f, false);
            hurts = new Leer_Textura(Content.Load<Texture2D>("Acciones/Herir"), 60, 0.10f, true);
            bflip = new Leer_Textura(Content.Load<Texture2D>("Acciones/Backflip"), 80, 0.12f, false);
            //      ATAQUE AGACHADO
            cAtk = new Leer_Textura(Content.Load<Texture2D>("Acciones/CAttack"), 70, 0.05f, false);

            //      RECTANGULOS UTILIZADOS PARA LAS COLISIONES
            hitText = Content.Load<Texture2D>("Rectangles/RectangleHit");

            //health.Load(Content);
            fuego.Load(Content);

            this.Content = Content;

            //ball.Load(Content);
        }



        private void UpdateHeads(Vector2 posActual)
        {
            timeHeads += CONTADOR_SUMATORIA;

            if (timeHeads >= 30)
            {
                Cabezas head = new Cabezas(posActual);
                head.Load(Content);
                cabezas.Add(head);

                timeHeads = 0;
            }
        }


        public void Update(Player player)
        {

            //animacionPlayer.PlayAnimation(caminar);
            posiHuitzi += velocity;

            // DISTANCIA ENTRE ENEMIGO(RITCHER) Y LOBITO
            playerDistance = player.posicion.X - posiHuitzi.X;

            //ataques cuando está en el piso
            ataquesAleatorios();

            //
            siguiendo = false;
            piso = false;
            caminando = false;

            jefeRect = new Rectangle((int)posiHuitzi.X, (int)posiHuitzi.Y - 40, hitText.Width, hitText.Height);

            if (prueba)
            {
                timeEsperaCabeza += CONTADOR_SUMATORIA;
                if (timeEsperaCabeza > 100)
                {
                    ataqueFuerte = true;
                    lanzarHeads = false;
                    timeEsperaCabeza = 0;
                    prueba = false;
                }

            }

            if (prueba2)
            {
                timeEsperaCabeza += CONTADOR_SUMATORIA;
                if (timeEsperaCabeza > 50)
                {
                    devastador = true;
                    lanzarHeads = false;
                    timeEsperaCabeza = 0;
                    prueba2 = false;
                }

            }

            //Esperar que se liberen todas las cabezas.
            /*  if (health.vida < 100)
              {
                  timeEsperaCabeza += CONTADOR_SUMATORIA;


                  if (timeEsperaCabeza > 20)
                  {
                      ataqueFuerte = true;
                      lanzarHeads = false;
                      timeEsperaCabeza = 0;
                  }
            }
              */


            //si está en el suelo no lanza cabezas
            if (lanzarHeads)
            {
                //Update Cabezas
                UpdateHeads(posiHuitzi);
            }

            foreach (Cabezas cabeza in cabezas)
            {
                cabeza.Update();
                player.Update(cabeza);
            }

            acciones(player);

            // health.Update(player);

            //ball.Update();

        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;

            if (flipeado == false)
                flip = SpriteEffects.None;
            else if (flipeado == true)
                flip = SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(hitText, jefeRect, Color.Blue);
            animacHuitzi.Draw(gameTime, spriteBatch, posiHuitzi, flip);


            foreach (Cabezas cabeza in cabezas)
            {
                cabeza.Draw(gameTime, spriteBatch);
            }

            fuego.Draw(gameTime, spriteBatch);

            // health.Draw(gameTime, spriteBatch);

            //ball.Draw(gameTime, spriteBatch);
        }

        public void ataquesAleatorios()
        {
            #region "Movimientos Aleatorios Medios"

            if (piso && !ataqueFuerte)
            {
                timeCountRandom += CONTADOR_SUMATORIA;

                if (!jefeDie)
                {
                    if (timeCountRandom >= 70)
                    {
                        aleat = r.NextDouble();

                        if (aleat <= 0.50)
                        {
                            cattack = true;
                        }
                        else if (aleat > 0.50)
                        {
                            backflip = true;
                        }

                        timeCountRandom = 0;
                    }
                }
            }

            #endregion

            #region "Movimientos Aleatorios Fuertes"

            if (ataqueFuerte) // quitar piso posiblemente piso && 
            {
                timeCountRandom += CONTADOR_SUMATORIA;

                if (!jefeDie)
                {
                    if (timeCountRandom >= 20)
                    {
                        aleat = r.NextDouble();

                        if (aleat <= 0.89)
                        {
                            ataqueGolpes = true;
                        }
                        else if (aleat > 0.11)
                        {
                            // atack=true;
                            ataqueGolpes = true;
                        }

                        timeCountRandom = 0;
                    }
                }
            }

            #endregion
        }


        // ++++++++++++++++++++++++++++    ACCIONES A REALIZAR    ++++++++++++++++++++++++++++++++++++++++  //
        public void acciones(Player player)
        {
            //      ***SELECCION ACCIONES***      //

            #region "Entorno"



            // AL NO ESTAR EN EL PISO
            if (posiHuitzi.Y <= PISO_HUIT)
            {
                velocity.Y = 4f;
            }

                // CUANDO YA ESTÁS SOBRE EL PISO "Y" NO SE MUEVE
            else
            {
                velocity.Y = 0f;
                piso = true;
                salto = false;
            }
            #endregion

            #region "Buscar Posición del Jugador(Ritcher)"

            if (piso && !jefeDie && !cattack && !atack)
            {
                siguiendo = true;
                caminando = true;


                //flip para saber tu orientacion
                if (posiHuitzi.X < player.posicion.X)
                {
                    flipeado = false;
                }

                else if (posiHuitzi.X > player.posicion.X)
                {
                    flipeado = true;
                }
            }

            #endregion

            #region "Salto Normal"

            if (salto)
            {
                float i = 2f;
                velocity.Y += velValor * i;
            }

            if (!salto)
            {
                velocity.Y = 0f;
            }

            #endregion

            #region "Flip"

            if (flip)
            {
                animacHuitzi.PlayAnimation(saltar);
                flip = false;
            }

            #endregion

            #region "Muerte"

            if (jefeDie)
            {
                animacHuitzi.PlayAnimation(dies);
                velocity.X = 0;
            }

            #endregion

            #region "Backflip"

            if (backflip)
            {
                cattack = false;

                timeCounter += CONTADOR_SUMATORIA;

                posiHuitzi.Y -= 6f;
                velocity.Y = -0.001f;
                velocity.X = -0.01f;

                animacHuitzi.PlayAnimation(bflip);
                salto = true;

                if (!flipeado)
                {
                    posiHuitzi.X -= RESTSUMA_BACKFLIP;
                }

                if (flipeado)
                {
                    posiHuitzi.X += RESTSUMA_BACKFLIP;
                }

                if (timeCounter >= 1.8)
                {
                    backflip = false;
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Ataque"

            if (atack)
            {
                caminando = false;
                timeCounter += CONTADOR_SUMATORIA;

                velocity.Y = -0.001f;
                velocity.X = 0f;

                animacHuitzi.PlayAnimation(atacar);

                if (timeCounter > 11)
                {
                    atack = false;
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Ataque Abajo"

            if (cattack)
            {
                timeCounter += CONTADOR_SUMATORIA;

                velocity.X = 0f;

                animacHuitzi.PlayAnimation(cAtk);

                if (timeCounter >= 11)
                {
                    cattack = false;
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Ataque Golpes"

            if (ataqueGolpes && !jefeDie)
            {
                siguiendo = false;

                switch (ataqueDir)
                {

                    case 1:

                        timeVuelta += 0.25F;
                        if (flipeado)
                        {
                            flipeado = false;
                            backflip = true;
                        }

                        if (timeVuelta > 3)
                        {
                            backflip = false;
                            salto = true;
                        }

                        if (timeVuelta > 18)
                        {
                            flipeado = true;
                        }

                        posiHuitzi.X -= 0.5F;

                        repetir = true;

                        if (posiHuitzi.X <= 15174 || jefeGetHit && posiHuitzi.X >= 15456 && posiHuitzi.X <= 15466)//15474
                        {
                            timeVuelta = 0;
                            Golpes = 0;
                            ataqueDir = 2;
                        }

                        break;

                    case 2:

                        if (flipeado)
                        {
                            flipeado = false;
                        }
                        posiHuitzi.X += 0.5F;

                        repetir = true;

                        if (posiHuitzi.X >= 15474)
                        {
                            timeVuelta += 0.25F;

                            if (!flipeado)
                            {
                                subirHuit = r.Next(4, 7);
                                flipeado = true;
                                backflip = true;
                            }
                            if (timeVuelta > subirHuit)
                            {
                                ataqueDir = 0;
                                backflip = false;
                                salto = false;
                                ataqueGolpes = false;
                                ataqueFuerte = false;
                                timeVuelta = 0;
                                cattack = true;
                                ataqueDir = 3;
                                //////////quitar///////
                                prueba2 = true;
                            }
                        }

                        break;

                    case 3:
                        break;

                }

                if (repetir && Golpes <= 3)
                {
                    timeGolpes += 0.25F;

                    if (timeGolpes >= 45)
                    {
                        atack = true;
                        Golpes++;
                        timeGolpes = 0;
                    }
                    repetir = false;
                }
            }

            #endregion

            #region "Devastador"

            if (devastador)
            {
                timeCounter += CONTADOR_SUMATORIA;
                fuego.Update(player);
                player.Update(fuego);

                if (timeCounter >= 245)
                {
                    devastador = false; 
                    timeCounter = 0;
                }
            }

            #endregion

            #region "Ser Atacado"

            if (jefeGetHit)//&&!atack
            {
                atack = false;
                //backflip = false;

                timeCounter += CONTADOR_SUMATORIA;

                posiHuitzi.Y -= 21f;
                velocity.Y = -0.85f;

                animacHuitzi.PlayAnimation(hurts);
                salto = true;

                if (!flipeado)
                {
                    posiHuitzi.X -= RESTARX_DAÑO;
                }

                if (flipeado)
                {
                    posiHuitzi.X += RESTARX_DAÑO;
                }

                if (timeCounter >= 1)
                {
                    jefeGetHit = false;
                    timeCounter = 0;
                }



            }

            #endregion

            // ** ACCIÓN DE CAMINAR ** //
            if (caminando)
            {
                animacHuitzi.PlayAnimation(caminar);
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
                    else if (playerDistance <= 10)
                    {
                        atack = true;
                    }


                }
                siguiendo = false;

            }
            if (!siguiendo && piso && ataqueGolpes) { velocity.X = 0f; }

            //Valida si hay cabezas y si ya se saliron de pantalla o si dejaron de botar
            if (cabezas.Count > 0)
            {
                for (int i = 0; i < cabezas.Count; i++)
                {
                    if ((cabezas[i].enemPosicion.X) + 15 < 14100 || cabezas[i].mFin == true)
                    {
                        cabezas.Remove(cabezas[i]);
                    }
                }
            }


        }



    }

}
