using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
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
        DirectoryInfo[] courses = new DirectoryInfo(@"D:\Annee 4\").GetDirectories();

        DirectoryInfo[] lecteur = new DirectoryInfo(@"D:\Annee 4\").GetDirectories();
        


        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
         System.Windows.Forms.ContextMenuStrip contextMenu;
        System.Windows.Forms.ToolStripMenuItem GameMenu, courseMenu, Game, Courses;
        System.Windows.Forms.ToolStripMenuItem optionMenuItem; //inside menuitem : game menu, open and courses



        ContextMenuStrip menu = new ContextMenuStrip();
        ToolStripMenuItem item, submenu, submenu2;

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
            #region must be in a function
            nIcon.Icon = new System.Drawing.Icon(@"C:\Users\antho\Desktop\GameOrLesson\GameOrLesson\icon.ico");
            nIcon.Visible = true;
            //nIcon.ShowBalloonTip(5000, "Title", "Text", System.Windows.Forms.ToolTipIcon.Info);
            nIcon.DoubleClick += new EventHandler( nIcon_DoubleClick);
           

            this.contextMenu = new System.Windows.Forms.ContextMenuStrip();
           
            this.GameMenu = new System.Windows.Forms.ToolStripMenuItem();
            GameMenu.Text = "Jeux";
            this.courseMenu = new ToolStripMenuItem();
            courseMenu.Text = "cours";

            this.optionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
           
            optionMenuItem.Text = "open";
            optionMenuItem.Click += new EventHandler(this.open_click);

          
            foreach(FileInfo file in Games)
            {
                this.Game = new System.Windows.Forms.ToolStripMenuItem();
                
                GameMenu.DropDownItems.Add(Game);
                

                Game.Text = file.Name;  //just for screen 
                Game.Name = file.FullName;// for gate the full name for launch application 
                Game.Click += new EventHandler(this.menuItem_Game_Click); //pour tous les fichiers, le meme events
                
            }
            #endregion

            courseMenu = Lesson();

            this.Courses = new ToolStripMenuItem();
            Courses.Text = "cours";
            Courses = getMenuItem(Courses, lecteur);

            contextMenu.Items.Add(optionMenuItem);
            contextMenu.Items.Add(GameMenu);
           // contextMenu.Items.Add(courseMenu);
            contextMenu.Items.Add(Courses);

            nIcon.ContextMenuStrip = contextMenu; //add contextmenu to icon

            InitializeComponent();

            //Window_Closing(); //hide window 
            this.Window_Closing(this, new CancelEventArgs()) ;

            


        }

        private void open_click(object sender, EventArgs e) //for see application, for options etc 
        {
            this.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Normal;
        }

        private void menuItem_Game_Click(object sender, EventArgs e) //when click on menuitem for game 
        {
            // get name from eventhandler
            String name = ((System.Windows.Forms.ToolStripMenuItem)sender).Name;
            System.Diagnostics.Process.Start(name);
        }

        private void nIcon_DoubleClick(object sender, EventArgs e) //close application
        {
            this.Close();

        } 

       

        private void lesson_Click(object sender, EventArgs e)
        {
            String fullname = ((ToolStripMenuItem)sender).Name;
            Process.Start("explorer.exe", fullname);

            //throw new NotImplementedException();
        }
        private ToolStripMenuItem Lesson() //faire en recursif
        {
            ToolStripMenuItem ReturnMenuItem = new ToolStripMenuItem();
            ReturnMenuItem.Text = "Cours";
            ToolStripMenuItem tempMenuItemReturn = new ToolStripMenuItem();
            ToolStripMenuItem MenuItemInside = new ToolStripMenuItem();

            foreach (DirectoryInfo directory in courses)
            {
                tempMenuItemReturn = new ToolStripMenuItem();
                tempMenuItemReturn.Text = directory.Name;
             // debugList.Items.Add(directory.Name); //sans git si possible 
                foreach (DirectoryInfo dir in (new DirectoryInfo(directory.FullName).GetDirectories()))
                {
                    //debugList.Items.Add(dir.Name);
                    MenuItemInside = new ToolStripMenuItem();
                    MenuItemInside.Text = dir.Name;
                    MenuItemInside.Name = dir.FullName;
                    MenuItemInside.Click += new EventHandler(lesson_Click);
                    tempMenuItemReturn.DropDownItems.Add(MenuItemInside);
                }
                ReturnMenuItem.DropDownItems.Add(tempMenuItemReturn);
            }
            return ReturnMenuItem;
            //throw new NotImplementedException();
        }

        private void Window_Closing(object sender, CancelEventArgs e) //hide application
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            
        }
        private void minimizeButton_Click(object sender, System.EventArgs e)
        {
            
            this.Visibility = Visibility.Hidden;
        }


        private ToolStripMenuItem getMenuItem(ToolStripMenuItem m,DirectoryInfo[] menu) //must have a name
        {
            if (menu != null) //or m !=null ? 
            {
                foreach (DirectoryInfo dir in menu)
                {
                    if (!dir.Attributes.HasFlag(FileAttributes.Hidden)) //no folder hidden 
                    {
                        ToolStripMenuItem retMenu = new ToolStripMenuItem();
                        retMenu.Name = dir.FullName;
                        retMenu.Text = dir.Name;
                        retMenu.Click += new EventHandler(lesson_Click);
                        m.DropDownItems.Add(getMenuItem(retMenu, new DirectoryInfo(dir.FullName).GetDirectories()));
                        if (dir.GetFiles() != null) { getFilesIntoFolder(dir,retMenu); }

                    }
                } 
            }
               return m;
        }
        private void getFilesIntoFolder(DirectoryInfo dir, ToolStripMenuItem m)
        {
            
            FileInfo[] test = new DirectoryInfo(dir.FullName).GetFiles(); //fileinfo ? 

            foreach (FileInfo fil in test)
            {
                if (!fil.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    ToolStripMenuItem file = new ToolStripMenuItem();
                    file.Text = fil.Name;
                    file.Name = fil.FullName;
                    file.Click += new EventHandler(fileOnclick);
                    m.DropDownItems.Add(file);
                }
            }
        }

        private void fileOnclick(object sender, EventArgs e)
        {
            String fullname = ((ToolStripMenuItem)sender).Name;
            Process.Start(fullname);
        }
    }
}
