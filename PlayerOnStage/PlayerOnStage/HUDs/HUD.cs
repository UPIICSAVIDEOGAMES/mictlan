using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace PlayerOnStage
{
    class HUD
    {
        //+++++++++++++++++++++++++++++     ATRIBUTOS PARA DEFINIR LA PANTALLA COMPLETA     ++++++++++++++++++++++++++++
        GraphicsDevice pantalla;
        int pantallaAncho, pantallaAlto;
        Player jugador;

        //+++++++++++++++++++++++++++++     ATRIBUTOS DE TEXTURA, VECTORES DE LOCALIZACIÓN Y RECTANGULOS (MARCOS) QUE SOSTENDRÁN LA IMAGEN      ++++++++++++++++++++++++++++
        public Texture2D HP_Player, MP_Player, HP_Boss, CalendarioA, CalendarioB, Mascara, Paraguas, Arco, Mira;
        public Vector2 pos_HP_Player, pos_MP_Player, pos_HP_Boss, posCalendarioA, posCalendarioB, origenCalendarioB, pos_Mascara, pos_Paraguas, pos_Arco, posMira, origenMira;
        public Rectangle rect_HP_Player, rect_MP_Player, rect_HP_Boss, rectCalendarioA, rectCalendarioB, rect_Mascara, rect_Paraguas, rect_Arco, rectMira;

        //+++++++++++++++++++++++++++++     ATRIBUTOS DE PARA DEFINIR QUE SE DEBE PRESIONAR EL BOTON MULTIPLES VECES (NO ADMITE DEJAR PRESIONADO BOTON)     ++++++++++++++++++++++++++++
        KeyboardState teclaActual;
        KeyboardState teclaAnterior;

        //+++++++++++++++++++++++++++++     ATRIBUTO DE ROTACION, ROTACION DERECHA O IZQUIERDA Y POWER UP SELECCIONADO      ++++++++++++++++++++++++++++
        float rotacionCalendario, rotacionMira;
        bool rotadorCalendario, rotadorMira;
        int powerUp;


        public bool muestra_HP_Boss = false;  //   MOSTRARÁ U OCULTARÁ LA BARRA DE SANGRE DEL BOSS. ES PUBLICÓ PORQUÉ DEPENDERÁ DEL NIVEL.

        public bool arma = false;   //  SELECCIONARÁ EL ARMA (PARAGUAS O ARCO)


        SpriteFont muestra;     //  UTILIZADO PARA MOSTRAR PROCESO EN PANTALLA; NO ES NECESARIO SU USO. PUEDE BORRARSE

        public HUD()
        {

        }
        public void Load(ContentManager Content, GraphicsDevice graphics)
        {
            pantalla = graphics;     //      AQUI SE LE PASAN LOS PARAMETROS DE GRAPHICS A DEV PARA
            muestra = Content.Load<SpriteFont>("MuestraDatos");     //  MOSTRAR PROCESO. SE PUEDE BORRAR

            //+++++++++++++++++++++++++++++     AJUSTAR PARAMETROS DE PANTALLA PARA SU USO EN FULL SCREEN      ++++++++++++++++++++++++++++
            pantallaAlto = pantalla.PresentationParameters.BackBufferHeight;
            pantallaAncho = pantalla.PresentationParameters.BackBufferWidth;

            //+++++++++++++++++++++++++++++     DEFINIR IMAGEN, POSICION Y RECTANGULO EN PANTALLA      ++++++++++++++++++++++++++++
            HP_Player = Content.Load<Texture2D>("HUD/HP_Player");
            pos_HP_Player = new Vector2(65, 20);
            rect_HP_Player = new Rectangle(0, 0, pantallaAncho / 3, HP_Player.Height);

            MP_Player = Content.Load<Texture2D>("HUD/MP_Player");
            pos_MP_Player = new Vector2(65, 50);
            rect_MP_Player = new Rectangle(0, 0, pantallaAncho / 3, MP_Player.Height);

            HP_Boss = Content.Load<Texture2D>("HUD/HP_Boss");
            pos_HP_Boss = new Vector2(65, pantallaAlto - 50);
            rect_HP_Boss = new Rectangle(0, 0, pantallaAncho - 130, HP_Boss.Height);

            CalendarioA = Content.Load<Texture2D>("HUD/CalendarioA");
            posCalendarioA = new Vector2(pantallaAncho - (CalendarioA.Width / 2), -(CalendarioA.Height / 2));
            rectCalendarioA = new Rectangle(0, 0, CalendarioA.Width, CalendarioA.Height);

            CalendarioB = Content.Load<Texture2D>("HUD/CalendarioB");
            posCalendarioB = new Vector2(pantallaAncho, pantallaAlto / 200);
            rectCalendarioB = new Rectangle(0, 0, CalendarioB.Width, CalendarioB.Height);

            Mira = Content.Load<Texture2D>("HUD/Mira");
            posMira = new Vector2(pantallaAncho, pantallaAlto / 200);
            rectMira = new Rectangle(0, 0, Mira.Width, Mira.Height);

            Mascara = Content.Load<Texture2D>("HUD/Mascara");
            pos_Mascara = new Vector2(5, 13);
            rect_Mascara = new Rectangle(0, 0, Mascara.Width, Mascara.Height);

            Arco = Content.Load<Texture2D>("HUD/Arco");
            pos_Arco = new Vector2(5, 13);
            rect_Arco = new Rectangle(0, 0, Arco.Width, Arco.Height);

            Paraguas = Content.Load<Texture2D>("HUD/Paraguas");
            pos_Paraguas = new Vector2(5, 13);
            rect_Paraguas = new Rectangle(0, 0, Paraguas.Width, Paraguas.Height);

        }

        public void Update()
        {
            teclaActual = Keyboard.GetState();      //  TECLA PRESIONADA
            origenCalendarioB = new Vector2(rectCalendarioB.Width / 2, rectCalendarioB.Height / 2);     //  DEFINIR EL CENTRO DE LA IMAGEN PARA INICAR SU ROTACIÓN; DEBE IR EN UPDATE Y NO EN LOAD DEBIDO A QUE SE DEBE ACTUALIZAR A CADA MOMENTO



            //   +++++++++++++++   ELIMINAR
            if (teclaActual.IsKeyDown(Keys.D9) && teclaAnterior.IsKeyUp(Keys.D9) && rect_HP_Player.Width >= 0)
                rect_HP_Player.Width -= 7;
            // +++++++++++++++           


            //+++++++++++++++++++++++++++++     DELIMITAR LA SELECCION DEL POWER-UP; EL CONTEO INICIA DESDE 0      ++++++++++++++++++++++++++++
            if (powerUp > 5)
            {
                powerUp = 5;
            }

            else if (powerUp < 0)
            {
                powerUp = 0;
            }

            //+++++++++++++++++++++++++++++     SELECCION DEL SIGUIENTE O DEL ANTERIOR POWER-UP      ++++++++++++++++++++++++++++
            if (teclaActual.IsKeyDown(Keys.R) && teclaAnterior.IsKeyUp(Keys.R))
            {
                rotadorCalendario = true;
                powerUp++;
            }

            else if (teclaActual.IsKeyDown(Keys.W) && teclaAnterior.IsKeyUp(Keys.W) && powerUp <= 5)
            {
                rotadorCalendario = false;
                powerUp--;
            }

            //+++++++++++++++++++++++++++++     DELIMITAR CAMPOS DEL ROTADOR PARA EL SIGUIENTE POWER UP, PARA QUE SE VEA FLUIDA LA SELECCION DEL ITEM      ++++++++++++++++++++++++++++
            if (rotadorCalendario == true && powerUp == 1 && rotacionCalendario <= 1)
            {
                rotacionCalendario += .1f;
            }

            else if (rotadorCalendario == true && powerUp == 2 && rotacionCalendario <= 2)
            {
                rotacionCalendario += .1f;
            }

            else if (rotadorCalendario == true && powerUp == 3 && rotacionCalendario <= 3)
            {
                rotacionCalendario += .1f;
            }

            else if (rotadorCalendario == true && powerUp == 4 && rotacionCalendario <= 4)
            {
                rotacionCalendario += .1f;
            }

            else if (rotadorCalendario == true && powerUp == 5 && rotacionCalendario <= 5)
            {
                rotacionCalendario += .1f;
            }

            //+++++++++++++++++++++++++++++     DELIMITAR CAMPOS DEL ROTADOR PARA EL ANTERIOR POWER UP, PARA QUE SE VEA FLUIDA LA SELECCION DEL ITEM      ++++++++++++++++++++++++++++
            if (rotadorCalendario == false && powerUp == 0 && rotacionCalendario >= 0)
            {
                rotacionCalendario -= .1f;
            }

            else if (rotadorCalendario == false && powerUp == 1 && rotacionCalendario >= 1)
            {
                rotacionCalendario -= .1f;
            }

            else if (rotadorCalendario == false && powerUp == 2 && rotacionCalendario >= 2)
            {
                rotacionCalendario -= .1f;
            }

            else if (rotadorCalendario == false && powerUp == 3 && rotacionCalendario >= 3)
            {
                rotacionCalendario -= .1f;
            }

            else if (rotadorCalendario == false && powerUp == 4 && rotacionCalendario >= 4)
            {
                rotacionCalendario -= .1f;
            }

            else if (rotadorCalendario == false && powerUp == 5 && rotacionCalendario >= 5)
            {
                rotacionCalendario -= .1f;
            }

            //+++++++++++++++++++++++++++++     ACCIONES AL SELECCIONAR EL POWER UP (PARA TODAS APLICA LO MISMO)     ++++++++++++++++++++++++++++
            switch (powerUp)
            {
                case 0:     // EN ESTE CASO, EL POWER UP ES EL "RECUPERADOR DE SANGRE"
                    if (teclaActual.IsKeyDown(Keys.E) && rect_MP_Player.Width > 0)      //  FUNCIONARÁ SI SE PRESIONA LA TECLA Y LA BARRA DE MP NO ESTA VACIA
                    {
                        //      ACCIONES A REALIZAR
                        rect_MP_Player.Width -= 1;  //  BARRA DE MP BAJARA
                        rect_HP_Player.Width += 1;  //  BARRA DE HP SUBIRÁ
                    }
                    break;

                case 1:
                    if (teclaActual.IsKeyDown(Keys.E) && teclaAnterior.IsKeyUp(Keys.E) && rect_MP_Player.Width > 0)
                        rect_MP_Player.Width -= 4;
                    break;

                case 2:
                    if (teclaActual.IsKeyDown(Keys.E) && teclaAnterior.IsKeyUp(Keys.E) && rect_MP_Player.Width > 0)
                        rect_MP_Player.Width -= 6;
                    break;

                case 3:
                    if (teclaActual.IsKeyDown(Keys.E) && teclaAnterior.IsKeyUp(Keys.E) && rect_MP_Player.Width > 0)
                        rect_MP_Player.Width -= 8;
                    break;

                case 4:
                    if (teclaActual.IsKeyDown(Keys.E) && teclaAnterior.IsKeyUp(Keys.E) && rect_MP_Player.Width > 0)
                        rect_MP_Player.Width -= 10;
                    break;

                case 5:
                    if (teclaActual.IsKeyDown(Keys.E) && teclaAnterior.IsKeyUp(Keys.E) && rect_MP_Player.Width > 0)
                        rect_MP_Player.Width -= 12;
                    break;
            }

            //+++++++++++++++++++++++++++++     SELECCIONAR Y MOSTRAR EN HUD EL ARMA A UTILIZAR      ++++++++++++++++++++++++++++

            if (teclaActual.IsKeyDown(Keys.S) && teclaAnterior.IsKeyUp(Keys.S) && arma == false)
                arma = true;

            else if (teclaActual.IsKeyDown(Keys.S) && teclaAnterior.IsKeyUp(Keys.S) && arma == true)
                arma = false;


            teclaAnterior = teclaActual; //     CON ESTO, SOLO SE ACEPTARÁ UNA ACCIÓN POR CADA VEZ QUE SE PRESIONA LA TECLA
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //+++++++++++++++++++++++++++++     MOSTRAR EN PANTALLA LAS IMAGENES      ++++++++++++++++++++++++++++
            spriteBatch.Draw(HP_Player, pos_HP_Player, rect_HP_Player, Color.White);
            spriteBatch.Draw(MP_Player, pos_MP_Player, rect_MP_Player, Color.White);
            spriteBatch.Draw(CalendarioA, posCalendarioA, rectCalendarioA, Color.White);
            spriteBatch.Draw(CalendarioB, posCalendarioB, null, Color.White, rotacionCalendario, origenCalendarioB, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(Mascara, pos_Mascara, rect_Mascara, Color.White);
            spriteBatch.Draw(Mira, posMira, rectMira, Color.White);

            //+++++++++++++++++++++++++++++     SELECCIONAR EL ARMA A VISUALIZAR EN EL HUD      ++++++++++++++++++++++++++++
            if (arma == true)
                spriteBatch.Draw(Arco, pos_Arco, rect_Arco, Color.White);

            else if (arma == false)
                spriteBatch.Draw(Paraguas, pos_Paraguas, rect_Paraguas, Color.White);

            //+++++++++++++++++++++++++++++     MOSTRAR PROCESO      ++++++++++++++++++++++++++++
            spriteBatch.DrawString(muestra, "PowerUp: " + (powerUp + 1), new Vector2(350, 0), Color.White);

            //+++++++++++++++++++++++++++++     MOSTRAR HP DEL BOSS      ++++++++++++++++++++++++++++
            if (muestra_HP_Boss == true)
            {
                spriteBatch.Draw(HP_Boss, pos_HP_Boss, rect_HP_Boss, Color.White);
                spriteBatch.DrawString(muestra, "TLAKAUAK WARLOCK", new Vector2(pantallaAncho / 3, pantallaAlto - 40), Color.Red);

            }
        }

        public void isMuestraJefe(bool muestra_HP_Boss)
        {
            this.muestra_HP_Boss = muestra_HP_Boss;
        }
        public bool getMuestraJefe()
        {
            return muestra_HP_Boss;
        }

    }
}
