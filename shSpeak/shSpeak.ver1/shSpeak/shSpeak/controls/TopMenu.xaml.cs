using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace shSpeak.controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopMenu : ContentView
    {
        public event EventHandler Click;

        public TopMenu()
        {
            InitializeComponent();

            DisplayMarquee();

            //InitMenuColor();

            this.AddTouchHandler(this, this.OnClick);
        }

        private void DisplayMarquee()
        {
            mView.ContentMarquee = string.Format("혼자 말하기");
        }

        private void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }

        protected void AddTouchHandler(View view, Action action)
        {
            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    view.Opacity = 0.6;
                    view.FadeTo(1);
                    action();
                })
            });
        }

        private void DisplayVisibleMenu(bool bVisibleMenu)
        {
            fMenu.IsVisible = bVisibleMenu;
            fMenuSelect.IsVisible = !bVisibleMenu;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            DisplayVisibleMenu(false);
        }
                

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            DisplayVisibleMenu(true);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            DisplayVisibleMenu(true);

            Click?.Invoke(this, new BtnEventArgs { ClickBtnEvent = "UserImg" });
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            DisplayVisibleMenu(true);

            Click?.Invoke(this, new BtnEventArgs { ClickBtnEvent = "BgImg" });
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            DisplayVisibleMenu(true);

            Click?.Invoke(this, new BtnEventArgs { ClickBtnEvent = "Eraser" });
        }

        public void InitMenuColor(bool bChange = false)
        {
            if (bChange)
            {
                TopMenubgColor.BackgroundColor = Color.White;
                x1.Foreground = Color.FromHex("#63ACD5");
                x2.Foreground = Color.FromHex("#63ACD5");
                x3.Foreground = Color.FromHex("#63ACD5");
                x4.Foreground = Color.FromHex("#63ACD5");
                x5.Foreground = Color.FromHex("#63ACD5");
            }
            else
            {
                TopMenubgColor.BackgroundColor = Color.FromHex("#63ACD5");
                x1.Foreground = Color.White;
                x2.Foreground = Color.White;
                x3.Foreground = Color.White;
                x4.Foreground = Color.White;
                x5.Foreground = Color.White;
            }
        }

    }
}