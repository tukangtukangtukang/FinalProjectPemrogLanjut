using FinPlanProject.Model.Context;
using FinPlanProject.Model.Repository;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using FinPlanProject.Model.Entity;
using System.Data.SQLite;

namespace FinPlanProject
{
    public partial class Homepage : Form
    {

        List<User> users = new List<User>();

        private DateTime selectedDateTime;
        public Homepage()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Primary");
            comboBox1.Items.Add("Secondary");
            comboBox1.Items.Add("Saving");
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            InitializeListView();
            loadData();
        }

        private OleDbConnection GetOpenConnection()
        {
            OleDbConnection conn = null;

            try
            {
                string dbName = @"D:\kuliah_difa\Semester_3\pemrog\ini uas\FinPlanProject_Dawam\FinPlanProject\bin\Debug\Database\userdatabase2.mdb";
                string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={dbName}";
                conn = new OleDbConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedValue = comboBox1.SelectedItem.ToString();
            MessageBox.Show("Pilihan Anda: " + selectedValue);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            selectedDateTime = dateTimePicker1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sumber = sumbertxt.Text; // Ubah sumberTxt sesuai dengan 
                DateTime tanggal = dateTimePicker1.Value; // Ubah dateTimePicker1 sesuai dengan DateTimePicker Anda
                decimal jumlah = Convert.ToDecimal(jumrptxt.Text); // Ubah jumrptxt sesuai dengan TextBox jumlah Anda
                string keterangan = keterangantxt.Text; // Ubah keterangantxt sesuai dengan TextBox keterangan Anda
                string jenis = comboBox1.SelectedItem.ToString(); // Mengambil nilai ComboBox

                DbContext dbContext = new DbContext();
                Repository repository = new Repository(dbContext);
                bool simpanSuccessful = repository.Simpan(sumber, selectedDateTime, jumlah, keterangan, jenis);

                if (simpanSuccessful)
                {
                    MessageBox.Show("Data berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sumbertxt.Text = ""; 
            jumrptxt.Text = "";
            persentxt.Text = "";
            keterangantxt.Text = ""; 
            comboBox1.SelectedIndex =0; 
        }
        private void InitializeListView()
        {
            // Your existing code to initialize ListView columns
            listView1.View = System.Windows.Forms.View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("No.", 30, HorizontalAlignment.Center);
            listView1.Columns.Add("Waktu", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Sumber Dana", 190, HorizontalAlignment.Left);
            listView1.Columns.Add("Jumlah", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Keterangan", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Sisa Saldo", 70, HorizontalAlignment.Center);
        }

        private void loadData() 
        {
            listView1.Items.Clear();
            OleDbConnection conn = GetOpenConnection();

            string query = @"select * from tbl_pemasukan";

            OleDbCommand cmd = new OleDbCommand(query, conn);

            OleDbDataReader dtr = cmd.ExecuteReader();

            while(dtr.Read())
            {
                var noUrut = listView1.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(dtr["Tgl_pemasukan"].ToString());
                item.SubItems.Add(dtr["sumber"].ToString());
                item.SubItems.Add(dtr["jumlah"].ToString());
                item.SubItems.Add(dtr["keterangan"].ToString());
                item.SubItems.Add(dtr["jenis"].ToString());
                listView1.Items.Add(item);
            }
            conn.Close();
            //try
            //{
            //    DbContext dbContext = new DbContext();
            //    Repository repository = new Repository(dbContext);

            //    var data = repository.RetrieveData();

            //    foreach (var row in data)
            //    {
            //        int count = listView1.Items.Count + 1;
            //        ListViewItem item = new ListViewItem(count.ToString());
            //        item.SubItems.Add(row.sumber);
            //        //item.SubItems.Add(row.)
            //        Console.WriteLine(row);
            //        listView1.Items.Add(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
