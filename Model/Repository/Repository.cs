using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using FinPlanProject.Model.Context;

namespace FinPlanProject.Model.Repository
{
    public class Repository
    {
        private readonly OleDbConnection connection;

        public Repository(DbContext context)
        {
            connection = context.Conn;
        }

        public List<string[]> RetrieveData()
        {
            List<string[]> data = new List<string[]>();

            try
            {
                connection.Open();
                string query = "SELECT * FROM tbl_pemasukan"; // Change this query to fetch data from
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] row = new string[]
                            {
                        reader["Tgl_pemasukan"].ToString(),
                        reader["sumber"].ToString(),
                        reader["jumlah"].ToString(),
                        reader["keterangan"].ToString(),
                        reader["jenis"].ToString()
                            };
                            data.Add(row);
                        }
                    }
                }
            }
            catch (OleDbException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return data;
        }

        public bool Simpan(string sumber, DateTime tanggal, decimal jumlah, string keterangan, string jenis)
        {
            try
            {
             
                
                    string insertQuery = "INSERT INTO tbl_pemasukan(sumber, [Tgl_pemasukan], [jumlah], keterangan, jenis) VALUES(@sumber, @Tgl_pemasukan, @jumlah, @keterangan, @jenis)";
                    using (OleDbCommand cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = insertQuery;
                        cmd.Parameters.AddWithValue("@sumber", sumber);
                        cmd.Parameters.AddWithValue("@Tgl_pemasukan",tanggal);
                        cmd.Parameters.AddWithValue("@jumlah",jumlah);
                        cmd.Parameters.AddWithValue("@keterangan", keterangan);
                        cmd.Parameters.AddWithValue("@jenis",jenis);
                        cmd.ExecuteNonQuery();

                    }

                return true;
            }
            catch (OleDbException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public bool RegisterUser(string username, string password, string name, string confirmPassword, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Username and password fields cannot be empty");
                }
                else if (password != confirmPassword)
                {
                    throw new ArgumentException("Passwords do not match, please re-enter");
                }
                else
                {
                    using (OleDbCommand cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = "INSERT INTO tbl_user (username, [password], name_user, email_user) VALUES (@username, @password, @name_user, @email_user)";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@name_user", name);
                        cmd.Parameters.AddWithValue("@email_user", email);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (OleDbException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public bool LoginUser(string username, string password)
        {
            bool loginSuccessful = false;

            try
            {
                using (OleDbCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    string loginQuery = "SELECT COUNT(*) FROM tbl_user WHERE username = @username AND [password] = @password";
                    cmd.CommandText = loginQuery;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        loginSuccessful = true;
                        connection.Close();
                    }
                    else
                    {
                        loginSuccessful = false;

                    }

                }
                return loginSuccessful;
            }
            catch (OleDbException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
          
        }
    }
    }
