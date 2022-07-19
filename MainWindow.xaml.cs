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


namespace sql_contactslist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        List<SQLDatabaseProperties> people_results = new List<SQLDatabaseProperties>();

        static private string sqlConnectionString = "Data Source=localhost;Integrated Security = True";
        DatabaseHelper dbUsers = new DatabaseHelper(sqlConnectionString);
        public MainWindow()
        {
            

        }//end main

        private void RefreshListBoxBinding()
        {
            //SET BINDING INSTANCE FOR LISTBOX
            lstbox.ItemsSource = people_results;

            //SET BINDING FIELD FOR LISTBOX
            lstbox.DisplayMemberPath = "resultData";
        }

        private void Display_Info(object sender, SelectionChangedEventArgs e)
        {



            if (lstbox.SelectedIndex > -1)
            {

                SQLDatabaseProperties person = people_results.ElementAt(lstbox.SelectedIndex);
                //display properties in appropriate textboxes
                firstname.Text = person.firstName.ToString();
                lastname.Text = person.lastName.ToString();

            }//end if
            RefreshListBoxBinding();
        }//end event
        private void Search_Contact(object sender, RoutedEventArgs e)
        {
            
            //USE DATA ACCESS INSTANCE TO GET PEOPLE DATA FROM THE DB
            people_results = dbUsers.GetPeople(txtbox.Text);

            //REFRESH LIST BOX
            RefreshListBoxBinding();
        }//end event
    }//end class
}//end namespace
