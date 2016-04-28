using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlayerOnStage
{
    class Camara
    {
        int nivel;
        public Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
            
        }
        
        private Vector2 centre;
        private Viewport viewport;
        bool llego = false;
        public Camara(Viewport newViewport)
        {
            viewport = newViewport;
            nivel = 1;
        }
        public void setNivel(int nivel)
        {
            this.nivel = nivel;
        }
        public int getNivel()
        {
            return this.nivel;
        }

        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            switch (nivel)
            {
                case 1:

                    if (position.X <= 15350 && llego == false)
                    {
                        if (position.X < viewport.Width / 2)
                            centre.X = viewport.Width / 2;

                        else if (position.X > xOffset - (viewport.Width / 2))
                            centre.X = xOffset - (viewport.Width / 2);

                        else centre.X = position.X;


                        if (position.Y < viewport.Height / 2)
                            centre.Y = viewport.Height / 2;

                        else if (position.Y > yOffset - (viewport.Height / 2))
                            centre.Y = yOffset - (viewport.Height / 2);

                        else centre.Y = position.Y;

                        if (position.Y <= 1090)
                        {
                            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2),
                                                                              -centre.Y + 250 + (viewport.Height / 2), 0));
                        }
                        else
                        {
                            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2),
                                                                                  -centre.Y + (viewport.Height / 2), 0));
                        }

                    }

                    else
                    {
                        if (position.X >= 16119) { nivel = 2; } //esta es la condicion para que pase al siguiente nivel la camara
                        llego = true;
                        // Aqui se traba la camara
                        //para hacer que la camara se destrabe cuando mate al mictlan solo debes de poner:
                        //if(jefeDie){llego = false} 

                    }

                    break;

                case 2:
                    //Aqui es cuando esta en el segundo nivel de mictlan hara que la camara siga al jugador
                    if (position.X <= 15450)
                    {
                        if (position.X < viewport.Width / 2)
                            centre.X = viewport.Width / 2;

                        else if (position.X > xOffset - (viewport.Width / 2))
                            centre.X = xOffset - (viewport.Width / 2);

                        else centre.X = position.X;


                        if (position.Y < viewport.Height / 2)
                            centre.Y = viewport.Height / 2;

                        else if (position.Y > yOffset - (viewport.Height / 2))
                            centre.Y = yOffset - (viewport.Height / 2);

                        else centre.Y = position.Y;

                        transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2),
                                                                          -centre.Y + (viewport.Height / 2), 0));
                    }
                    else
                    {
                        //Aqui hace que la camara se quede estatica cuando llega con mictlan

                        llego = false;
                        //para hacer que la camara se destrabe cuando mate al mictlan solo debes de poner:
                        //if(jefeDie){llego = false} 

                        transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width),
                                                                        -centre.Y + (viewport.Height / 2), 0));
                    }
                    //aqui esta la camara del nivel Huitzi
                    break;

            }
        }
    }
}
