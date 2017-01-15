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

namespace GameOrLesson
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Diagnostics.Process.Start(@"F:\Steam\Steam.exe");

            //ask if game or lesson 

            //if game : foreach file on "jeux" and ask wich play ? 

            //if lesson, watch all in semestre 08 and open browser and if application must be open, open it 
            // maybe open browser with connection on portail and with 
            InitializeComponent();
        }
    }
}
