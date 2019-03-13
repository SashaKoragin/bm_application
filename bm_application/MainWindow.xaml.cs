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

namespace bm_application
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebBrowser browser = new WebBrowser();
        ImageBrush myBrush = new ImageBrush();
        Button btn_back = new Button();
        Button btn_forward = new Button();
        StackPanel panel = new StackPanel();

        public MainWindow()
        {
            InitializeComponent();
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
        private void button_click_home(object sender, EventArgs e)
        {
            canvas.Children.Clear();
            myBrush.ImageSource = Convert(Properties.Resources.bm_group);
            canvas.Background = myBrush;
            myBrush.Stretch = Stretch.Fill;
        }
        #endregion

        #region Сайт
        private void button_click_url_home(object sender, EventArgs e)
        {
            canvas.Children.Clear();
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
            browser.Width = 630;
            browser.Height = 400;
            browser.Navigated += new NavigatedEventHandler(WebBrowser_Navigated);
            
            btn_back.Click += GoBack_Click;
            btn_forward.Click += GoForward_Click;
        }
        #endregion

        #region Видео
        private void button_click_video(object sender, RoutedEventArgs e)
        {
            var mediaPlayer = new MediaElement();

            mediaPlayer.HorizontalAlignment = HorizontalAlignment.Left;
            mediaPlayer.VerticalAlignment = VerticalAlignment.Top;
            mediaPlayer.Height = 630;
            mediaPlayer.Width = 400;
            mediaPlayer.Source = new Uri("https://www.youtube.com/watch?v=UZSFY8YdSrA", UriKind.Absolute);

            mediaPlayer.LoadedBehavior = MediaState.Manual;
            if (mediaPlayer != null)
            {
                canvas.Children.Add(mediaPlayer);
                mediaPlayer.Play();
            }
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
    }
}
