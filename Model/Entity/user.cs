using System.Data.OleDb;
using FinPlanProject.Model.Repository;

namespace FinPlanProject.Model.Entity
{    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Tgl_pemasukan { get; set; }
        public string sumber { get; set; }
        public string jumlah { get; set; }
        public string keterangan { get; set; }
        public string jenis { get; set; }

    }
}
