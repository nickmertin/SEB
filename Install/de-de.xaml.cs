using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Install
{
    /// <summary>
    /// Interaction logic for en_ca.xaml
    /// </summary>
    public partial class de_de : Page
    {
        public de_de()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Lang.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            progress.IsIndeterminate = true;
            BinaryReader r;
            Stream s = Get.Installer(key.Text);
            if (s == null)
            {
                new BadKey_fr(key.Text).ShowDialog();
                return;
            }
            r = new BinaryReader(s);
            progress.IsIndeterminate = false;
            string dir = Environment.GetEnvironmentVariable("programdata") + "\\SEB";
            Directory.CreateDirectory(dir);
            progress.Value++;
            File.WriteAllText(Environment.GetEnvironmentVariable("programdata") + @"\SEB\Locale.dat", "de-DE|SEB ist fertig.|Sagen Sie einen Befehl:| .,:;!?|über|von|geschafft|beistand|Öffnungs Handbuch.|finden|informationen|Suche im Internet nach |Sorry, nicht bekommen, dass.|Ein Fehler ist aufgetreten: |Verlassen|{0} Ergebnisse mit.|Seite |Ergebnis {0} ist {1} von {2}.|nächste|vorherige|ergebnis|Ungültige Ergebniszahl|Ergebnis |öffnen|Öffnungs Seite im Web-Browser ...|schnipsel", Encoding.Unicode);
            progress.Value++;
            dir = Environment.GetEnvironmentVariable("temp") + "\\SEB-Install";
            Directory.CreateDirectory(dir);
            int t = r.ReadInt32();
            for (int i = 0; i < t; ++i)
                Directory.CreateDirectory(dir + r.ReadString());
            t = r.ReadInt32();
            progress.Value++;
            for (int i = 0; i < t; ++i)
                File.WriteAllBytes(dir + r.ReadString(), r.ReadBytes(r.ReadInt32()));
            progress.Value++;
            Environment.CurrentDirectory = dir;
            Process.Start(dir + "\\Setup.exe").WaitForExit();
            progress.Value++;
            App.Current.MainWindow.Close();
        }

        private void key_TextChanged(object sender, TextChangedEventArgs e)
        {
            go.IsEnabled = key.Text.Length == 8;
        }
    }
}
