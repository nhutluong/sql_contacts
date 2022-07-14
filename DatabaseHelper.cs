using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace sql_contactslist
{
    class DatabaseHelper
    {

        private string _connectionString;
        private bool _isConnected = false;
        private SqlConnection _dbConnection;
        private SqlCommand _sqlCommand;

        public string ConnectionString
        {
            get { return _connectionString; }
        }//end prop

        public bool isConnected
        {
            get { return GetCurrentConnectionStatus(); }
        }//end prop

        public DatabaseHelper(string connectionString, bool connectNow = true)
        {
            _connectionString = connectionString;

            if (connectNow)
            {
                Connect();
            }
        }//end constructor

        public bool Connect()
        {
            try
            {
                _dbConnection = new SqlConnection(_connectionString);
                _isConnected = true;
            }
            catch
            {

                _isConnected = false;
            }

            return _isConnected;
        }//end method

        public object[][] ExecuteReader(string sqlStatement)
        {
            SqlDataReader queryReturnData = null;
            object[][] returnData = null;
            try
            {
                if (isConnected)
                {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(sqlStatement, _dbConnection);
                    queryReturnData = _sqlCommand.ExecuteReader();
                    returnData = ConvertDataReaderto2DArray(queryReturnData);

                    _dbConnection.Close();
                }//end if
            }
            catch (SqlException)
            {

                throw new Exception("Invalid SQL");
            }


            return returnData;

        }//end method

        public int ExecuteNonQuery(string sqlStatement)
        {
            int recordsAffected = -1;

            try
            {
                if (isConnected)
                {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(sqlStatement, _dbConnection);
                    recordsAffected = _sqlCommand.ExecuteNonQuery();
                    _dbConnection.Close();
                }

            }
            catch (SqlException)
            {

                throw new Exception("Invalid SQL");
            }
            return recordsAffected;
        }

        public int GetTableRecordCount(string tableName)
        {
            string stmt = string.Format("SELECT COUNT(*) FROM {0}", tableName);

            int count = 0;

            try
            {
                if (isConnected)
                {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(stmt, _dbConnection);
                    count = (int)_sqlCommand.ExecuteScalar();
                    _dbConnection.Close();
                }//end if            

                return count;

            }
            catch (SqlException)
            {

                throw new Exception("Invalid sql");
            }


        }//end method

        public bool FlushTable(string tableName)
        {
            bool flushed = false;

            string stmt = string.Format("DELETE FROM [" + tableName + "] ");


            try
            {
                if (isConnected)
                {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(stmt, _dbConnection);
                    _sqlCommand.ExecuteNonQuery();
                    _dbConnection.Close();
                    flushed = true;
                }//end if            

                return flushed;

            }
            catch (SqlException)
            {

                throw new Exception("Invalid sql");
            }
        }//end method

        public bool DeleteTable(string tableName)
        {
            bool deleted = false;

            string stmt = string.Format("DROP TABLE [" + tableName + "] ");

            try
            {
                if (isConnected)
                {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(stmt, _dbConnection);
                    _sqlCommand.ExecuteNonQuery();
                    _dbConnection.Close();
                    deleted = true;
                }//end if            

                return deleted;

            }
            catch (SqlException)
            {

                throw new Exception("Invalid sql");
            }
        }//end method

        public bool AddTable(string tablename)
        {
            bool createdTable = false;

            string stmt = string.Format("create table <Table Name>(firstName varchar(10),lastName varchar(10))");
            try
            {
                if (isConnected)
                {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(stmt, _dbConnection);
                    _sqlCommand.ExecuteNonQuery();
                    _dbConnection.Close();
                    createdTable = true;
                }

                return createdTable;

            }
            catch (SqlException)
            {

                throw new Exception("Invalid sql");
            }

        }//end method

        public bool Connect(string newConnectionString)
        {//overload to connect to new db

            bool connect = false;
            try
            {
                if (connect == false)
                {
                    _dbConnection = new SqlConnection(newConnectionString);
                    connect = true;
                }

            }
            catch (Exception)
            {

                connect = false;
            }

            return connect;

        }//end method

        private object[][] ConvertDataReaderto2DArray(SqlDataReader data)
        {

            //Declare 2d array
            object[,] returnData = null; // 
            List<object[]> rows = new List<object[]>();

            while (data.Read())
            {
                object[] newRow = new object[data.FieldCount];
                for (int fieldIndex = 0; fieldIndex < data.FieldCount; fieldIndex++)
                {
                    newRow[fieldIndex] = data[fieldIndex];
                }//end for
                rows.Add(newRow);

            }//end while

            return rows.ToArray();


        }//end method


        private bool GetCurrentConnectionStatus()
        {
            bool pastConnection = _dbConnection != null;
            bool currentlyConnected = false;

            if (pastConnection == true)
            {
                currentlyConnected = _dbConnection.State != System.Data.ConnectionState.Broken;
            }
            return currentlyConnected;
        }//end method

    }//end class
}//end namespace
