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
    class AireHori:Enemigo
    {
         
        
        Texture2D AireTextHori;
        public Vector2 AirePosHori;
        public Rectangle rectaireHori;
        public AireHori() {
          
        
        }

        public void Load(ContentManager Content)
        {
            
           
            
            
            AireTextHori = Content.Load<Texture2D>("Rectangles/ReectangleAireHori");
            
            
           
        }
        public void Update(GameTime gameTime, Player player)
        {
            
                rectaireHori = new Rectangle((int)AirePosHori.X, (int)AirePosHori.Y, AireTextHori.Width, AireTextHori.Height);
            
            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
           

                          

            spriteBatch.Draw(AireTextHori, AirePosHori, Color.White);
        }
    }
    
}
