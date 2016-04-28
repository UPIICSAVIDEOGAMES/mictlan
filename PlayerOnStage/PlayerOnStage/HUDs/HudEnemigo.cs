using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
namespace PlayerOnStage
{
    class HudEnemigo
    {
        int vida = 1;
        Enemigo enemigo;

        public HudEnemigo(Enemigo enemigo)
        {
            this.enemigo = enemigo;
        }
     
        public void Update(Player jugador)
        {

            if (enemigo.enemigoRect.Intersects(jugador.rectangulo_paraguas))
            {
                if (vida >= 0)
                {
                    vida -= 5;
                    enemigo.enemGetHit = true;
                }

                else if (vida <= 0)
                {
                    enemigo.enemDie = true;
                }
            }
            if (jugador.flechas.Count > 1)
            {
                foreach (Proyectil proyectil in jugador.flechas)
                {
                    if (proyectil.rectangulo_flecha.Intersects(enemigo.enemigoRect))
                    {

                        vida -= 1;
                        enemigo.enemGetHit = true;
                    }
                    if (vida <= 0)
                    {
                        enemigo.enemDie = true;
                    }
                }
            }

        }
    
    }
}
