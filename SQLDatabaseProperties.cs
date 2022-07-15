using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sql_contactslist
{
    internal class SQLDatabaseProperties
    {
        public int id { get; set; } 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; } 
        public string nickNamwe { get; set; }
        public string email { get; set; }   
        public string address { get; set; }
        public string birthday { get; set; }    
        public string notes { get; set; }
        public string company { get; set; }
        public Boolean favorites { get; set; }
        public Boolean active { get; set; }

        public string resultData
        {
            get { return firstName + " " + lastName; }
        }

    }
}
