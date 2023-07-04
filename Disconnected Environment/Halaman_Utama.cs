using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class Halaman_Utama : Form
    {
        public Halaman_Utama()
        {
            InitializeComponent();
        }

        private void dataProdiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data_Prodi fe = new Data_Prodi();
            fe.Show();
            this.Hide();
        }

        private void dataMahasiswaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Halaman_Data_Mahasiswa fo = new Halaman_Data_Mahasiswa();
            fo.Show();
            this.Hide();
        }

        private void dataStatusMahasiswaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data_Status_Mahasiswa fr = new Data_Status_Mahasiswa();
            fr.Show();
            this.Hide();
        }

        private void Halaman_Utama_Load(object sender, EventArgs e)
        {

        }
    }
}
