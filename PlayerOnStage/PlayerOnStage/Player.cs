using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace PlayerOnStage
{
    class Player
    {
        ContentManager Content;

        HUD HUD;

        List<Proyectil> flechas;

        KeyboardState tecla_actual;
        KeyboardState tecla_anterior;

        int distanciaLanzado = 0;
        int contadorIzquierda = 0;
        int contadorDerecha = 0;
        int temporizadorBarrida = 0;
        int temporizadorDaño = 0;
        int temporizadorParaguas = 0;
        int temporizadorArco = 0;
        int temporizadorGolpe = 0;

        Animacion_Textura animacionPlayer;
        Leer_Textura Agachar;
        Leer_Textura Ataque_Arco;
        Leer_Textura Ataque_Paraguas;
        Leer_Textura Ataque_Paraguas_Agachado;
        Leer_Textura BackFlip;
        Leer_Textura Barrida;
        Leer_Textura Caida;
        Leer_Textura Correr;
        Leer_Textura Golpe;
        Leer_Textura Herido;
        Leer_Textura Inactivo;
        Leer_Textura Muerte;
        Leer_Textura Paralizado;
        Leer_Textura Saltar;

        public bool muerteBool;
        public bool paralizadoBool;
        bool agacharBool;
        bool atacadoBool;
        bool backflipBool;
        bool barridaBool;
        bool escudoBool;
        bool golpeBool;
        bool mostrar_golpe;
        bool paracaidasBool;
       public bool saltarBool;
        bool ataque_arco;
        bool ataque_paraguas;
        bool mostrar_paraguas;
        bool voltearSprite;

        Texture2D textura_cuerpo;
        public Rectangle rectangulo_cuerpo;

        Texture2D textura_plataforma;
        public Rectangle rectangulo_plataforma;

        Texture2D textura_paraguas;
        public Rectangle rectangulo_paraguas;

        Texture2D paraguard_derecha;
        public Rectangle rect_paraguard_derecha;

        Texture2D paraguard_izquierda;
        public Rectangle rect_paraguard_izquierda;

        public Vector2 posicion = new Vector2(300, 910);//12180
        public Vector2 velocity;


        //++++      RECUERDA MODIFICAR        ++++      RECUERDA MODIFICAR      ++++      RECUERDA MODIFICAR        ++++      RECUERDA MODIFICAR

        Texture2D paraguas;
        public Rectangle rectparaguas;

        private const int ANCHO_NIVEL = 14000;
        private const int LARGO_NIVEL = 14000;

        //++++      RECUERDA MODIFICAR      ++++      RECUERDA MODIFICAR        ++++      RECUERDA MODIFICAR        ++++      RECUERDA MODIFICAR

        public void setHUD(HUD HUD)
        {
            this.HUD = HUD;
        }

        public HUD getHUD()
        {
            return HUD;
        }

        public Player(HUD HUD)
        {
            setHUD(HUD);
            flechas = new List<Proyectil>();
        }

        public void Load(ContentManager Content)
        {
            this.Content = Content;

            Agachar = new Leer_Textura(Content.Load<Texture2D>("Player/Agachar"), 55, 0.08f, false);
            Ataque_Arco = new Leer_Textura(Content.Load<Texture2D>("Player/Ataque_Arco"), 80, 0.08f, false);
            Ataque_Paraguas = new Leer_Textura(Content.Load<Texture2D>("Player/Ataque_Paraguas"), 80, 0.05f, false);
            Ataque_Paraguas_Agachado = new Leer_Textura(Content.Load<Texture2D>("Player/Ataque_Paraguas_Agachado"), 75, 0.08f, false);
            BackFlip = new Leer_Textura(Content.Load<Texture2D>("Player/Backflip"), 108, 0.10f, false);
            Barrida = new Leer_Textura(Content.Load<Texture2D>("Player/Barrida"), 115, 0.05f, false);
            Golpe = new Leer_Textura(Content.Load<Texture2D>("Player/Golpe"), 124, 0.05f, false);
            Caida = new Leer_Textura(Content.Load<Texture2D>("Player/Caida"), 65, 0.05f, false);
            Correr = new Leer_Textura(Content.Load<Texture2D>("Player/Correr"), 75, 0.08f, true);
            Herido = new Leer_Textura(Content.Load<Texture2D>("Player/Herido"), 60, 0.10f, true);
            Inactivo = new Leer_Textura(Content.Load<Texture2D>("Player/Inactivo"), 70, 0.12f, false);
            Muerte = new Leer_Textura(Content.Load<Texture2D>("Player/Muerte"), 80, 0.10f, false);
            Paralizado = new Leer_Textura(Content.Load<Texture2D>("Player/Paralizado"), 110, 0.05f, true);
            Saltar = new Leer_Textura(Content.Load<Texture2D>("Player/Saltar"), 61, 0.12f, false);

            paraguard_derecha = Content.Load<Texture2D>("Armas/Paraguas_Protector_Derecha");
            paraguard_izquierda = Content.Load<Texture2D>("Armas/Paraguas_Protector_Izquierda");
            paraguas = Content.Load<Texture2D>("HUD/Paraguas");

            textura_cuerpo = Content.Load<Texture2D>("Rectangles/RectangleHit");
            textura_plataforma = Content.Load<Texture2D>("Rectangles/RectangleStand");
            textura_paraguas = Content.Load<Texture2D>("Rectangles/RectangleWhip");
        }

        public void Update(GameTime gameTime)
        {
            tecla_actual = Keyboard.GetState();
            posicion += velocity;
            velocity.X = 0f;

            agacharBool = false;
            paracaidasBool = false;
            escudoBool = false;
            mostrar_paraguas = false;
            mostrar_golpe = false;

            Input(gameTime);
            Acciones(gameTime);
            Rectangulo_Colision(gameTime);

            tecla_anterior = tecla_actual;

            #region++++++++++++++++++++++++      GRAVEDAD     ++++++++++++++++++++++++++++++++

            if (paracaidasBool == false)
                velocity.Y += 0.35f * 1;
            else
                velocity.Y += 0.1f * 1;

            #endregion

            #region++++++++++++++++++++++++      FLECHAS     ++++++++++++++++++++++++++++++++

            //      MANEJO DE FLECHAS (ELIMINAR DE MEMORIA)
            if (flechas.Count > 0)
            {
                //Sacarlo cuando este en los limites de la pantalla
                for (int i = 0; i < flechas.Count; i++)
                {
                    //Aqui poner cuando salga del ancho del nivel o cuando toque al enemigo
                    if (flechas[i].Posicion.X < 1)
                    {
                        flechas.Remove(flechas[i]);
                    }
                }
                foreach (Proyectil flecha in flechas)
                {
                    flecha.Update();
                }
            }

            #endregion
        }

        public void Update(Jefe jefe)
        {
             int tipo_daño_jefe = jefe.jefe_tipo_daño, factor_daño_jefe = jefe.jefe_factor_daño;
            
           #region INTERSECTAR CON JEFE
            
           if (rectangulo_cuerpo.Intersects(jefe.jefeRect) && HUD.rect_HP_Player.Width >= 0 && atacadoBool == false && barridaBool == false && backflipBool == false)
           {
               HUD.rect_HP_Player.Width -= factor_daño_jefe;
               atacadoBool = true;

               switch (tipo_daño_jefe)
               {
                   case 0:
                       distanciaLanzado = 10;
                       break;

                   case 1:
                       distanciaLanzado = 20;
                       break;

                   case 2:
                       distanciaLanzado = 30;
                       break;
                   case 3:
                       atacadoBool = false;
                       break;
               }

           }

           #endregion

         
               /*
           else
               heridoBool = false;*/
            
        }

        public void Update(Enemigo enemigo)
        {
            int tipo_daño_enemigo = enemigo.enemigo_tipo_daño, factor_daño_enemigo = enemigo.enemigo_factor_daño;

           
            #region INTERSECTAR CON ENEMIGO

            if (rectangulo_cuerpo.Intersects(enemigo.enemigoRect) && HUD.rect_HP_Player.Width >= 0 && atacadoBool == false && barridaBool == false && backflipBool == false)
            {
                HUD.rect_HP_Player.Width -= factor_daño_enemigo;
                atacadoBool = true;

                switch (tipo_daño_enemigo)
                {
                    case 0:
                        distanciaLanzado = 7;
                        break;

                    case 1:
                        distanciaLanzado = 14;
                        break;

                    case 2:
                        distanciaLanzado = 21;
                        break;
                }

            }

            #endregion

            /*
           else
               heridoBool = false;*/

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;

            if (voltearSprite == false)
                flip = SpriteEffects.None;
            else if (voltearSprite == true)
                flip = SpriteEffects.FlipHorizontally;

            animacionPlayer.Draw(gameTime, spriteBatch, posicion, flip);

            foreach (Proyectil flecha in flechas)
            {
                flecha.Draw(gameTime, spriteBatch);
            }

            //++++      ELIMINAR        ++++        ELIMINAR        ++++        ELIMINAR        ++++        ELIMINAR        ++++
            spriteBatch.Draw(textura_paraguas,rectangulo_paraguas, Color.White);
            spriteBatch.Draw(paraguard_derecha, rect_paraguard_derecha, Color.White);
            spriteBatch.Draw(paraguard_izquierda, rect_paraguard_izquierda, Color.White);
            spriteBatch.Draw(paraguas, rectparaguas, Color.White);
            //++++      ELIMINAR        ++++        ELIMINAR        ++++        ELIMINAR        ++++        ELIMINAR        ++++
        }

        //+++++++++++++++++++++++++++    ACCIONES DEL TECLADO    +++++++++++++++++++++++++++++++++++++++
        public void Input(GameTime gameTime)
        {
            #region      AVANZAR DERECHA & IZQUIERDA

            if (atacadoBool == false && muerteBool == false && barridaBool == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && backflipBool == false)
                {
                    velocity.X = 6f;
                    voltearSprite = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && backflipBool == false)
                {
                    velocity.X = -6f;
                    voltearSprite = true;
                }
            }

            #endregion

            #region      SALTO NORMAL & BACKFLIP

            if (tecla_actual.IsKeyDown(Keys.Z) && tecla_anterior.IsKeyUp(Keys.Z) && saltarBool == false && tecla_anterior.IsKeyUp(Keys.Down) && paralizadoBool == false)
            {
                if (tecla_actual.IsKeyDown(Keys.Up) && saltarBool == false)
                {
                    posicion.Y -= 18f;
                    velocity.Y = -9f;

                    saltarBool = true;
                    backflipBool = true;
                }

                else
                {
                    posicion.Y -= 15f;
                    velocity.Y = -8f;
                    saltarBool = true;
                }
            }

            #endregion

            #region      ATAQUES && GOLPES

            if (tecla_actual.IsKeyDown(Keys.A) && tecla_anterior.IsKeyUp(Keys.A))
            {
                if (HUD.arma == true)
                    ataque_arco = true;
                else
                    ataque_paraguas = true;
            }

            if (tecla_actual.IsKeyDown(Keys.X) && tecla_anterior.IsKeyUp(Keys.X))
            {
                golpeBool = true;
                mostrar_golpe = true;
            }

            #endregion

            #region AGACHARSE & BARRIDA

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && saltarBool == false)
            {
                velocity.X = 0f;
                agacharBool = true;

                if (tecla_actual.IsKeyDown(Keys.X) && tecla_anterior.IsKeyUp(Keys.X) && velocity.Y == 0)
                {
                    barridaBool = true;
                }
            }

            #endregion

            #region      PARALIZADO

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                paralizadoBool = true;
            }

            if (paralizadoBool == true)
            {
                velocity.X = 0;

                if (tecla_actual.IsKeyDown(Keys.Right) && tecla_anterior.IsKeyUp(Keys.Right))
                    contadorDerecha++;

                if (tecla_actual.IsKeyDown(Keys.Left) && tecla_anterior.IsKeyUp(Keys.Left))
                    contadorIzquierda++;

                if (contadorDerecha >= 5 && contadorIzquierda >= 5)
                {
                    paralizadoBool = false;
                    contadorDerecha = 0;
                    contadorIzquierda = 0;
                }
            }

            #endregion

            #region      USO DE ARMA SELECCIONADA EN HUD

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (saltarBool == true && HUD.arma == false && backflipBool == false && paralizadoBool == false)
                {
                    paracaidasBool = true;
                }

                else if (HUD.arma == true)
                {
                    //      EJECUTAR LA MIRA :(
                }

                else if (HUD.arma == false && saltarBool == false)
                {
                    escudoBool = true;
                }
            }

            #endregion

        }

        // ++++++++++++++++++++++++++++    ACCIONES A MOSTRAR EN EL SPRITE    ++++++++++++++++++++++++++++++++++++++++
        public void Acciones(GameTime gameTime)
        {
            #region INACTIVO, CORRER & CAIDA

            if (velocity.X == 0f && velocity.Y == 0f && paralizadoBool == false && barridaBool == false && HUD.HP_Player.Width > 0 && agacharBool == false && ataque_arco == false && ataque_paraguas == false && golpeBool == false)
                animacionPlayer.PlayAnimation(Inactivo);

            if (velocity.X != 0f && saltarBool == false && backflipBool == false && HUD.HP_Player.Width > 0 && ataque_arco == false && ataque_paraguas == false && golpeBool == false)
                animacionPlayer.PlayAnimation(Correr);

            if (velocity.Y >= 1f && saltarBool == false && backflipBool == false && HUD.HP_Player.Width >= 0 && ataque_arco == false && ataque_paraguas == false && paralizadoBool == false && golpeBool == false)
                animacionPlayer.PlayAnimation(Caida);

            #endregion

            #region AGACHARSE, SALTAR & PARALIZADO

            //      AGACHARSE
            if (agacharBool == true && barridaBool == false && paralizadoBool == false && ataque_paraguas == false)
            {
                animacionPlayer.PlayAnimation(Agachar);
            }

            //      SALTAR
            if (saltarBool == true && backflipBool == false && ataque_arco == false & paralizadoBool == false && ataque_paraguas == false && golpeBool == false)
            {
                animacionPlayer.PlayAnimation(Saltar);
            }

            //      PARALIZADO
            if (paralizadoBool == true)
            {
                animacionPlayer.PlayAnimation(Paralizado);
                velocity.X = 0;
            }

            #endregion

            #region      BACKFLIP

            if (backflipBool == true && paralizadoBool == false)
            {
                animacionPlayer.PlayAnimation(BackFlip);

                if (voltearSprite == false)
                    posicion.X -= 6.7f;
                else
                    posicion.X += 6.7f;
            }

            #endregion

            #region      MORIR

            if (HUD.rect_HP_Player.Width <= 0)
            {
                animacionPlayer.PlayAnimation(Muerte);
                velocity.X = 0f;
                posicion.Y -= 1f;
                velocity.Y = -1f;
                muerteBool = true;
            }

            #endregion

            #region      BARRIDA

            if (barridaBool == true)
            {
                temporizadorBarrida += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                golpeBool = false;
                ataque_paraguas = false;
                animacionPlayer.PlayAnimation(Barrida);

                if (temporizadorBarrida <= 300)
                {
                    if (voltearSprite == false)
                        velocity.X = 15f;
                    else
                        velocity.X = -15f;
                }

                else
                {
                    barridaBool = false;
                    temporizadorBarrida = 0;
                }
            }

            #endregion

            #region      ATAQUE CON ARCO

            if (ataque_arco == true && agacharBool == false && paralizadoBool == false)
            {
                temporizadorArco += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (temporizadorArco <= 400)
                {
                    if (velocity.Y == 0)
                    {
                        animacionPlayer.PlayAnimation(Ataque_Arco);
                        velocity.X = 0;
                    }

                    else
                        animacionPlayer.PlayAnimation(Ataque_Paraguas);
                }

                else
                {
                    Flechas();
                    ataque_arco = false;
                    temporizadorArco = 0;
                }
            }

            #endregion

            #region      ATAQUE CON PARAGUAS

            if (ataque_paraguas == true && backflipBool == false)
            {
                temporizadorParaguas += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (temporizadorParaguas <= 200)
                {
                    if (agacharBool == true && barridaBool == false)
                        animacionPlayer.PlayAnimation(Ataque_Paraguas_Agachado);

                    else if (velocity.Y == 0)
                    {
                        animacionPlayer.PlayAnimation(Ataque_Paraguas);
                        velocity.X = 0;
                    }

                    else
                        animacionPlayer.PlayAnimation(Ataque_Paraguas);
                }

                else
                {
                    mostrar_paraguas = true;
                    ataque_paraguas = false;
                    temporizadorParaguas = 0;
                }
            }

            #endregion

            #region      GOLPE

            if (golpeBool == true && backflipBool == false && ataque_paraguas == false && ataque_arco == false)
            {
                temporizadorGolpe += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (temporizadorGolpe <= 400)
                {
                    animacionPlayer.PlayAnimation(Golpe);
                }

                else
                {
                    golpeBool = false;
                    temporizadorGolpe = 0;
                }
            }

            #endregion

            #region      SER ATACADO

            if (atacadoBool == true)
            {
                temporizadorDaño += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                animacionPlayer.PlayAnimation(Herido);

                posicion.Y -= 2f;
                velocity.Y = -2f;

                if (temporizadorDaño <= 300)
                {
                    if (voltearSprite == false)
                    {
                        velocity.X = -distanciaLanzado;
                    }
                    else
                    {
                        velocity.X = distanciaLanzado;
                    }
                }

                else
                {
                    atacadoBool = false;
                    temporizadorDaño = 0;
                }
            }

            #endregion
        }

        // ++++++++++++++++++++++++++++    MANEJO DE LOS RECTANGULOS DE COLISION DEL PLAYER CONTRA LOS NPC's    ++++++++++++++++++++++++++++++++++++++++
        public void Rectangulo_Colision(GameTime gameTime)
        {
            rectangulo_plataforma = new Rectangle((int)posicion.X - 23, (int)posicion.Y - 18, textura_plataforma.Width, textura_plataforma.Height);

            #region       ATAQUE CON PARAGUAS, GOLPE & AGACHARSE

            if (agacharBool == false)
            {
                rectangulo_cuerpo = new Rectangle((int)posicion.X - 10, (int)posicion.Y - 85, textura_cuerpo.Width, textura_cuerpo.Height);

                if (mostrar_paraguas == true)
                {
                    mostrar_golpe = false;
                    if (voltearSprite == false)
                        rectangulo_paraguas = new Rectangle((int)posicion.X + 15, (int)posicion.Y - 75, textura_paraguas.Width, textura_paraguas.Height);
                    else
                        rectangulo_paraguas = new Rectangle((int)posicion.X - 115, (int)posicion.Y - 75, textura_paraguas.Width, textura_paraguas.Height);
                }

                else if (mostrar_golpe == true)
                {
                    if (voltearSprite == false)
                        rectangulo_paraguas = new Rectangle((int)posicion.X + 15, (int)posicion.Y - 75, 40, textura_paraguas.Height + 44);
                    else
                        rectangulo_paraguas = new Rectangle((int)posicion.X - 55, (int)posicion.Y - 75, 40, textura_paraguas.Height + 44);
                }
            }

            else
            {
                rectangulo_cuerpo = new Rectangle((int)posicion.X - 10, (int)posicion.Y - 63, textura_cuerpo.Width, textura_cuerpo.Height);

                if (voltearSprite == false)
                    rectangulo_paraguas = new Rectangle((int)posicion.X + 15, (int)posicion.Y - 55, textura_paraguas.Width, textura_paraguas.Height);
                else
                    rectangulo_paraguas = new Rectangle((int)posicion.X - 115, (int)posicion.Y - 55, textura_paraguas.Width, textura_paraguas.Height);
            }

            #endregion

            #region MOSTRAR U OCULTAR PARAGUAS Y PARACAIDAS

            //      OCULTAR COLISION PARAGUAS
            if (mostrar_paraguas == false && mostrar_golpe == false)
                rectangulo_paraguas = new Rectangle(0, 0, 0, 0);

            //      PARACAIDAS
            rectparaguas = new Rectangle((int)posicion.X, (int)posicion.Y - 130, paraguas.Width, paraguas.Height);

            if (paracaidasBool == false)
                rectparaguas = new Rectangle(0, 0, 0, 0);

            #endregion

            #region      ESCUDO

            if (escudoBool == false)
            {
                rect_paraguard_derecha = new Rectangle(0, 0, 0, 0);
                rect_paraguard_izquierda = new Rectangle(0, 0, 0, 0);
            }

            else
            {
                if (agacharBool == false && voltearSprite == false)
                {
                    rect_paraguard_derecha = new Rectangle((int)posicion.X + 15, (int)posicion.Y - 85, paraguard_derecha.Width, paraguard_derecha.Height);
                    rect_paraguard_izquierda = new Rectangle(0, 0, 0, 0);
                }
                else if (agacharBool == true && voltearSprite == false)
                {
                    rect_paraguard_derecha = new Rectangle((int)posicion.X + 15, (int)posicion.Y - 65, paraguard_derecha.Width, paraguard_derecha.Height);
                    rect_paraguard_izquierda = new Rectangle(0, 0, 0, 0);
                }

                else if (agacharBool == false && voltearSprite == true)
                {
                    rect_paraguard_izquierda = new Rectangle((int)posicion.X - 75, (int)posicion.Y - 85, paraguard_derecha.Width, paraguard_derecha.Height);
                    rect_paraguard_derecha = new Rectangle(0, 0, 0, 0);
                }

                else if (agacharBool == true && voltearSprite == true)
                {
                    rect_paraguard_izquierda = new Rectangle((int)posicion.X - 75, (int)posicion.Y - 65, paraguard_derecha.Width, paraguard_derecha.Height);
                    rect_paraguard_derecha = new Rectangle(0, 0, 0, 0);
                }

            }

            #endregion

        }

        // ++++++++++++++++++++++++++++    MANEJO DE FLECHAS    ++++++++++++++++++++++++++++++++++++++++
        public void Flechas()
        {
            Proyectil flecha = new Proyectil();
            flecha.Load(this.Content);
            flecha.Flipeado = voltearSprite;

            if (voltearSprite == false)
            {
                flecha.Posicion = new Vector2(posicion.X, posicion.Y - 75);
                flecha.Velocity = new Vector2(10, 0);
                flechas.Add(flecha);
            }

            else
            {
                flecha.Posicion = new Vector2(posicion.X - 65, posicion.Y - 75);
                flecha.Velocity = new Vector2(-10, 0);
                flechas.Add(flecha);
            }
        }

        // ++++++++++++++++++++++++++++    METODO DE COLISION CON RECTANGULOS DEL NIVEL (PLATAFORMAS)    ++++++++++++++++++++++++++++++++++++++++        
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (muerteBool == false)
            {
                if (rectangulo_plataforma.TouchTopOf(newRectangle))
                {
                    rectangulo_plataforma.Y = newRectangle.Y - rectangulo_plataforma.Height;
                    velocity.Y = 0f;
                    saltarBool = false;
                    backflipBool = false;
                }

                if (rectangulo_plataforma.TouchLeftOf(newRectangle) || rectangulo_cuerpo.TouchLeftOf(newRectangle) && voltearSprite == false)
                {
                    velocity.X = 0f;
                    backflipBool = false;
                    atacadoBool = false;
                }

                if (rectangulo_plataforma.TouchRightOf(newRectangle) || rectangulo_cuerpo.TouchRightOf(newRectangle) && voltearSprite == true)
                {
                    velocity.X = 0f;
                    backflipBool = false;
                    atacadoBool = false;
                }

                if (rectangulo_plataforma.TouchBottomOf(newRectangle) || rectangulo_cuerpo.TouchBottomOf(newRectangle))
                    velocity.Y = 1f;

                if (posicion.X < 0) posicion.X = 0;
                if (posicion.X > xOffset - rectangulo_plataforma.Width) posicion.X = xOffset - rectangulo_plataforma.Width;
                if (posicion.Y < 0) velocity.Y = 1f;
                if (posicion.Y > yOffset - rectangulo_plataforma.Height) posicion.Y = yOffset - rectangulo_plataforma.Height;
            }
        }
    }    
}