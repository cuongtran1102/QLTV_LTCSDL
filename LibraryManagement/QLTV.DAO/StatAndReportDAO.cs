using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QLTV.DAO
{
    public class StatAndReportDAO
    {
        public DataTable NumberBookInMonth(int month, int year)
        {
            SqlConnection conn = new Connection().Connect();
            conn.Open();
            String sql = "SELECT s.TenSach, COUNT(s.MaSach) AS SoLuongMuon\r\n" +
                "FROM PhieuMuon ps JOIN ChiTietPhieuMuon c ON ps.MaPM = c.MaPM JOIN Sach s ON c.MaSach = s.MaSach\r\n" +
                "WHERE MONTH(ps.NgayLapPhieu) = @month AND YEAR(ps.NgayLapPhieu) = @year\r\n" +
                "GROUP BY s.MaSach, s.TenSach";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataReader readdata = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(readdata);
            return data;
        }

        public DataTable NumberReaderInMonth(int month, int year)
        {
            SqlConnection conn = new Connection().Connect();
            conn.Open();
            String sql = "SELECT d.TenDocGia, COUNT(d.MaDocGia) AS SoLanMuonSach\r\n" +
                "FROM DocGia d JOIN PhieuMuon p ON d.MaDocGia = p.MaDocGia\r\n" +
                "WHERE MONTH(p.NgayLapPhieu) = @month AND YEAR(p.NgayLapPhieu) = @year\r\n" +
                "GROUP BY d.MaDocGia, d.TenDocGia";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataReader readdata = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(readdata);
            return data;
        }

        public DataTable RevenueOfBook(int year)
        {
            SqlConnection conn = new Connection().Connect();
            conn.Open();
            String sql = "SELECT s.TenSach, SUM(h.TienThanhToan) AS SubTotal\r\n" +
                "FROM Sach s JOIN ChiTietPhieuMuon c ON s.MaSach = c.MaSach\r\n\t" +
                "JOIN PhieuMuon p ON c.MaPM = p.MaPM\r\n\t" +
                "JOIN HoaDonTraSach H ON p.MaPM = h.MaPM\r\n" +
                "WHERE YEAR(h.NgayLapHD) = @year\r\n" +
                "GROUP BY s.TenSach";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataReader readdata = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(readdata);
            return data;
        }

        public DataTable SoLanMuonTrongNam(int year)
        {
            SqlConnection conn = new Connection().Connect();
            conn.Open();
            String sql = "\tSELECT  MONTH(NgayLapPhieu) AS Thang, COUNT(*) AS SoLanMuon " +
                "FROM PhieuMuon \r\n" +
                "WHERE YEAR(NgayLapPhieu) = @year\r\n" +
                "GROUP BY MONTH(NgayLapPhieu) \r\n" +
                "ORDER BY MONTH(NgayLapPhieu)";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataReader readdata = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(readdata);
            return data;
        }

        public DataTable Top5MuonNhieuNhat(int year)
        {
            SqlConnection conn = new Connection().Connect();
            conn.Open();
            String sql = "SELECT TOP 5 s.TenSach, COUNT(c.MaSach) AS SoLuongDangMuon\r\n" +
                "FROM Sach s \r\nJOIN ChiTietPhieuMuon c ON s.MaSach = c.MaSach \r\n" +
                "JOIN PhieuMuon p ON c.MaPM = p.MaPM\r\n" +
                "WHERE YEAR(p.NgayLapPhieu) = @year\r\n" +
                "GROUP BY s.TenSach\r\n" +
                "ORDER BY COUNT(c.MaSach) DESC";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataReader readdata = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(readdata);
            return data;
        }

        public DataTable Top5MuonItNhat(int year)
        {
            SqlConnection conn = new Connection().Connect();
            conn.Open();
            String sql = "SELECT TOP 5 s.TenSach, COUNT(c.MaSach) AS SoLuongDangMuon\r\n" +
                "FROM Sach s \r\n" +
                "JOIN ChiTietPhieuMuon c ON s.MaSach = c.MaSach \r\n" +
                "JOIN PhieuMuon p ON c.MaPM = p.MaPM\r\n" +
                "WHERE YEAR(p.NgayLapPhieu) = @year\r\n" +
                "GROUP BY s.TenSach\r\n" +
                "ORDER BY COUNT(c.MaSach) ASC";
            SqlCommand cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataReader readdata = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(readdata);
            return data;
        }
    }
}
