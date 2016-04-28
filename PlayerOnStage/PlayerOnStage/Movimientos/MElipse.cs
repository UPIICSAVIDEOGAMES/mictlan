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
    class MElipse
    {
        private Vector2 v1, v2, f1, f2, centro;
        private float a, b, c;

        #region "Getters and Setters"

        public void setV1(Vector2 v1)
        {
            this.v1 = v1;
        }

        public Vector2 getV1()
        {
            return this.v1;
        }

        public void setV2(Vector2 v2)
        {
            this.v2 = v2;
        }

        public Vector2 getV2()
        {
            return this.v2;
        }

        public void setF1(Vector2 f1)
        {
            this.f1 = f1;
        }

        public Vector2 getF1()
        {
            return this.f1;
        }

        public void setF2(Vector2 f2)
        {
            this.f2 = f2;
        }

        public Vector2 getF2()
        {
            return this.f2;
        }

        public void setCentro(Vector2 centro)
        {
            this.centro = centro;
        }

        public Vector2 getCentro()
        {
            return this.centro;
        }

        public void setA(float a)
        {
            this.a = a;
        }

        public float getA()
        {
            return this.a;
        }

        public void setB(float b)
        {
            this.b = b;
        }

        public float getB()
        {
            return this.b;
        }

        public void setC(float c)
        {
            this.c = c;
        }

        public float getC()
        {
            return this.c;
        }

        #endregion

        public void calCentro(Vector2 punto1, Vector2 punto2)
        {
            Vector2 centro = new Vector2();
            centro.X = ((punto1.X + punto2.X) / 2);
            centro.Y = ((punto1.Y - punto2.Y) / 2); // en lugar de ser + es - para arriba
            setCentro(centro);
        }

        public void calcCA()
        {
            // calculamos A
            setA(getV1().X - getCentro().X);
            //calculamos C
            setC(getF1().X - getCentro().X);
        }

        public void calcB()
        {


            setB((float)(Math.Sqrt((Math.Pow(getA(), 2) - Math.Pow(getC(), 2)))));

        }

        public Vector2 movePunto(float x)
        {

            Vector2 punto;
            float Y;

            Y = Math.Abs(((float)(Math.Sqrt(Math.Pow(getB(), 2) - ((Math.Pow(getB(), 2)) / (Math.Pow(getA(), 2))) * (Math.Pow(x - getCentro().X, 2)))) + getCentro().Y));
            
            punto = new Vector2(x, Y);

            return punto;
        }
        
    }
}
