using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerOnStage
{
    class Elipse
    {
        private Punto v1, v2, f1, f2, centro;
        private float a, b, c;

        #region "Getters and Setters"

        public void setV1(Punto v1)
        {
            this.v1 = v1;
        }

        public Punto getV1()
        {
            return this.v1;
        }

        public void setV2(Punto v2)
        {
            this.v2 = v2;
        }

        public Punto getV2()
        {
            return this.v2;
        }

        public void setF1(Punto f1)
        {
            this.f1 = f1;
        }

        public Punto getF1()
        {
            return this.f1;
        }

        public void setF2(Punto f2)
        {
            this.f2 = f2;
        }

        public Punto getF2()
        {
            return this.f2;
        }

        public void setCentro(Punto centro)
        {
            this.centro = centro;
        }

        public Punto getCentro()
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

        public void calCentro(Punto punto1, Punto punto2) {

            Punto centro = new Punto();
            centro.SetX((punto1.GetX() + punto2.GetX())/2);
            centro.SetY((punto1.GetY() + punto2.GetY()) / 2);
            setCentro(centro);
        }

        public void calcCA() {
            // calculamos A
            setA(getV1().GetX() - getCentro().GetX());
            //calculamos C
            setC(getF1().GetX() - getCentro().GetX());
        }

        public void calcB() {


            setB((float)(Math.Sqrt((Math.Pow(getA(), 2) - Math.Pow(getC(), 2)))));

        }

        public Punto movePunto(float x) {

            Punto punto;
            float Y;

            Y = Math.Abs(((float)(Math.Sqrt(Math.Pow(getB(), 2) - ((Math.Pow(getB(), 2)) / (Math.Pow(getA(), 2)))*(Math.Pow(x-getCentro().GetX(),2)))) + getCentro().GetY()));
            
            punto = new Punto(x,Y);

            return punto;
        }

    }
}