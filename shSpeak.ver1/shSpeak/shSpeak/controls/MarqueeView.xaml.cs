using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace shSpeak.controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarqueeView : ContentView
    {
        string sContent { get; set; }
        public String ContentMarquee
        {
            get
            {
                if (sContent == "" || sContent == null)
                    sContent = "혼자 말하기";

                return sContent;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    sContent = value.Replace("\n", " ");
                    Ticker.Text = sContent;
                }
            }
        }

        public MarqueeView()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(10), OnTimerTick);
        }

        private bool OnTimerTick()
        {
            playMarquee();
            return true;
        }

        public static Animation TransLateXAnimation(VisualElement element, double from, double to)
        {
            return new Animation(d => { element.TranslationX = d; }, from, to);
        }

        private async void playMarquee()
        {
            var ani1 = TransLateXAnimation(Ticker, 1000, 0);
            this.Animate("tranx", ani1, 16, 1000, Easing.Linear, (d, f) => { });
        }

    }
}