using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QLTV.BUS;

namespace LibraryManagement
{
    public partial class Form_Stat_Report : Form
    {
        private StatAndReportBUS stat;
        public Form_Stat_Report()
        {
            InitializeComponent();
            stat = new StatAndReportBUS();
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            PieChart_SoSachMuon();
            PieChart_DocGia();
            SplineRevenueOfBook();
            SplineTotalRentInYear();
            ColumnTop5();

        }

        // Số sách mượn trong 1 tháng
        private void PieChart_SoSachMuon()
        {
            PieSoSachMuon.Series.Clear();
            PieSoSachMuon.Titles.Clear();
            PieSoSachMuon.Titles.Add("Báo cáo số sách mượn trong tháng 4 năm 2023");
            PieSoSachMuon.Series.Add("SoSachMuon");
            PieSoSachMuon.Series["SoSachMuon"].ChartType = SeriesChartType.Doughnut;
            PieSoSachMuon.Series["SoSachMuon"].IsValueShownAsLabel = true;
            DataTable data = stat.NumberBookInMonth(4, 2023);
            foreach (DataRow row in data.Rows) 
            {
                PieSoSachMuon.Series["SoSachMuon"].Points.AddXY(row["TenSach"], row["SoLuongMuon"]);
            }
        }

        private void PieChart_DocGia()
        {
            PieReader.Series.Clear();
            PieReader.Titles.Clear();
            PieReader.Titles.Add("Báo cáo số lần độc giả mượn sách trong tháng 4 năm 2023");
            PieReader.Series.Add("DocGia");
            PieReader.Series["DocGia"].ChartType = SeriesChartType.Doughnut;
            PieReader.Series["DocGia"].IsValueShownAsLabel = true;
            DataTable data = stat.NumberReaderInMonth(4, 2023);
            foreach (DataRow row in data.Rows)
            {
                PieReader.Series["DocGia"].Points.AddXY(row["TenDocGia"], row["SoLanMuonSach"]);
            }
        }

        private void SplineTotalRentInYear()
        {
            SplineYear.Series.Clear();
            SplineYear.Titles.Clear();
            SplineYear.Titles.Add("Báo cáo số lần mượn của từng quyển sách qua 1 năm");
            Series series = SplineYear.Series.Add("Tháng");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 2;
            series.MarkerSize = 100;

            // Thêm dữ liệu vào loại biểu đồ
            for(int i = 1; i <= 12; i++)
            {
                int rows = 0;
                double yValue;
                DataRow[] foundRows = stat.SoSachMuonInYear(2023).Select($"Thang = {i}");
                yValue = foundRows.Length > 0 ? Convert.ToDouble(stat.SoSachMuonInYear(2023).Rows[rows]["SoLanMuon"]) : 0;
                string xValue = $"{i}";
                series.Points.AddXY(xValue, yValue);
                rows++;
            }

        }

        private void SplineRevenueOfBook()
        {
            ColumnRevenue.Series.Clear();
            ColumnRevenue.Titles.Clear();
            ColumnRevenue.Titles.Add("Báo cáo Doanh Thu Mượn Sách");
            ColumnRevenue.Series.Add("Revenue");
            ColumnRevenue.Series["Revenue"].ChartType = SeriesChartType.Column;
            DataTable data = stat.RevenueOfBook(2023);
            foreach (DataRow row in data.Rows)
            {
                ColumnRevenue.Series["Revenue"].Points.AddXY(row["TenSach"], row["SubTotal"]);
            }

        }

        private void ColumnTop5()
        {
            BarMax.Series.Clear();
            BarMax.Titles.Clear();
            BarMax.Titles.Add("Số Lượng Sách mượn nhiều nhất trong năm 2023");
            BarMax.Series.Add("Max");
            BarMax.Series["Max"].ChartType = SeriesChartType.Bar;
            DataTable data_Max = stat.Top5MuonNhieuNhat(2023);
            foreach (DataRow row in data_Max.Rows)
            {
                BarMax.Series["Max"].Points.AddXY(row["TenSach"], row["SoLuongDangMuon"]);
            }


            BarMin.Series.Clear();
            BarMin.Titles.Clear();
            BarMin.Titles.Add("Số Lượng Sách mượn ít nhất trong năm 2023");
            BarMin.Series.Add("Min");
            BarMin.Series["Min"].ChartType = SeriesChartType.Bar;
            DataTable data_Min = stat.Top5MuonItNhat(2023);

            foreach (DataRow row in data_Min.Rows)
            {
                BarMin.Series["Min"].Points.AddXY(row["TenSach"], row["SoLuongDangMuon"]);
            }
        }
    }
}
