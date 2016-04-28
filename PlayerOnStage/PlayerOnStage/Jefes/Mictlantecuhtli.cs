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
    class Mictlantecuhtli : Jefe
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice dev;
        //esta bandera sirve para finjir que eres una estatua cuando llega el peronaje principal la primera vez
        bool banderaPrincipio;
        //Este contador es para decir en que TIEMPO_CIERRA  y TIEMPO_ZOMBIE segue el siguiente atataque 
        float timeCounterCierra;
        float timeCounterZombie;
        float timeCounterComer;
        //Este contador es para decir en que TIEMPO_CIERRA  y TIEMPO_ZOMBIE segue el siguiente atataque
        float TIEMPO_CIERRA = 50;//100=1 segundo
        float TIEMPO_ZOMBIE = 30;//100=1 segundo
        int TIEMPO_COMER = 100;//5 segundos
        List<Cierra> cierras;
        List<Zombie> zombies;
        Animacion_Textura animacionMictl;
        Leer_Textura mictSentado;
        Leer_Textura mictComiendo;
        Leer_Textura mictSilla;
        Leer_Textura mictEnfadado;
        Leer_Textura mictMandarAtaque;


        Texture2D mictText;

        public Vector2 mictlPos;

        Texture2D sillaText;
        public Rectangle sillaRect;
        public Vector2 sillaPos;
        bool llega = false;
        bool absorver = false;
        //Para mover la camara
        public bool moverCamara = false;

        public bool parado = false;
        bool regresa;
        int opcionAtaque = 1;

        //Para paralizar a player la primera vez
        public bool paralizarPlayer;
        public bool paralizaPlayerAboserviendo;
        private int POSICION_MICLAN_INICIAL = 13000;//13000
        private int POSICION_SILLA_INICIAL = 12965;//13000
        private int POSICION_INICIAL = 12965;//13000
        private int FACTOR_DANO = 50;
        private int FACTOR_DANO_ABSORBER = 300;
        private int LIMITE_TEMPLO = 12000;
        ContentManager Content;

        public Mictlantecuhtli()
            : base()
        {

            base.jefe_factor_daño = FACTOR_DANO;


            //Listas de ataques
            cierras = new List<Cierra>();
            zombies = new List<Zombie>();
            timeCounterCierra = TIEMPO_CIERRA;
            timeCounterZombie = TIEMPO_ZOMBIE;
            timeCounterComer = TIEMPO_COMER;
        }

        public void Load(ContentManager Content, GraphicsDevice dev)
        {




            //Animaciones
            mictSentado = new Leer_Textura(Content.Load<Texture2D>("Acciones/miclantechutli"), 236, 0.25f, false);
            mictEnfadado = new Leer_Textura(Content.Load<Texture2D>("Acciones/MicltantechutliEnojado"), 236, 0.25f, true);
            mictMandarAtaque = new Leer_Textura(Content.Load<Texture2D>("Acciones/MicltantechutliEnojado"), 236, 0.25f, true);
            mictComiendo = new Leer_Textura(Content.Load<Texture2D>("Miclantechutli/Miclantechutli"), 306, 0.25f, false);

            mictText = Content.Load<Texture2D>("Rectangles/RectangleMicltantechutli");
            sillaText = Content.Load<Texture2D>("Rectangles/sillaMiclantichutli");


            sillaPos.X = POSICION_SILLA_INICIAL;//GraphicsDeviceManager.DefaultBackBufferWidth - (236);
            sillaPos.Y = 1050;//mictText.Height;

            //El rectangulo de colision
            mictlPos.X = POSICION_MICLAN_INICIAL;//GraphicsDeviceManager.DefaultBackBufferWidth - (236);
            mictlPos.Y = 900;

            //El sprite
            jefePosicion.X = POSICION_INICIAL;//GraphicsDeviceManager.DefaultBackBufferWidth - (236 / 2);
            jefePosicion.Y = 1215;//GraphicsDeviceManager.DefaultBackBufferHeight - (90);

            jefeRect = new Rectangle((int)mictlPos.X - 100, (int)mictlPos.Y, mictText.Width, mictText.Height);
            sillaRect = new Rectangle((int)sillaPos.X, (int)sillaPos.Y, sillaText.Width, sillaText.Height);
            this.Content = Content;

        }

        public void Update(GameTime gameTime, Player player)
        {

            //Cargas el jefe al jugador
            if (player.posicion.X <= 12150)
            {
                animacionMictl.PlayAnimation(mictEnfadado);
            }
            else
            {
                if (!parado)
                {

                    animacionMictl.PlayAnimation(mictSentado);

                    if (timeCounterComer <= 0)
                    {
                        Random r = new Random();
                        int tiC = r.Next(TIEMPO_COMER, TIEMPO_COMER + 10);
                        timeCounterComer = tiC;


                    }
                }
                else
                {

                    animacionMictl.PlayAnimation(mictComiendo);
                    comer(player);
                }

                Ataque(gameTime, player);

                if (!paralizarPlayer)
                {
                    foreach (Cierra cierra in cierras)
                    {
                        cierra.Update(player);
                        player.Update(cierra);
                    }
                    foreach (Zombie zombie in zombies)
                    {

                        zombie.Update(player);
                        player.Update(zombie);
                    }
                }
                if (parado)
                    animacionMictl.PlayAnimation(mictComiendo);

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            SpriteEffects flip = SpriteEffects.None;

            spriteBatch.Draw(mictText, jefeRect, Color.White);
            spriteBatch.Draw(sillaText, sillaRect, Color.White);
            animacionMictl.Draw(gameTime, spriteBatch, jefePosicion, flip);
            foreach (Cierra cierra in cierras)
            {
                cierra.Draw(gameTime, spriteBatch);
            }
            foreach (Zombie zombie in zombies)
            {

                zombie.Draw(gameTime, spriteBatch);
            }


        }
        //Se cargan los ataques a las listas de ataques
        public void Ataque(GameTime gameTime, Player player)
        {
            switch (opcionAtaque)
            {
                case 1:
                    if (absorver == false)
                    {
                        absorver = true;
                        llega = true;
                        base.jefe_factor_daño = FACTOR_DANO_ABSORBER;
                    }

                    if (llega == true && player.posicion.X >= 12110)
                    {
                        if (!paralizaPlayerAboserviendo)
                        {
                            paralizaPlayerAboserviendo = true;
                            player.paralizadoBool = true;
                        }
                        else if (player.paralizadoBool)
                        {
                            if (llega)
                            {
                                player.velocity.Y -= 0.35f;
                                player.posicion.Y -= 0.05f;
                                player.posicion.X += 5;
                            }
                            if (player.rectangulo_cuerpo.Intersects(jefeRect))
                            {
                                if (player.muerteBool)
                                {
                                    llega = false;
                                    absorver = false;
                                    player.paralizadoBool = false;
                                }
                            }
                        }
                        else
                        {
                            llega = false;
                            absorver = false;
                            base.jefe_factor_daño = FACTOR_DANO;
                            opcionAtaque = 2;
                            paralizaPlayerAboserviendo = false;
                        }
                    }
                    break;
                case 2:
                    Random random = new Random();
                    double ataqueProb = random.NextDouble();


                    //Ataque parase y comer tiene una probabilidad de 0.1
                    if (ataqueProb <= 0.1 && timeCounterComer-- == TIEMPO_COMER && llega == false)
                    {

                        parado = true;

                    }
                    //Ataque cierra tiene una probabilidad de 0.4
                    if (ataqueProb <= 0.4 && llega == false    &&!paralizarPlayer )
                    {
                        if (cierras.Count < 8)
                        {
                            int casoPosicionCierra = random.Next(1, 3); //1 es para que la cierra vaya hacia la izquierda y 2 es para que vaya a la derecha
                            bool movIzq = false;
                            int posicionPersonaje = (int)player.posicion.X;

                            if (!(posicionPersonaje + 100 >= jefeRect.X))
                            {
                                switch (casoPosicionCierra)
                                {
                                    case 1:
                                        movIzq = true;
                                        break;
                                    case 2:
                                        movIzq = false;
                                        break;
                                    default:
                                        movIzq = false;
                                        break;
                                }

                            }


                            if (timeCounterCierra-- == TIEMPO_CIERRA)
                            {
                                int velociadCierra = random.Next(1, 20);
                                Cierra cierra = new Cierra();
                                cierra.Load(Content);
                                cierra.setPisicionInicial(player, movIzq);
                                cierra.velocidadPiso = velociadCierra;
                                cierras.Add(cierra);
                            }
                            else if (timeCounterCierra <= 0)
                            {
                                timeCounterCierra = TIEMPO_CIERRA;
                            }




                        }
                        //Valida si hay cierras y si ya se saliron de pantalla
                        if (cierras.Count > 0)
                        {
                            for (int i = 0; i < cierras.Count; i++)
                            {
                                if (cierras[i].enemPosicion.X < LIMITE_TEMPLO || cierras[i].enemPosicion.X - 30 > sillaRect.X)
                                {
                                    cierras.Remove(cierras[i]);
                                }
                            }
                        }
                    }

                    //Ataque zombie tiene una probabilidad de 0.1
                    else if (ataqueProb <= 0.5 && llega == false && !paralizarPlayer)
                    {
                        if (zombies.Count < 5)
                        {

                            if (timeCounterZombie-- == TIEMPO_ZOMBIE)
                            {
                                Zombie zombie = new Zombie();
                                zombie.Load(Content);
                                zombies.Add(zombie);

                            }
                            else if (timeCounterZombie <= 0)
                            {
                                timeCounterZombie = TIEMPO_ZOMBIE;
                            }

                        }
                        if (zombies.Count > 0)
                        {
                            for (int i = 0; i < zombies.Count; i++)
                            {
                                if (zombies[i].enemPosicion.X < LIMITE_TEMPLO || zombies[i].enemPosicion.X > jefeRect.X || zombies[i].enemDie)
                                {
                                    zombies.Remove(zombies[i]);
                                }
                            }
                        }
                    }

                    //Ataque succionar el alma tiene una probabilidad de 0.2
                    break;




            }
        }
        public void comer(Player player)
        {
            if (!paralizarPlayer)
            {
                if (jefeRect.X > LIMITE_TEMPLO + jefeRect.Width / 3 && !regresa && jefeRect.X + (jefeRect.Width / 2) > player.posicion.X)// && player.posicion.X != jefeRect.X && parado == true && regresa == false
                {

                    jefeRect.X = jefeRect.X - 2;
                    jefePosicion.X -= 2;

                    if (player.rectangulo_cuerpo.Intersects(jefeRect))
                    {

                        if (!paralizarPlayer)
                        {
                            moverCamara = true;
                            paralizarPlayer = true;
                            player.paralizadoBool = true;
                            base.jefe_tipo_daño = 3;// no lanzar
                            base.jefe_factor_daño = 1;
                        }
                    }
                }
                else
                {
                    moverCamara = false;
                    regresa = true;

                }
            }
            else
            {

                if (player.muerteBool)
                {
                    player.paralizadoBool = false;
                }
                else
                {
                    if (!player.paralizadoBool)
                    {
                        paralizarPlayer = false;
                        base.jefe_tipo_daño = 0;
                        base.jefe_factor_daño = FACTOR_DANO;
                        regresa = true;
                    }


                }


            }
            if (regresa == true)
            {

                //  if (posicion.X != GraphicsDeviceManager.DefaultBackBufferWidth - (236 / 2))
                if (jefeRect.X < POSICION_INICIAL)
                {

                    jefeRect.X = jefeRect.X + 1;
                    jefePosicion.X += 1;
                }
                else
                {
                    parado = false;
                    regresa = false;
                }
            }

        }
        //Retorno y seteo de listas de ataques
        public void setListaAtaqueCierras(List<Cierra> cierras)
        {
            this.cierras = cierras;
        }
        public List<Cierra> getListaAtaqueCierras()
        {
            return this.cierras;
        }
        public void setListaAtaqueZombies(List<Zombie> zombies)
        {
            this.zombies = zombies;
        }
        public List<Zombie> getListaZombies()
        {
            return this.zombies;
        }
    }
}
