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
    public class RUVG_Game : Microsoft.Xna.Framework.Game
    {
        int nivel = 1;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice dev;
        Camara camera;
        Player player;
        HUD HUD;
        SpriteFont muestra;
        NivelMictlan nivelMclan;
        NivelHuitzi nivelHuitzi;
        NivelTlaloc nivelTlaloc;
        Nivel nivelGenerico;

        public RUVG_Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;          
        }

        protected override void Initialize()
        {

            HUD = new HUD();
            player = new Player(HUD);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            muestra = Content.Load<SpriteFont>("MuestraDatos");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camara(GraphicsDevice.Viewport);
            player.Load(Content);
            dev = graphics.GraphicsDevice;
            HUD.Load(Content, dev);
            nivelHuitzi = new NivelHuitzi(Content, dev);
            nivelHuitzi.Load(Content, dev);
            nivelGenerico = (Nivel)nivelHuitzi;
            camera.setNivel(nivel);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update(gameTime);

            switch (nivel)
            {
                case 1:
                    if (!nivelHuitzi.nivelCompleto)
                    {

                        foreach (CollisionTiles tile in nivelHuitzi.getMapa().CollisionTiles)
                        {
                            player.Collision(tile.Rectangle, nivelHuitzi.getMapa().Width, nivelHuitzi.getMapa().Height);
                            camera.Update(player.posicion, nivelHuitzi.getMapa().Width, nivelHuitzi.getMapa().Height);
                        }
                        nivelHuitzi.Update(gameTime, player);
                    }
                    else if (!player.muerteBool)
                    {
                        nivel = 2;
                        camera.setNivel(nivel);
                        nivelHuitzi = null;

                        nivelMclan = new NivelMictlan(Content, GraphicsDevice);
                        nivelGenerico = (Nivel)nivelMclan;
                        nivelMclan.Load(Content, dev);
                        nivelMclan.setCamera(camera);
                    }
                    break;

                case 2:
                    if (!nivelMclan.nivelCompleto)
                    {
                        foreach (CollisionTiles tile in nivelMclan.getMapa().CollisionTiles)
                        {
                            player.Collision(tile.Rectangle, nivelMclan.getMapa().Width, nivelMclan.getMapa().Height);
                            camera.Update(player.posicion, nivelMclan.getMapa().Width, nivelMclan.getMapa().Height);
                        }
                        nivelMclan.Update(gameTime, player);
                    }
                    else if (!player.muerteBool)
                    {
                        nivel = 3;
                        nivelMclan = null;
                    }
                    break;

                case 3:

                    break;

            }

            HUD.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            spriteBatch.DrawString(muestra, "X " + player.posicion.X, new Vector2(player.posicion.X, player.posicion.Y - 100), Color.White);
            spriteBatch.DrawString(muestra, "Y " + player.posicion.Y, new Vector2(player.posicion.X, player.posicion.Y - 150), Color.White);
            switch (nivel)
            {
                case 1:
                    nivelHuitzi.Draw(gameTime, spriteBatch);
                    break;
                case 2:
                    nivelMclan.Draw(gameTime, spriteBatch);
                    break;
                case 3:
                    break;
            }

            player.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            HUD.Draw(gameTime, spriteBatch);

            if (nivelGenerico.banderaJefe)
            {
                if (!HUD.getMuestraJefe())
                    HUD.isMuestraJefe(true);
                HUD.Update();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
