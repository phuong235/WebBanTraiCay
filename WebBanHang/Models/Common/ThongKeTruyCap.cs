using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBanHang.Models.Common
{
    public class ThongKeTruyCap
    {

        private static string strConnect = ConfigurationManager.ConnectionStrings["WebBanHangEntities"].ToString();

        public static StatisticViewModel ThongKe()
        {
            if (strConnect.ToLower().StartsWith("metadata="))
            {
                System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder efBuilder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(strConnect);
                strConnect = efBuilder.ProviderConnectionString;
            }
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<StatisticViewModel>("sp_ThongKe", CommandType.StoredProcedure);
                return item;
            }
        }

     
        public List<ReportProduct> monthlyRevenue()
        {

            List<ReportProduct> objList = new List<ReportProduct>();
            if (strConnect.ToLower().StartsWith("metadata="))
            {
                System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder efBuilder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(strConnect);
                strConnect = efBuilder.ProviderConnectionString;
            }
            using (var connection = new SqlConnection(strConnect))
            {
                string query = "sp_GetMonthlyRevenue";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        objList.Add(new ReportProduct()
                        {
                            Date = rd["Thang"].ToString(),
                            Total = rd["Total"].ToString(),
                        });
                    }
                }
            }
            return objList;
        }

        public List<ReportProduct> revenueByYear()
        {
            List<ReportProduct> objList = new List<ReportProduct>();
            if (strConnect.ToLower().StartsWith("metadata="))
            {
                System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder efBuilder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(strConnect);
                strConnect = efBuilder.ProviderConnectionString;
            }
            using (var connection = new SqlConnection(strConnect))
            {
                string query = "sp_GetRevenueByYear";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        objList.Add(new ReportProduct()
                        {
                            Date = rd["Nam"].ToString(),
                            Total = rd["Total"].ToString(),
                        });
                    }
                }
            }
            return objList;
        }

        public List<ReportProduct> revenueByQuarter()
        {
            List<ReportProduct> objList = new List<ReportProduct>();
            if (strConnect.ToLower().StartsWith("metadata="))
            {
                System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder efBuilder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(strConnect);
                strConnect = efBuilder.ProviderConnectionString;
            }
            using (var connection = new SqlConnection(strConnect))
            {
                string query = "sp_GetRevenueByQuarter";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        objList.Add(new ReportProduct()
                        {
                            Date = rd["Quy"].ToString(),
                            Total = rd["Total"].ToString(),
                        });
                    }
                }
            }
            return objList;
        }
        public List<ReportProduct> TopBanChay()
        {
            List<ReportProduct> objList = new List<ReportProduct>();
            using (var connection = new SqlConnection(strConnect))
            {
                string query = "sp_TopSellingProducts";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        objList.Add(new ReportProduct()
                        {
                            Product = rd["product"].ToString(),
                            Quantity = int.Parse(rd["quantity"].ToString()),
                        });
                    }
                }
            }
            return objList;
        }
    }
}