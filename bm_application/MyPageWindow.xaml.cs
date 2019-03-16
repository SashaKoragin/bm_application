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
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Globalization;

namespace bm_application
{
    /// <summary>
    /// Логика взаимодействия для MyPageWindow.xaml
    /// </summary>
    public partial class MyPageWindow: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MyPageWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Submit(object sender, EventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text,
                @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else if (tel_number.Text.Length == 0)
            {
                errormessage.Text = "Enter telephone number.";
                tel_number.Focus();
            }
            else if (!Regex.IsMatch(tel_number.Text, @"^\+\d{1,3}\s?\(\d{3}\)\s?\d{3}(-\d{2}){2}$"))
            {
                errormessage.Text = "Enter a valid number.";
                tel_number.Select(0, tel_number.Text.Length);
                tel_number.Focus();
            }
            else
            {
                errormessage.Text = "Successfully!";
                MessageBox.Show("Спасибо за обращение. Мы обязательно с Вами свяжемся!");
            }
            if (!Directory.Exists (@"C:\Temp")) Directory.CreateDirectory(@"C:\Temp");
            string writePath = @"C:\Temp\ath.txt";

            string text = "ФИО: " + name_f_s.Text + ", " + "e-mail: " + textBoxEmail.Text + ", " + "Номер телефона: " +
                          tel_number.Text + ", " + "Причина обращения: " + reason.Text + ", " +
                          "Удобное время для связи: " + phonesList.Text;
            try
            {

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Дозапись");
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string _name, _email;
        public string Name1
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name1");
                ButtonChanged(Name1, Email1); 
            }
        }
        public string Email1
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email1");
                ButtonChanged(Name1, Email1);
            }
        }

        void ButtonChanged (string name, string email)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                Colorb.Background = new SolidColorBrush(Colors.Green);
                Colorb.IsEnabled = true;
            }

            else
            {
                Colorb.Background = new SolidColorBrush(Colors.White);
                Colorb.IsEnabled = false;
            }
        }

    }
}
