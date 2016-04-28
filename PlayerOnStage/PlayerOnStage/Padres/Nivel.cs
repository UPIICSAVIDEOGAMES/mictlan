using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PlayerOnStage
{
    class Nivel
    {


        public bool banderaJefe;
        public Jefe jefe;
        protected Mapeador map;
        protected Mapas nivel;
        protected Texture2D backgroundTexture;
        protected Song cancionNivel;
        protected ContentManager Content;
        protected GraphicsDevice device;
        protected int[,] fondo;
        

        public Nivel()
        {

            nivel = new Mapas();
        }
        public Mapeador getMap()
        {
            return this.map;
        }

        protected void crearNivel(int[,] mapa)
        {
            
            
           
            map = nivel.generar(mapa);


           // atk = Content.Load<SoundEffect>("Attack");
        }
   }

}
