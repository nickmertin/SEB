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
    public partial class BadKey_en : Window
    {
        public BadKey_en(string key)
        {
            InitializeComponent();
            _.Text = string.Format("The product key which you specified, {0}, either does not exist or was already used.", key);
        }
    }
}
