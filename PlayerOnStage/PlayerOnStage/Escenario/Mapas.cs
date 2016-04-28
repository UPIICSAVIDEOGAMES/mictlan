using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PlayerOnStage
{
    class Mapas
    {   
        Mapeador map;  
       
        public Mapeador generar(int [,] nivel){
            map = new Mapeador();

            map.Generate(nivel, 64);

            return map;
        }

    }
}
