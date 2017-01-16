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
using System.Windows.Forms;
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
       DirectoryInfo d = new DirectoryInfo(@"F:\"); //get on option menu
         FileInfo[] Games = new DirectoryInfo(@"F:\").GetFiles("*.lnk"); //only games and TS 
        DirectoryInfo[] courses = new DirectoryInfo(@"D:\Annee 4\").GetDirectories(); //pas les sous dossier


        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
         System.Windows.Forms.ContextMenu contextMenu;
        System.Windows.Forms.ContextMenu GameMenu;
        System.Windows.Forms.MenuItem menuItem;
        //list start at 0 

            //TODO
        //ask if game or lesson 

        //if game : foreach file on "jeux" and ask wich play ? 

        //if lesson, watch all in semestre 08 and open browser and if application must be open, open it 
        // maybe open browser with connection on portail and with 
        //find how do a separator 
        // find how do liste déroulante  en fonction du contenu de l'autre 
        // ask user for wich program launch when click on courses
        // ask where courses are 
        // ask where games are 
        //ask if want to launch many things when one things is hit (ask where is link want to add something and ask where is link for things to add )  



        public MainWindow()
        {
            nIcon.Icon = new System.Drawing.Icon(@"C:\Users\antho\Desktop\GameOrLesson\GameOrLesson\icon.ico");
            nIcon.Visible = true;
            //nIcon.ShowBalloonTip(5000, "Title", "Text", System.Windows.Forms.ToolTipIcon.Info);
            nIcon.DoubleClick += new EventHandler( nIcon_DoubleClick);

            this.contextMenu = new System.Windows.Forms.ContextMenu();
           
            this.GameMenu = new System.Windows.Forms.ContextMenu();

            this.menuItem = new System.Windows.Forms.MenuItem();
            contextMenu.MenuItems.Add(menuItem);
            menuItem.Text = "open";
            menuItem.Click += new EventHandler(this.open_click);
            

            nIcon.ContextMenu = contextMenu; //add contextmenu to icon

            InitializeComponent();
          
            
            foreach(FileInfo file in Games)
            {
                this.menuItem = new System.Windows.Forms.MenuItem();
                contextMenu.MenuItems.Add(menuItem);

                menuItem.Text = file.Name;  //just for screen 
                menuItem.Name = file.FullName;// for gate the full name for launch application 
                menuItem.Click += new EventHandler(this.menuItem_Game_Click); //pour tous les fichiers, le meme events
                
            }
           

            Window_Closing(); //hide window 

            


        }

        private void open_click(object sender, EventArgs e) //for see application, for options etc 
        {
            this.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Normal;
        }

        private void menuItem_Game_Click(object sender, EventArgs e) //when click on menuitem for game 
        {
            // get name from eventhandler
            String name = ((System.Windows.Forms.MenuItem)sender).Name;
            System.Diagnostics.Process.Start(name);
        }

        private void nIcon_DoubleClick(object sender, EventArgs e) //close application
        {
            this.Close();
        } 

        private void game_Click(object sender, RoutedEventArgs e)
        {          
            Console.WriteLine();

            //PowerLineStatus status = System.Windows.SystemParameters.PowerLineStatus; 
            //if (status == PowerLineStatus.Online) { debugList.Items.Add("branché"); } //get si il est branch ou non 
        }

        private void lesson_Click(object sender, RoutedEventArgs e)
        {
            foreach(DirectoryInfo directory in courses)
            {
                debugList.Items.Add(directory.Name); //sans git si possible 
                foreach(DirectoryInfo dir in (new DirectoryInfo(directory.FullName).GetDirectories()))
                {
                    debugList.Items.Add(dir.Name);
                }
            }

            //throw new NotImplementedException();
        }

        private void Window_Closing() //hide application
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
