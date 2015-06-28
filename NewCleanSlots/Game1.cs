using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameFramework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using System;
using System.Collections;
using System.IO;
namespace NewCleanSlots
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : GameHost //instead of using :Game, used GameHost to go with "using GameFramework. Enabled UpdateAll() function
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Texture2D FourReelsBackground, TotalCreditsBackground, AnimationReels;
        Vector2 FourReelsBackgroundPosition, TotalCreditsBackgroundPosition,AnimationReelsPosition = new Vector2 (55,65);
        int animationFrame = 0;

        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            

            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            FourReelsBackground = Content.Load<Texture2D>("Four_Reels_bkgrnd");
            TotalCreditsBackground = Content.Load<Texture2D>("Total_Credits_bkgrnd");
            AnimationReels = Content.Load<Texture2D>("AnimationReelImages"); 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit(); //makes back button exit game

         UpdateAll(gameTime);   //method from GameFramework        

      /* if (GamePage.Current.SpinButtonClicked != null)
        {
            GamePage.Current.SpinButtonClicked = true;
        }*/
      //  if ((bool)GamePage.Current.SpinButtonClicked)
       // {
          if (GamePage.Current.SpinButtonClicked)
           {
                //Code for animation

                //sprite continues to move down and then jumps to the top
                AnimationReelsPosition.Y += 5;
                if (AnimationReelsPosition.Y >= 175) //>= GraphicsDevice.Viewport.Height)
                    // spritePosition.Y = 0; 
                    //sprite jumps to the negative position based on the height of the sprite
                    //negative bc it above the 0 position, above the screen
                    AnimationReelsPosition.Y = 100; //= -spriteSheet.Height;

                animationFrame += 1;
                if (animationFrame >= 8)
                    animationFrame = 0;

              // GamePage.Current.SpinButtonClicked = false; //unclicked button
            }
     //  }
        
            base.Update(gameTime);
        }

       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);

            FourReelsBackgroundPosition = new Vector2(55, 65);
            TotalCreditsBackgroundPosition = new Vector2(55,285);
           // AnimationReelsPosition = new Vector2(55, 65);

            spriteBatch.Begin();
            //image,postion,rectangle,color,rotation,origin,scale,effect,depth
            spriteBatch.Draw(FourReelsBackground, FourReelsBackgroundPosition, null, Color.White, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
          // spriteBatch.Draw(TotalCreditsBackground, TotalCreditsBackgroundPosition, null, Color.White, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);

            spriteBatch.Draw(AnimationReels, AnimationReelsPosition, new Rectangle(animationFrame * 75, 0, 75, 75), new Color(Color.White,0.8f), 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.5f);
            spriteBatch.End();
          
            base.Draw(gameTime);
        }


    }

}
