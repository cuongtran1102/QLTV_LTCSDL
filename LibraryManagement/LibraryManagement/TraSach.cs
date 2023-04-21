using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLTV.DTO;
using QLTV.BUS;

namespace LibraryManagement
{
    public partial class TraSach : Form
    {
        public TraSach()
        {
            InitializeComponent();
        }

        private void TraSach_Load(object sender, EventArgs e)
        {
            dgPhieuMuon.AllowUserToAddRows = false;
            dgPhieuMuon.DataSource = new PhieuMuonBUS().LoadPhieuMuon();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dgPhieuMuon.DataSource = new PhieuMuonBUS().LoadPhieuMuonByIDDG(txtTimKiem.Text);
        }

        private void dgPhieuMuon_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dgPhieuMuon.SelectedRows[0];
            lblMaPM.Text = row.Cells["MaPM"].Value.ToString();
            lblNgayLapPhieu.Text = row.Cells["NgayLapPhieu"].Value.ToString();
            lblMaDocGia.Text = row.Cells["MaDocGia"].Value.ToString();
            DocGia dg = new PhieuMuonBUS().LoadDocGiaByIDPM(lblMaDocGia.Text);
            lblTenDocGia.Text = dg.TenDocGia;
            lblNgaySinh.Text = dg.NgaySinh.ToString();
            lblSoDT.Text = dg.SDT;
        }
    }
}
