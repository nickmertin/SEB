using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Install
{
    /// <summary>
    /// Interaction logic for BadKey_en.xaml
    /// </summary>
    public partial class BadKey_de : Window
    {
        public BadKey_de(string key)
        {
            InitializeComponent();
            _.Text = string.Format("Die von Ihnen angegebene Produktschlüssel, {0}, ist entweder nicht vorhanden oder wurde bereits benutzt.", key);
        }
    }
}
