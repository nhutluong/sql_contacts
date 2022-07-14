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
    static private string sqlConnectionString = "Data Source=localhost;Integrated Security = True";
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DatabaseHelper dbUsers = new DatabaseHelper(sqlConnectionString);


        }//end main
    }//end class
}//end namespace
