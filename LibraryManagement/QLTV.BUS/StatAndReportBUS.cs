using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLTV.DAO;
namespace QLTV.BUS
{
    public class StatAndReportBUS
    {
        private StatAndReportDAO stat = new StatAndReportDAO();
        public DataTable NumberBookInMonth(int month, int year)
        {
            return stat.NumberBookInMonth(month, year);
        }

        public DataTable NumberReaderInMonth(int month, int year)
        {
            return stat.NumberReaderInMonth(month, year);
        }

        public DataTable RevenueOfBook(int year)
        {
            return stat.RevenueOfBook(year);
        }

        public DataTable SoSachMuonInYear(int year)
        {
            return stat.SoLanMuonTrongNam(year);
        }

        public DataTable Top5MuonNhieuNhat(int year)
        {
            return stat.Top5MuonNhieuNhat(year);
        }

        public DataTable Top5MuonItNhat(int year)
        {
            return stat.Top5MuonItNhat(year);
        }
    }
}
