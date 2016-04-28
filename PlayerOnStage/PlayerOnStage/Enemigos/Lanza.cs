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
    class Lanza
    {
   /*     //variables de la cirra

        PlayerAnimacion animacionCierra;
        Animacion cierraGirando;
        public Vector2 position;
        Vector2 velocity;
        bool lanzar = true;
        int x;
        bool hasJumped=true;
        int caso;
        Texture2D cierraText;
        public Rectangle cierraRect;
       
        // Velociadad de la cierra en el piso
        public int velocidadPiso;
        
        public Lanza(){
            velocidadPiso = 1;

        }
        public void Load(ContentManager Content)
        {
            cierraGirando = new Animacion(Content.Load<Texture2D>("Acciones/cierra"), 62, 0.25f, true);
            cierraText = Content.Load<Texture2D>("Rectangles/RectangleCierra");
          
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;   
            spriteBatch.Draw(cierraText, position, Color.White);
            animacionCierra.Draw(gameTime, spriteBatch, position, flip);
        }
        //Esta funcion se encarga de sumar o restar una posicion aleatoria inicial a las cierras que caen hacia la derecha o izquirda
        //Esta funcion se encarga de sumar o restar una posicion aleatoria inicial a las cierras que caen hacia la derecha o izquirda
        public void setPisicionInicial(Player player, int espacioCierraJugador, bool movIzq)
        {
            if (movIzq)
            {
                caso = 1;
                position.X  = player.rectangulo_colision.X;
                if (!(position.X  -espacioCierraJugador < 0))
                  position.X -= espacioCierraJugador;

            }
            else
            {
                caso = 2;
                position.X = player.rectangulo_colision.X+espacioCierraJugador;
                
            }

        }
        public void Update(GameTime gameTime, Player player)
        {
            cierraRect = new Rectangle((int)position.X-(61/2), (int)position.Y, cierraText.Width, cierraText.Height);
            if (lanzar)
            {


                lanzar = false;
                if (position.X == 0)
                    setPisicionInicial(player, 61, player.flipeado);

            }
            switch (caso)
            {
                case 1:
                    position.X += velocidadPiso;
                    break;
                case 2:
                    position.X -= velocidadPiso;
                    break;
            }
          
          
            position += velocity;

         
            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }

            if (position.Y + cierraText.Height >= 450)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;

           

            animacionCierra.PlayAnimation(cierraGirando);
    
                
            
            
}
        */
      }
    
}
