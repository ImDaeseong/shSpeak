using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}