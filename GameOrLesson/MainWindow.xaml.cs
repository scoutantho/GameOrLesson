using System;
using System.Collections;
using System.IO;
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
       // DirectoryInfo d = new DirectoryInfo(@"F:\"); //get on option menu
            FileInfo[] Games = new DirectoryInfo(@"F:\").GetFiles("*.lnk"); //only games and TS 
        FileInfo[] courses = new DirectoryInfo(@"D:\Annee 4\").GetFiles(); //pas les sous dossier
        //list start at 0 

        public MainWindow()
        {     InitializeComponent();
          
            foreach(FileInfo file in courses)
            {
                debugList.Items.Add(file.Name); //affiche les nom des fichiers dans la list d'affichage
            }
            
            //ask if game or lesson 

            //if game : foreach file on "jeux" and ask wich play ? 

            //if lesson, watch all in semestre 08 and open browser and if application must be open, open it 
            // maybe open browser with connection on portail and with 
       
        }

        private void game_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Process.Start(Games[9].FullName);
            
            Console.WriteLine();

            PowerLineStatus status = System.Windows.SystemParameters.PowerLineStatus;
            if (status == PowerLineStatus.Online) { debugList.Items.Add("branché"); } //get si il est branch ou non 
        }

        private void lesson_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
