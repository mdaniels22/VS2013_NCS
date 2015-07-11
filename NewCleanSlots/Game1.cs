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
        
        Texture2D FourReelsBackground, 
            TotalCreditsBackground, 
            AnimationReel1,
            AnimationReel2,
            AnimationReel3,
            AnimationReel4;

        Vector2 FourReelsBackgroundPosition, 
            TotalCreditsBackgroundPosition,
            AnimationReel1Position,
            AnimationReel2Position,
            AnimationReel3Position,
            AnimationReel4Position;
            

        int animationFrame = 0;
        

        TimeSpan spinTimer = TimeSpan.Zero;

       


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
            AnimationReel1 = Content.Load<Texture2D>("AnimationReelImages");
            AnimationReel2 = Content.Load<Texture2D>("AnimationReelImages");
            AnimationReel3 = Content.Load<Texture2D>("AnimationReelImages");
            AnimationReel4 = Content.Load<Texture2D>("AnimationReelImages");

            
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

          if (GamePage.Current.SpinButtonClicked)
           {

              Animation();
              //Spinning();
              spinTimer += gameTime.ElapsedGameTime;
              

              if(spinTimer >= TimeSpan.FromSeconds(3))
              {
                  Spinning();
                  GamePage.Current.SpinButtonClicked = false;
                 // AnimationReels = Content.Load<Texture2D>("AnimationReelImages");
                  spinTimer = TimeSpan.Zero;

              }
             
           
            }
          
        
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
            AnimationReel1Position = new Vector2(35, 65);
            AnimationReel2Position = new Vector2(235, 65);
            AnimationReel3Position = new Vector2(435, 65);
            AnimationReel4Position = new Vector2(635, 65);

           
           

            spriteBatch.Begin();
            //image,postion,rectangle,color,rotation,origin,scale,effect,depth
            spriteBatch.Draw(FourReelsBackground, FourReelsBackgroundPosition, null, Color.White, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0.7f);
            spriteBatch.Draw(TotalCreditsBackground, TotalCreditsBackgroundPosition, null, Color.White, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);

            spriteBatch.Draw(AnimationReel1, AnimationReel1Position, new Rectangle(animationFrame * 75, 0, 75, 75), new Color(Color.White,1.0f),
                0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(AnimationReel2, AnimationReel2Position, new Rectangle(animationFrame * 75, 0, 75, 75), Color.White, 
                0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(AnimationReel3, AnimationReel3Position, new Rectangle(animationFrame * 75, 0, 75, 75), Color.White,
               0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(AnimationReel4, AnimationReel4Position, new Rectangle(animationFrame * 75, 0, 75, 75), Color.White,
               0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.5f);
            

                              
            spriteBatch.End();
          
            base.Draw(gameTime);
        }


        public void Animation()
        {
            //Code for animation
            AnimationReel1 = Content.Load<Texture2D>("AnimationReelImages"); //everytime animation begins it starts with the AnimationReelImages
            AnimationReel2 = Content.Load<Texture2D>("AnimationReelImages");
            AnimationReel3 = Content.Load<Texture2D>("AnimationReelImages"); 
            AnimationReel4 = Content.Load<Texture2D>("AnimationReelImages");

            //sprite continues to move down and then jumps to the top
            AnimationReel1Position.Y += 5;
            if (AnimationReel1Position.Y >= 120) //>= GraphicsDevice.Viewport.Height). If the Y coordinats of animation goes below 120...
                // spritePosition.Y = 0; 
                //sprite jumps to the negative position based on the height of the sprite
                //negative bc it above the 0 position, above the screen
                AnimationReel1Position.Y = 65; //= -spriteSheet.Height;.....the Y coordinates of the animation jumps back up to 65

            AnimationReel2Position.Y += 5;
            if (AnimationReel2Position.Y >= 120)
            {
                AnimationReel2Position.Y = 65;
            }

            AnimationReel3Position.Y += 5;
            if (AnimationReel3Position.Y >= 120)
            {
                AnimationReel3Position.Y = 65;
            }

            AnimationReel4Position.Y += 5;
            if (AnimationReel4Position.Y >= 120)
            {
                AnimationReel4Position.Y = 65;
            }
          

            animationFrame += 1;
            if (animationFrame >= 8)
                animationFrame = 0;

          
        }

        
        
        public void Spinning()
        {
            animationFrame = 0; //resets the animationFrame back to 0
            AnimationReel1Position.Y = 65; //resets the Y coordinats of the animation. Change later so Random reel image displays closer to center

           Random rand = new Random(); //initiates random class
            int reel1 = rand.Next(1, 4); //assigns num with a random number between 1-3. random # is stored in num
            int reel2 = rand.Next(1, 4);
            int reel3 = rand.Next(1, 4);
            int reel4 = rand.Next(1, 4);

            switch (reel1)
            {
                case 1:
                    AnimationReel1 = Content.Load<Texture2D>("Reel_Images_Bar1");
                    
                    break;

                case 2:
                    AnimationReel1 = Content.Load<Texture2D>("Reel_Images_Star1");
                   
                    break;

                case 3:
                    AnimationReel1 = Content.Load<Texture2D>("Reel_Images_Diamond1");
                    
                    break;


            }
            switch (reel2)
            { 
                case 1:
                    AnimationReel2 = Content.Load<Texture2D>("Reel_Images_Bar1");
                    break;
                case 2:
                    AnimationReel2 = Content.Load<Texture2D>("Reel_Images_Star1");
                    break;
                case   3:
                    AnimationReel2 = Content.Load<Texture2D>("Reel_Images_Diamond1");
                    break;

            }
            switch (reel3)
            {
                case 1:
                    AnimationReel3 = Content.Load<Texture2D>("Reel_Images_Bar1");
                    break;
                case 2:
                    AnimationReel3 = Content.Load<Texture2D>("Reel_Images_Star1");
                    break;
                case 3:
                    AnimationReel3 = Content.Load<Texture2D>("Reel_Images_Diamond1");
                    break;
            }
            switch (reel4)
            {
                case 1:
                    AnimationReel4 = Content.Load<Texture2D>("Reel_Images_Bar1");
                    break;
                case 2:
                    AnimationReel4 = Content.Load<Texture2D>("Reel_Images_Star1");
                    break;
                case 3:
                    AnimationReel4 = Content.Load<Texture2D>("Reel_Images_Diamond1");
                    break;
            }
          

        }

    }

}
