using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace shSpeak.controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeadLineView : ContentView
    {
        public event EventHandler Click;

        public HeadLineView()
        {
            InitializeComponent();

            InitEndButton();

            this.AddTouchHandler(this, this.OnClick);
        }

        private void InitEndButton()
        {
            BgEndBtn.BorderColor = Color.FromHex("38A496");
            BgEndBtn.BackgroundColor = Color.FromHex("38A496");
            BgEndBtn.TextColor = Color.White;
            BgEndBtn.FontAttributes = FontAttributes.Bold;

            EndBtn.BorderColor = Color.FromHex("38A496");
            EndBtn.BackgroundColor = Color.FromHex("38A496");
            EndBtn.TextColor = Color.White;
            EndBtn.FontAttributes = FontAttributes.Bold;
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

        private void EndBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as Button;
                string sParam = btn.CommandParameter.ToString();

                Click?.Invoke(this, new BtnEventArgs { ClickBtnEvent = sParam });

                /*
                var btn = sender as Button;
                if (btn.CommandParameter.ToString() == "BgImg")
                {
                    Click?.Invoke(this, new BtnEventArgs { ClickBtnEvent = "BgImg" } );
                }
                else
                {
                    Click?.Invoke(this, new BtnEventArgs { ClickBtnEvent = "UserImg" });
                }
                */

                //OnClick();
            }
            catch
            {
            }
        }
    }

    public class BtnEventArgs : EventArgs
    {
        public string ClickBtnEvent { get; set; }
    }

}