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
    class Aire:Enemigo
    {
        Texture2D AireText;
        public Vector2 AirePos;
        public Rectangle rectaire;
        public Aire() {
          
        
        }

        public void Load(ContentManager Content)
        {

            AireText = Content.Load<Texture2D>("Rectangles/RectangleAire");
            
           
        }
        public void Update(GameTime gameTime, Player player)
        {

            rectaire = new Rectangle((int)AirePos.X, (int)AirePos.Y, AireText.Width, AireText.Height);

            


        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(AireText, AirePos, Color.White);


        }
    }
}
