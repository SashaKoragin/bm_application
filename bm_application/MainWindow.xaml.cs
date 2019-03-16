using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Timers;
using System.Threading;
using System.Text.RegularExpressions;

namespace bm_application
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebBrowser browser = new WebBrowser();
        ImageBrush myBrush = new ImageBrush();
        ImageBrush myBrush1 = new ImageBrush();
        ImageBrush myBrush2 = new ImageBrush();
        Button btn_back = new Button();
        Button btn_forward = new Button();
        StackPanel panel = new StackPanel();

        public bool check;

        public MainWindow()
        {
            InitializeComponent();
            TextBlock txtBlock = new TextBlock();
            ImageBrush myText = new ImageBrush();
            txtBlock.Height = 415;
            txtBlock.Width = 634;
            txtBlock.Text = "Добро пожаловать в BM GROUP!";
            myText.ImageSource = Convert(Properties.Resources.bm_main);
            txtBlock.Background = myText;
            myText.Stretch = Stretch.Fill;
            txtBlock.FontSize = 24;
            txtBlock.TextAlignment = TextAlignment.Center;
            canvas.Children.Add(txtBlock);
            check = true;
            DelayedExecutionService.DelayedExecute(() =>
            {
                if (check == true) { Screensaver(); }
            });
        }

        public void Screensaver()
        {
            canvas.Children.Clear();
            MediaTimeline timeline = new MediaTimeline(new Uri(@"Resources\bm_video.mp4", UriKind.Relative));
            timeline.RepeatBehavior = RepeatBehavior.Forever;
            MediaClock clock = timeline.CreateClock();
            MediaPlayer player = new MediaPlayer();
            player.Clock = clock;
            VideoDrawing drawing = new VideoDrawing();
            drawing.Rect = new Rect(0, 0, 50, 50);
            drawing.Player = player;
            DrawingBrush brush = new DrawingBrush(drawing);
            canvas.Background = brush;
        }
        #region Конвертер картинок из ресурсов
        public BitmapImage Convert(object value)
        {
            ImageConverter converter = new ImageConverter();

            byte[] val = (byte[])converter.ConvertTo(value, typeof(byte[]));
            BitmapImage exist = null;
            using (MemoryStream byteStream = new MemoryStream(val))
            {
                BitmapImage ko = new BitmapImage();
                ko.BeginInit();
                ko.CacheOption = BitmapCacheOption.OnLoad;
                ko.StreamSource = byteStream;
                ko.EndInit();
                exist = ko;
                byteStream.Close();
            }
            return exist;
        }
        #endregion

        #region Главная
        public void button_click_home(object sender, EventArgs e)
        {
            check = true;
            canvas.Children.RemoveAt(0);
            myBrush.ImageSource = Convert(Properties.Resources.bm_group);
            myBrush1.ImageSource = Convert(Properties.Resources.bm_background);
            myBrush2.ImageSource = Convert(Properties.Resources.form_background);
            canvas.Background = myBrush;
            //myBrush.Stretch = Stretch.Fill;
        }
        #endregion

        #region Сайт
        public void button_click_url_home(object sender, EventArgs e)
        {
            check = true;
            canvas.Children.RemoveAt(0);
            panel.Children.Clear();
            panel.Orientation = Orientation.Vertical;

            Grid gridfolder = new Grid { Background = new SolidColorBrush(Colors.White) };
            btn_back = new Button { Height = 25, Width = 315, Content = "<Назад", Background = new SolidColorBrush(Colors.AntiqueWhite) };
            btn_forward = new Button { Height = 25, Width = 315, Content = "Вперед>", Background = new SolidColorBrush(Colors.AntiqueWhite) };

            gridfolder.ColumnDefinitions.Add(new ColumnDefinition());  //1
            gridfolder.ColumnDefinitions.Add(new ColumnDefinition());  //2
            gridfolder.RowDefinitions.Add(new RowDefinition());

            Grid.SetColumn(btn_back, 0);
            Grid.SetColumn(btn_forward, 1);

            gridfolder.Children.Add(btn_back);
            gridfolder.Children.Add(btn_forward);

            panel.Children.Add(gridfolder);
            canvas.Children.Add(panel);

            browser.Navigate("http://www.bm-technology.ru");
            panel.Children.Add(browser);
            browser.Width = 634;
            browser.Height = 387;
            browser.Navigated += new NavigatedEventHandler(WebBrowser_Navigated);

            btn_back.Click += GoBack_Click;
            btn_forward.Click += GoForward_Click;
        }
        #endregion

        #region Видео
        public void button_click_video(object sender, EventArgs e)
        {
            check = false;
            canvas.Children.RemoveAt(0);
            MediaTimeline timeline = new MediaTimeline(new Uri(@"Resources\bm_video.mp4", UriKind.Relative));
            timeline.RepeatBehavior = RepeatBehavior.Forever;
            MediaClock clock = timeline.CreateClock();
            MediaPlayer player = new MediaPlayer();
            player.Clock = clock;
            VideoDrawing drawing = new VideoDrawing();
            drawing.Rect = new Rect(0, 0, 50, 50);
            drawing.Player = player;
            DrawingBrush brush = new DrawingBrush(drawing);
            canvas.Background = brush;
        }
        #endregion

        #region Ошибка со скриптом 
        void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            HideJsScriptErrors((WebBrowser)sender);
        }

        public void HideJsScriptErrors(WebBrowser wb)
        {
            FieldInfo fld = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fld == null)
                return;
            object obj = fld.GetValue(wb);
            if (obj == null)
                return;
            obj.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, obj, new object[] { true });
        }
        #endregion

        #region Навигация по сайту
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (browser.CanGoBack)
            {
                browser.GoBack();
                btn_forward.IsEnabled = true;
            }
            else
            {
                browser.Navigate("http://www.bm-technology.ru");
            }
        }
        private void GoForward_Click(object sender, RoutedEventArgs e)
        {
            if (browser.CanGoForward)
            {
                browser.GoForward();
            }
            else
            {
                btn_forward.IsEnabled = false;
            }

        }
        #endregion

        #region Таймер
        public static class DelayedExecutionService
        {
            private static IList<DispatcherTimer> timers = new List<DispatcherTimer>();

            public static void DelayedExecute(Action action, int delay = 60)
            {
                var dispatcherTimer = new DispatcherTimer();

                void Reset_Timer(object sender, EventArgs e)
                {
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 20, 0);
                }
                timers.Add(dispatcherTimer);

                EventHandler handler = null;
                handler = (sender, e) =>
                {
                    action();
                };

                dispatcherTimer.Tick += handler;
                dispatcherTimer.Interval = TimeSpan.FromSeconds(delay);
                dispatcherTimer.Start();
                EventManager.RegisterClassHandler(typeof(Window), Window.MouseMoveEvent, new RoutedEventHandler(Reset_Timer));
                EventManager.RegisterClassHandler(typeof(Window), Window.MouseDownEvent, new RoutedEventHandler(Reset_Timer));
                EventManager.RegisterClassHandler(typeof(Window), Window.KeyDownEvent, new RoutedEventHandler(Reset_Timer));
            }

        }

        #endregion

        #region Выход
        public void button_click_exit(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        #region Форма обращения
        private void button_click_form(object sender, EventArgs e)
        {
            MyPageWindow reg = new MyPageWindow();
            check = true;
            canvas.Children.RemoveAt(0);
            canvas.Background = new SolidColorBrush(Colors.White);
            try
            {
                canvas.Children.Add(reg);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion
    }
}
