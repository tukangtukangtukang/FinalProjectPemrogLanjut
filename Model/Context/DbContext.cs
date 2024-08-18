using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace FinPlanProject.Model.Context
{
    public class DbContext : IDisposable
    {
        private OleDbConnection _conn;

        public OleDbConnection Conn
        {
            get { return _conn ?? (_conn = GetOpenConnection()); }
        }

        private OleDbConnection GetOpenConnection()
        {
            OleDbConnection conn = null;
            try
            {
                //string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\userdatabase2.mdb");
                string databasePath = "D:\\kuliah_difa\\Semester_3\\pemrog\\ini uas\\FinPlanProject_Dawam\\FinPlanProject\\bin\\Debug\\Database\\userdatabase2.mdb";
                string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={databasePath}";
                conn = new OleDbConnection(connectionString);
                conn.Open();
                return conn; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Open Connection Error: {0}", ex.Message);
                conn?.Dispose(); 
                throw; 
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                if (_conn.State != ConnectionState.Closed)
                {
                    _conn.Close();
                    _conn.Dispose();
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
