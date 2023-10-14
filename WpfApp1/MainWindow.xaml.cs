using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Policy;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public string theBook;
        
        void GetBook(string uri)
        {
            WebClient wc = new WebClient();

            wc.DownloadStringCompleted += (s, eArgs) =>
            {
                theBook = eArgs.Result;
                Dispatcher?.Invoke(() => text.Text = theBook);
            };
            wc.DownloadStringAsync(new Uri(uri));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetBook(uri.Text);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string str = text.Text;
            string result = RemoveSpacesParallel(str);
            text.Text = result;
            static string RemoveSpacesParallel(string input)
            {
                char[] chars = input.ToCharArray();

                Parallel.For(0, chars.Length, i =>
                {
                    if (chars[i] == ' ')
                    {
                        Array.Reverse(chars);//ВОТ ЭТА ШТУКА ПЕРЕВОРАЧИВАЕТ ВЕСЬ ТЕКСТ
                        chars[i] = '\0';
                    }

                });
                return new string(chars).Replace("\0", "");

            }


        }
    }
}