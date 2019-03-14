using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace bm_application
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DispatcherTimer _timer;
        MainWindow window = new MainWindow();


        protected override void OnStartup(StartupEventArgs e)
        {
            _timer = new DispatcherTimer();
            //_timer.Tick += Timer_Tick;
            _timer.Tick += Video_Start;
            _timer.Interval = new TimeSpan(0, 0, 0, 2, 0);
            _timer.Start();

            EventManager.RegisterClassHandler(typeof(Window), Window.MouseMoveEvent,
                new RoutedEventHandler(Reset_Timer));
            EventManager.RegisterClassHandler(typeof(Window), Window.MouseDownEvent,
                new RoutedEventHandler(Reset_Timer));
            EventManager.RegisterClassHandler(typeof(Window), Window.KeyDownEvent, new RoutedEventHandler(Reset_Timer));
        }

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Ticked");
        //}

        private void Reset_Timer(object sender, EventArgs e)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 2, 0);
        }

        public void Video_Start(object sender, EventArgs e)
        {
            window.canvas.Children.Clear();
            MediaTimeline timeline = new MediaTimeline(new Uri("Resources/bm_video.mp4", UriKind.Relative));
            timeline.RepeatBehavior = RepeatBehavior.Forever;
            MediaClock clock = timeline.CreateClock();
            MediaPlayer player = new MediaPlayer();
            player.Clock = clock;
            VideoDrawing drawing = new VideoDrawing();
            drawing.Rect = new Rect(0, 0, 50, 50);
            drawing.Player = player;
            DrawingBrush brush = new DrawingBrush(drawing);
            window.canvas.Background = brush;
        }
    }
}
