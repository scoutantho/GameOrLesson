using System;
using System.Collections;
using System.Collections.Generic;
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
      
         FileInfo[] Games = new DirectoryInfo(@"F:\").GetFiles("*.lnk"); //only games and TS 
        DirectoryInfo gameDir = new DirectoryInfo(@"F:\");
        DirectoryInfo[] lecteur = new DirectoryInfo(@"D:\Annee 4\").GetDirectories();
        private String cheminIcon = @"C:\Users\antho\Desktop\GameOrLesson\GameOrLesson\icon.ico";


        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
         System.Windows.Forms.ContextMenuStrip contextMenu;
        System.Windows.Forms.ToolStripMenuItem GameMenu,  Courses, toolStripMenuItem;
        System.Windows.Forms.ToolStripMenuItem optionMenuItem; //inside menuitem : game menu, open and courses

        List<Base> Infos = new List<Base>();
       

        ContextMenuStrip menu = new ContextMenuStrip();
       

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
            Infos.Add(new Base("Jeux", @"F:\",true));
            Infos.Add(new Base("Cours", @"D:\Annee 4\"));

            initModule(nIcon);

            InitializeComponent();
            
            this.Window_Closing(this, new CancelEventArgs()) ;

            


        }

        private void initModule(NotifyIcon nIcon)
        {
            nIcon.Icon = new System.Drawing.Icon(cheminIcon);
            nIcon.Visible = true;
            nIcon.DoubleClick += new EventHandler(nIcon_DoubleClick);

            this.contextMenu = new System.Windows.Forms.ContextMenuStrip();
            
            this.optionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            optionMenuItem.Text = "open";
            optionMenuItem.Click += new EventHandler(this.open_click);

            contextMenu.Items.Add(optionMenuItem);

            createItemsForContextMenu(contextMenu);

            nIcon.ContextMenuStrip = contextMenu;
        }

        private void createItemsForContextMenu(ContextMenuStrip toAdd) { //option only files
            foreach(Base item in Infos)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Text = item.getNom;
                if (item.getOnlyFiles) { getFilesIntoFolder(menuItem,new DirectoryInfo(item.getChemin)); }
                else { menuItem = getMenuItem(menuItem, new DirectoryInfo(item.getChemin).GetDirectories());  }
                
                toAdd.Items.Add(menuItem);
            }
        }

        private void open_click(object sender, EventArgs e) //for see application, for options etc 
        {
            this.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Normal;
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
                        if (dir.GetFiles() != null) { getFilesIntoFolder(retMenu,dir); }

                    }
                } 
            }
               return m;
        }

        private void getFilesIntoFolder(ToolStripMenuItem m,DirectoryInfo dir )
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
