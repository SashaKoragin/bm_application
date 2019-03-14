using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        //    private DispatcherTimer _timer;
        //    MainWindow window = new MainWindow();

        //    protected override void OnStartup(StartupEventArgs e)
        //    {
        //        _timer = new DispatcherTimer();
        //        _timer.Tick += window.button_click_video;
        //        _timer.Interval = new TimeSpan(0, 0, 0, 2, 0);
        //        _timer.Start();

        //        EventManager.RegisterClassHandler(typeof(Window), Window.MouseMoveEvent,
        //            new RoutedEventHandler(Reset_Timer));
        //        EventManager.RegisterClassHandler(typeof(Window), Window.MouseDownEvent,
        //            new RoutedEventHandler(Reset_Timer));
        //        EventManager.RegisterClassHandler(typeof(Window), Window.KeyDownEvent, new RoutedEventHandler(Reset_Timer));
        //    }

        //    private void Reset_Timer(object sender, EventArgs e)
        //    {
        //        _timer.Interval = new TimeSpan(0, 0, 0, 2, 0);
        //    }
    }
}
