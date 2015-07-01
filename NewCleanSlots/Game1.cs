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
        
        Texture2D FourReelsBackground, TotalCreditsBackground, AnimationReels,ImageBox1;
        Vector2 FourReelsBackgroundPosition, TotalCreditsBackgroundPosition,AnimationReelsPosition = new Vector2 (55,65), Reel1Position=new Vector2 (56,65);
        int animationFrame = 0;

        Model Reel1;
        


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

            Reel1 = Content.Load<Model>("Reel_Images_Star");
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
           // spriteBatch.Draw(Reel1, Reel1Position, null, Color.White, 0f, Vector2.Zero, 0.0f, SpriteEffects.None, 0.0f);
            
            spriteBatch.End();
          
            base.Draw(gameTime);
        }
         public void Spinning()
        {

            Random rand = new Random(); //initiates random class
            int num1 = rand.Next(1, 4); //assigns num with a random number between 1-3. random # is stored in num
            int num2 = rand.Next(1, 4);
            int num3 = rand.Next(1, 4);
            int num4 = rand.Next(1, 4);

            switch (num1)
            {
                case 1:
                    Reel1 = Content.Load<Model>("Reel_Images_Bar.png");
                    break;

                case 2:
                    Reel1 = Content.Load<Model>("Reel_Images_Star.png");
                    break;

                case 3:
                    Reel1 = Content.Load<Model>("Reel_Images_Star.png");
                    break;

            }

           /* BitmapImage bitmapImage = new BitmapImage();






            switch (num1) //Desides what image to display based on random number 1 -3. This is the first reel/slot
            {
                case 1:
                    ImageBox1.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Bar.png"));
                    break;

                case 2:
                    ImageBox1.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Diamond.png"));
                    break;
                case 3:
                    ImageBox1.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Star.png"));
                    break;
            }

            switch (num2) //Desides what image to display based on random number 1 -3. This is the second reel/slot
            {
                case 1:
                    ImageBox2.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Bar.png"));
                    break;

                case 2:
                    ImageBox2.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Diamond.png"));
                    break;
                case 3:
                    ImageBox2.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Star.png"));
                    break;
            }

            switch (num3) //Desides what image to display based on random number 1 -3. This is the third reel/slot
            {
                case 1:
                    ImageBox3.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Bar.png"));
                    break;

                case 2:
                    ImageBox3.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Diamond.png"));
                    break;
                case 3:
                    ImageBox3.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Star.png"));
                    break;
            }

            switch (num4) //Desides what image to display based on random number 1 -3. This is the fourth reel/slot
            {
                case 1:
                    ImageBox4.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Bar.png"));
                    break;

                case 2:
                    ImageBox4.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Diamond.png"));
                    break;
                case 3:
                    ImageBox4.Source = new BitmapImage(new Uri("ms-appx:///Content/Reel_Images_Star.png"));
                    break;
            }*/
        }

    }

}
