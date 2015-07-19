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
            AnimationReel4,
            Button;

        Vector2 FourReelsBackgroundPosition, 
            TotalCreditsBackgroundPosition,
            AnimationReel1Position,
            AnimationReel2Position,
            AnimationReel3Position,
            AnimationReel4Position;
            

        SpriteFont totalText,
            betText;

        Rectangle betAddButton,
            betMinusButton,
            spinButton;

        int animationFrame = 0;


        TimeSpan spinTimer = TimeSpan.Zero;

           
        int _coinValue = 10,
            _betValue = 1,
            x = 0,
            y = 0;

        bool isInputPressed = false;
        bool isInputClicked = false; // so spin button keeps animation() going.

           


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
            totalText = Content.Load<SpriteFont>("ArialFont");
            betText = Content.Load<SpriteFont>("ArialFont");
            Button = Content.Load<Texture2D>("Orange_btn 1");

            
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

         var _touches = TouchPanel.GetState();

            
       //check if screen is touched
          if (_touches.Count >=1)
          {
              var touch = _touches[0];
              x = (int)touch.Position.X;
              y = (int)touch.Position.Y;

              isInputPressed = touch.State == TouchLocationState.Pressed; //|| touch.State == TouchLocationState.Moved; 
              isInputClicked = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Released;
          }
          //create bet button. check to see if it is touch and if so increase bet

            //rectangle(x location , y location, width of sprite, height of sprite)
          betAddButton = new Rectangle(300, 325, 185, 100); //rectangle will not show anything untill draw() is created w sprite
            if(isInputPressed && betAddButton.Contains(x,y) && _betValue < 5 )
            {
                _betValue++;
            }

          betMinusButton = new Rectangle(100, 325, 185, 100);
            if (isInputPressed && betMinusButton.Contains(x,y) && _betValue >=1)
            {
                _betValue--;
            }

            spinButton = new Rectangle(525, 325, 185, 100);
            if (isInputPressed && spinButton.Contains(x,y)) //coinValue shows as soon as spinButton is clicked
            {
                _coinValue = _coinValue -= _betValue;
            }
            if (isInputClicked && spinButton.Contains(x, y)) //keeps animation going... for 3 seconds
            {
                Animation();
                
                spinTimer += gameTime.ElapsedGameTime;
                if (spinTimer >= TimeSpan.FromSeconds(3))
                {
                  //  _coinValue = _coinValue -= _betValue;
                    Spinning();
                    spinTimer = TimeSpan.Zero;
                    isInputClicked = false;
                }
            }
            
                                         
                           
        /*  if (GamePage.Current.SpinButtonClicked) //may change if touchpanel works and able to create own buttons w Monogame
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
             
           
            }*/
          

        
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

            spriteBatch.DrawString(totalText, "TOTAL  " + _coinValue, new Vector2(500, 300), Color.Purple);

            spriteBatch.DrawString(betText, "BET  " + _betValue, new Vector2(200, 300), Color.Purple);

            spriteBatch.Draw(Button, spinButton, Color.White);

           // spriteBatch.Draw(BetIncreaseButton, BetIncreaseButtonPosition, betButton, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(Button, betAddButton, Color.White);

            if (_betValue >=5)
            {
                spriteBatch.Draw(Button, betAddButton, Color.Gray);
            }

            spriteBatch.Draw(Button, betMinusButton, Color.White);

            if (_betValue <=1)
            {
                spriteBatch.Draw(Button, betMinusButton, Color.Gray);
            }
                              
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
          

           // animationFrame += 1;
            animationFrame++;
            if (animationFrame >= 8)
            {
                animationFrame = 0;
            }
          
        }

        
        
        public void Spinning() //Displays Image based on random number, checkes for winning combo, 
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
          
            //Check for winning Combos
          //  int _coinValue = 5;

            if (reel1 == 1 & reel2 == 1 & reel3 == 1 & reel4 == 1)
            {
                _coinValue = _coinValue += 10; //increase the coinValue by 10

                //_coinValue shows in spritBatch draw               
                                
            }
            else
                if (reel1 == 2 & reel2 == 2 & reel3 == 2 & reel4 == 2)
                {
                    _coinValue = _coinValue += 10;


                }
                else
                    if (reel1 == 3 & reel2 == 3 & reel3 == 3 & reel4 == 3)
                    {
                        _coinValue = _coinValue +=10;
                    }
                  
        }
        
      
      

    }

}
