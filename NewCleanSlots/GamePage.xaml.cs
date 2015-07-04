using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MonoGame.Framework;
using GameFramework;

using System.Drawing;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace NewCleanSlots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {

       readonly Game1 _game;
       public static GamePage Current { get; set; }
       //public bool  SpinButtonClicked { get; set; }
       public bool SpinButtonClicked { get; set; }
       //Nullable<bool> SpinButtonClicked { get; set; }
        public GamePage()
        {
          this.InitializeComponent();
          //Current = this;
                      
        }

       public GamePage(string launchArguments)
        {
            this.InitializeComponent();// used to show xaml buttons

            _game = XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, this);

            Current = this; //made GamePage.Current.SpinButtonClicked work on Game1 page
        }

        int _coinValue = 5;
        int _betValue = 1;
        
              
        public void Spin(object sender, RoutedEventArgs e)
        {
            //indicates Spin button is clicked
            SpinButtonClicked = true;
            
               Random rand = new Random(); //initiates random class
                int num1 = rand.Next(1, 4); //assigns num with a random number between 1-3. random # is stored in num
                int num2 = rand.Next(1, 4);
                int num3 = rand.Next(1, 4);
                int num4 = rand.Next(1, 4);

                BitmapImage bitmapImage = new BitmapImage();






              /*  switch (num1) //Desides what image to display based on random number 1 -3. This is the first reel/slot
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


                //code to check for winning combo        
                if (num1 == 1 & num2 == 1 & num3 == 1 & num4 == 1)
                {
                    _coinValue = _coinValue += 10; //adds value to coins
                    CoinValue.Text = "" + _coinValue; //displays added coin value
                }
                else

                    if (num1 == 2 & num2 == 2 & num3 == 2 & num4 == 2)
                    {
                        _coinValue = _coinValue += 10;
                        CoinValue.Text = "" + _coinValue;
                    }
                    else
                        if (num1 == 3 & num2 == 3 & num3 == 3 & num4 == 3)
                        {
                            _coinValue = _coinValue += 10;
                            CoinValue.Text = "" + _coinValue;
                        }
                        /*    else
                            //subtracts from _coinvalue if reels dont match
                                if ((num1 != 1 & num2 != 1 & num3 != 1 & num4 != 1) || 
                                    (num1 != 2 & num2 != 2 & num3 != 2 & num4 != 2) ||
                                    (num1 != 3 & num2 != 3 & num3 != 3 & num4 != 3 ))

                                 {
                                     _coinValue = _coinValue -= 1;
                                     CoinValue.Text = "" + _coinValue;

                                 } */
                        else
                        {
                            _coinValue = _coinValue -= _betValue;
                            CoinValue.Text = "" + _coinValue;

                        }
                //disables Spin button if displays text if out of coins
                if (_coinValue <= 0) 
                {
                    CoinValue.Text = "Add More Coins";
                    Spin_btn.IsEnabled = false;
                    Bet_btn.IsEnabled = false;
                    BetMinus_btn.IsEnabled = false;
                }            
        }

        public void Bet_btn_Click(object sender, RoutedEventArgs e)
        {
            
          _betValue += 1;

           
        /* for (_betValue += 1; _betValue <= 5 && _betValue >= 1; _betValue++)
            {
                if (_betValue <5 )
                {
                    Bet_btn.IsEnabled = false;
                    break;
                }
                BetValue.Text = "" + _betValue;

             
                if (_betValue <=5)
                {
                    Bet_btn.IsEnabled = true;
                    break;
                }
                else
                {
                    Bet_btn.IsEnabled = false;
                    
                }
                BetValue.Text = "" + _betValue;
            }*/
           
             if (_betValue < 5 && _betValue > 1)
            {
                Bet_btn.IsEnabled = true;
                BetMinus_btn.IsEnabled = true;
            }
            else
            { 
                Bet_btn.IsEnabled = false;
                BetMinus_btn.IsEnabled = true;
                
            }
             BetValue.Text = "" + _betValue;
        }

        public void BetMinus_btn_Click(object sender, RoutedEventArgs e)
        {

            _betValue -= 1;
           

          /*  for (_betValue -= 1; _betValue >= 1 && _betValue <=5; _betValue++)
            {
                if (_betValue < 1)
                {
                    BetMinus_btn.IsEnabled = false;
                    break;
                }
                BetValue.Text = "" + _betValue;
                if(_betValue >=1)
                {
                    BetMinus_btn.IsEnabled = true;
                    break;
                }
                BetValue.Text = "" + _betValue;
                
                
            }*/
            
            if(_betValue > 1 && _betValue < 5)
            {
                BetMinus_btn.IsEnabled = true;
                Bet_btn.IsEnabled = true;
            }
            else
            {
                BetMinus_btn.IsEnabled = false;
                Bet_btn.IsEnabled = true;
            }
            BetValue.Text = "" + _betValue;
        }
    }
}
