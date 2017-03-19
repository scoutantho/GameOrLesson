using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
      
        private String cheminIcon = "icon.ico";
        private String InfosFile = "infos.bin";


        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
         System.Windows.Forms.ContextMenuStrip contextMenu;
        System.Windows.Forms.ToolStripMenuItem optionMenuItem; //inside menuitem : game menu, open and courses

        List<Base> Infos = new List<Base>();

        //System.Windows.Controls.TextBox nomTab = new System.Windows.Controls.TextBox();
        //System.Windows.Controls.Button addTab = new System.Windows.Controls.Button();
        //System.Windows.Controls.Button rmTab = new System.Windows.Controls.Button();
        //Grid grilleParam = new Grid();



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
           
            
            Infos = deserialize();

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
        

        private void createItemsForContextMenu(ContextMenuStrip toAdd) { //option for only files
            foreach(Base item in Infos)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Text = item.getNom;
                if (item.getOnlyFiles) { getFilesIntoFolder(menuItem, new DirectoryInfo(item.getChemin)); }
                else
                {
                    if (new DirectoryInfo(item.getChemin).GetDirectories().Count() > 0)
                    {
                        menuItem = getMenuItem(menuItem, new DirectoryInfo(item.getChemin).GetDirectories());
                    }
                    else getFilesIntoFolder(menuItem, new DirectoryInfo(item.getChemin));
                }
                toAdd.Items.Add(menuItem);
               
            }
        }

        private void open_click(object sender, EventArgs e) //for see application, for options etc 
        {
            this.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Normal;
            UpDate(); //for see tab on listbox
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

       private void UpDate()
        {
            initModule(this.nIcon);
         
            foreach(Base item in Infos)
            {
                addElement(item.getNom);
               
            }
            serialize();
        }

        private void addTab_Click(object sender, RoutedEventArgs e)
        {
            bool ischeck=false;
            //if cancel 
            //changer add button, in 2 separate button witht one fore folders and 2nd for add

            // FolderBrowserDialog chemin = new FolderBrowserDialog();
            // DialogResult result = chemin.ShowDialog();
            //String cheminTab = chemin.SelectedPath;
            // String nom = chemin.SelectedPath;
            String nom = browse.Content.ToString();
            String cheminTab = browse.Content.ToString();

            if (nomTab != null) { nom = nomTab.Text; }
            if (checkBox.IsChecked.HasValue) { ischeck = checkBox.IsChecked.Value; }

            Infos.Add(new Base(nom, cheminTab, ischeck )); //add it in datatbase

           
            UpDate(); // update notifyicon
            nomTab.Text = ""; ///reset fields
            browse.Content = "browse";
            if (checkBox.IsChecked == true) { checkBox.IsChecked = false; }
            

        }

        private void removeTab_Click(object sender, RoutedEventArgs e)
        {
           
            foreach(Base item in Infos)
            {
                if (item.getNom == listBoxTab.SelectedItem.ToString()) { Infos.Remove(item); break; }
            }
            listBoxTab.Items.Remove(listBoxTab.SelectedItem.ToString());
            UpDate();
        }

        private void addElement(String nom) //add element to listboxitems
        {
            if (!listBoxTab.Items.Contains(nom))
            {
                listBoxTab.Items.Add(nom);
            }
            //gérer le click sur l'élément du listbox
           

        }
        //private void initVisual()
        //{
        //    RowDefinition rowDef1 = new RowDefinition();
        //    ColumnDefinition colDef1 = new ColumnDefinition();


        //    this.addTab = new System.Windows.Controls.Button();
        //    this.nomTab = new System.Windows.Controls.TextBox();
        //    this.rmTab = new System.Windows.Controls.Button();
        //    this.grilleParam = new Grid();
        //    grilleParam.Name="grille";
        //    grilleParam.ColumnDefinitions.Add(colDef1);
        //    grilleParam.RowDefinitions.Add(rowDef1);




        //    nomTab.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        //    nomTab.VerticalAlignment = VerticalAlignment.Top;
        //    nomTab.Name = "nomTab";
        //    nomTab.Height = 20;
        //    nomTab.Width = 100;
        //    Thickness m = nomTab.Margin;
        //    m.Top = 10;
        //    nomTab.Margin = m;
        //    grilleParam.Children.Add(nomTab);
        //    Grid.SetColumn(nomTab, 0);
        //    Grid.SetRow(nomTab, 0);

        //    addTab.Name = "addTab";
        //    addTab.Content = "Add";
        //    addTab.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        //    addTab.VerticalAlignment = VerticalAlignment.Top;
        //    addTab.Width = 60;
        //    Thickness m1 = addTab.Margin;
        //    m1.Top = 10;
        //    addTab.Margin = m1;
        //    addTab.Click += new RoutedEventHandler(addTab_Click);
        //    grilleParam.Children.Add(addTab);
        //    Grid.SetColumn(addTab, 1);
        //    Grid.SetRow(addTab, 0);

        //    rmTab.Name = "rmTab";
        //    rmTab.Content = "Remove";
        //    rmTab.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        //    rmTab.VerticalAlignment = VerticalAlignment.Top;
        //    rmTab.Width = 60;
        //    Thickness m2 = rmTab.Margin;
        //    m2.Top = 10;
        //    rmTab.Margin = m2;
        //    grilleParam.Children.Add(rmTab);

        //    Grid.SetColumn(rmTab, 2);
        //    Grid.SetRow(rmTab, 0);

            

        //}
        private void fileOnclick(object sender, EventArgs e)
        {
            String fullname = ((ToolStripMenuItem)sender).Name;
            Process.Start(fullname);
        }

        private void serialize()
        {
            using (Stream stream = File.Open(InfosFile, FileMode.Create))
            {
                var bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, Infos);
            }
        }

        private List<Base> deserialize()
        {
            if(File.Exists(InfosFile))
            {
                using (Stream stream = File.Open(InfosFile, FileMode.Open))
                {
                    var bformatter = new BinaryFormatter();
                    return (List<Base>)bformatter.Deserialize(stream);
                }
            }
            return new List<Base>();
          
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog chemin = new FolderBrowserDialog();
            DialogResult result = chemin.ShowDialog();
            browse.Content = chemin.SelectedPath;

            //envoyer chemin chez add tab ou récupérer chemin de addtab
        }
    }
}
