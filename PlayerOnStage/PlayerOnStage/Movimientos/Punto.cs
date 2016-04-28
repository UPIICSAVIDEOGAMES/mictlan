using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerOnStage
{
    class Punto
    {
        private float x , y;

        public Punto(float x1,float y1) {

            SetX(x1);
            SetY(y1);
        }

        public Punto() {
            x = 0;
            y = 0;
        }


        public void SetX(float x) { 
        
            this.x = x;
        }

        public float GetX() {
            return this.x;
        }

        public void SetY(float y)
        {

            this.y = y;
        }

        public float GetY()
        {
            return this.y;
        }


    }
}
