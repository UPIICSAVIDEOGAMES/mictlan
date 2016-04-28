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
    class Cabezas : Enemigo
    {
        Animacion_Textura animacionCabeza;
        Leer_Textura Cabeza;

        Texture2D cabezaText;

        Random random = new Random();
        int randomX1, randomF1, randomF2;

        private float mVY = 0;
        private double mAY = 0.2;
        private float mVX;//puede ser random
        private int mDireccion = 1; // cayendo
        public bool mFin = false;
        private bool mOutOfArea = false;
        private double mK = 0.1;
        private double delta;
        private double newVY;
        int PISO_CABEZA = 1028;

        int FACTOR_DANO = 10;

        Vector2 vertice1;
        Vector2 vertice2;///////
        Vector2 foco1;
        Vector2 foco2;
        MElipse elipse;

        public Cabezas(Vector2 posi) : base()
        {
            enemPosicion = new Vector2((posi.X) - 30, posi.Y);//aqui es donde iniciara

            mVX = random.Next(1, 3);

            CalcularDestino(posi);

            base.enemigo_factor_daño = FACTOR_DANO;

        }

        public void Load(ContentManager Content)
        {
            Cabeza = new Leer_Textura(Content.Load<Texture2D>("Rectangles/Platform"), 65, 0.12f, true);
            cabezaText = Content.Load<Texture2D>("Rectangles/RectangleDeath");

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            spriteBatch.Draw(cabezaText, enemigoRect, Color.White);
            animacionCabeza.Draw(gameTime, spriteBatch, enemPosicion, flip);

            // spriteBatch.Draw(Cabeza, new Rectangle((int)position.X, (int)position.Y, Cabeza.Width, Cabeza.Height), Color.White);

        }

        public void CalcularDestino(Vector2 posicionActual)
        {

            randomX1 = random.Next(15100, ((int)posicionActual.X) - 100);//((int)posi.X)-100
            randomF1 = random.Next(randomX1, ((int)posicionActual.X) - 100);
            randomF2 = random.Next(randomF1, ((int)posicionActual.X) - 100);

            vertice1 = new Vector2(randomX1, ((int)posicionActual.Y) + 15);
            vertice2 = new Vector2(((int)posicionActual.X) - 30, ((int)posicionActual.Y) + 1300);///////
            foco1 = new Vector2(randomF1, 600);
            foco2 = new Vector2(randomF2, 600);

            elipse = new MElipse();
            elipse.setF1(foco1);
            elipse.setF2(foco2);
            elipse.setV1(vertice1);
            elipse.setV2(vertice2);
            elipse.calCentro(vertice1, vertice2);
            elipse.calcCA();
            elipse.calcB();

        }

        public void Update()
        {

            if (enemPosicion.X <= vertice1.X)
            {
                Rebote();
            }
            else
            {
                enemPosicion = elipse.movePunto(enemPosicion.X - 1);
            }

            animacionCabeza.PlayAnimation(Cabeza);
            enemigoRect = new Rectangle((int)enemPosicion.X - 30, (int)enemPosicion.Y - 60, cabezaText.Width, cabezaText.Height);

        }

        public void Rebote()
        {

            if (mFin || mOutOfArea)
            {
                return;
            }

            enemPosicion.X -= mVX;//aqui agarra la altura que tenga la pieza

            if (mDireccion == 1)
            {

                enemPosicion.Y += mVY;

                if (enemPosicion.Y >= PISO_CABEZA)//aqui si pega con esta altura o el suelo rebota hacia arriba
                {
                    mDireccion = 2;

                    delta = (double)mVY * mK;

                    newVY = mVY - (delta + 0.01);

                    if (delta < 0.1)
                    {
                        mFin = true;
                    }

                    mVY = (float)newVY;

                }
                else
                {
                    mVY += float.Parse(mAY.ToString());//si no encuentra el suelo sigue restandole a su altura hasta que lo encuentre
                }

            }
            else if (mDireccion == 2)//aqui es cuando va de subida
            {

                enemPosicion.Y -= mVY;

                mVY -= float.Parse(mAY.ToString());

                if (mVY < 0)//aqui es cuando pierde toda la fuerza y vuelve a bajar
                {
                    mDireccion = 1;

                }

            }
        }


    }


}