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
using System.Windows.Navigation;

namespace bm_application
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Button button = new Button();        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_click_home(object sender, EventArgs e)
        {
            canvas.Children.Clear();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"Resources\bm_group.png", UriKind.Relative));
            canvas.Background = myBrush;
            myBrush.Stretch = Stretch.Fill;
        }

        private void button_click_url_home(object sender, EventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.White);
            WebBrowser browser = new WebBrowser();
            browser.Navigate("http://www.bm-technology.ru");
            canvas.Children.Add(browser);
            browser.Width = 630;
            browser.Height = 400;
            //canvas.Children.Add(button);
            //button.Click += MyBack_Click;
            //button.Click += MyForward_Click;
        }

        private void Button_video_Click(object sender, RoutedEventArgs e)
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

        //private void MyBack_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        browser.GoBack();              
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void MyForward_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        browser.GoForward();
        //        button.Height = 50;
        //        button.Width = 50;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
