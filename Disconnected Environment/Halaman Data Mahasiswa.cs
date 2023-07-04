using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class Halaman_Data_Mahasiswa : Form
    {
        string connectionString = "data source = LAPTOP-CK57KRTO;database=Prodi;MultipleActiveResultSets=True;User ID = sa; Password = Mudah123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource customerBindingsSource = new BindingSource();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNim.Text = "";
            txtNama.Text = "";
            txtAlamat.Text = "";
            dtTanggalLahir.Value = DateTime.Today;
            txtNim.Enabled = true;
            txtNama.Enabled = true;
            cbxJK.Enabled = true;
            txtAlamat.Enabled = true;
            dtTanggalLahir.Enabled = true;
            cbxProdi.Enabled = true;
            Prodicbx();
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnAdd.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            nim = txtNim.Text;
            nama = txtNama.Text;
            jk = cbxJK.Text;
            alamat = txtAlamat.Text;
            tgl = dtTanggalLahir.Value;
            prodi = cbxProdi.Text;
            int hs = 0;
            koneksi.Open();
            string strs = "select id_prodi from dbo.prodi where nama_prodi = @dd";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@dd", prodi));
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                hs = int.Parse(dr["id_prodi"].ToString());
            }
            dr.Close();
            string str = "insert into dbo.mahasiswa (nim, nama_mahasiswa, Jenis_kelamin  , Alamat, tgl_lahir, Id_prodi)" + "values(@nim, @nama, @jk, @Alamat, @tgl, @idp)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("NIM", nim));
            cmd.Parameters.Add(new SqlParameter("nama", nama));
            cmd.Parameters.Add(new SqlParameter("jk", jk));
            cmd.Parameters.Add(new SqlParameter("Alamat", alamat));
            cmd.Parameters.Add(new SqlParameter("tgl", tgl));
            cmd.Parameters.Add(new SqlParameter("idp", hs));
            cmd.ExecuteNonQuery();

            koneksi.Close();

            MessageBox.Show("data berhasil disimpan", "sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshform();
        }

        BindingSource customerBindingSource = new BindingSource();

        public Halaman_Data_Mahasiswa()
        {
            InitializeComponent();
            koneksi = new SqlConnection(connectionString);
            this.bnMahasiswa.BindingSource = this.customerBindingSource;
            refreshform();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bnMahasiswa_Load(object sender, EventArgs e)
        {

        }

        private void clearBinding()
        {
            this.txtNim.DataBindings.Clear();
            this.txtNama.DataBindings.Clear();
            this.txtAlamat.DataBindings.Clear();
            this.cbxJK.DataBindings.Clear();
            this.dtTanggalLahir.DataBindings.Clear();
            this.cbxProdi.DataBindings.Clear();
        }

        private void Halaman_Data_Mahasiswa_Load()
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("select m.nim, m.nama_mahasiswa," +
                "m.alamat, m.jenis_kelamin, m.tgl_lahir, p.nama_prodi from dbo.mahasiswa m " +
                "join dbo.prodi p on m.id_prodi = p.id_prodi", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.customerBindingSource.DataSource = ds.Tables[0];
            this.txtNim.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "NIM", true));
            this.txtNama.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "nama_mahasiswa", true));
            this.txtAlamat.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "alamat", true));
            this.cbxJK.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "jenis_kelamin", true));
            this.dtTanggalLahir.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "tgl_lahir", true));
            this.cbxProdi.DataBindings.Add(
                new Binding("Text", this.customerBindingSource, "nama_prodi", true));
            koneksi.Close();
        }

        private void bnMahasiswa_RefreshItems(object sender, EventArgs e)
        {

        }


        private void refreshform()
        {
            txtNim.Enabled = false;
            txtNama.Enabled = false;
            cbxJK.Enabled = false;
            txtAlamat.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
            Halaman_Data_Mahasiswa_Load();
        }

        private void Prodicbx()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxProdi.DisplayMember = "nama_prodi";
            cbxProdi.ValueMember = "id_prodi";
            cbxProdi.DataSource = ds.Tables[0];
        }

        private void FormDataMahasiswa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Halaman_Utama fm = new Halaman_Utama();
            fm.Show();
            this.Hide();
        }

    }
}
