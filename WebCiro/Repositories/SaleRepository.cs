using System.Data;
using System.Data.SqlClient;
using WebCiro.Models;

namespace WebCiro.Repositories
{
    public class SaleRepository
    {
        private string connectionString;
        private DateTime referenceDate = DateTime.Now;
        int Rn = 0;

        public SaleRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //public List<WI_DashboardTbl> GetSales()
        //{
        //    List<WI_DashboardTbl> sales = new List<WI_DashboardTbl>();



        //    var data = GetLatestSaleFromDb();

        //    foreach (DataRow row in data.Rows)
        //    {
        //        WI_DashboardTbl sale = new WI_DashboardTbl
        //        {
        //            Id = Convert.ToInt32(row["Id"]),
        //            CustomerName = row["CustomerName"].ToString(),
        //            CustomerCode = row["CustomerCode"].ToString(),

        //            OrderDate = row["OrderDate"] != DBNull.Value ? Convert.ToDateTime(row["OrderDate"]) : (DateTime?)null,

        //            OrderNumber = row["OrderNumber"].ToString(),
        //            City = row["City"].ToString(),
        //            ProductName = row["ProductName"].ToString().Substring(0, Math.Min(25, row["ProductName"].ToString().Length)),
        //            TotalTl = Convert.ToDouble(row["TotalTl"].ToString()),
        //            VehicleType = row["VehicleType"].ToString(),
        //            VehicleBrand = row["VehicleBrand"].ToString(),
        //            PartBrand = row["PartBrand"].ToString(),
        //            Category = row["Category"].ToString(),
        //            Status = row["Status"].ToString(),
        //        };

        //        sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetLatestSaleFromDb()
        //{

        //    var query = "SELECT [Id], [CustomerName], [CustomerCode], [OrderDate], [OrderNumber], [City], [ProductName], [TotalTl], [VehicleType], [VehicleBrand], [PartBrand], [Category], [Status] FROM [WebCiroTest].[dbo].[WI_DashboardTbl] WHERE [OrderDate] > GETDATE() ORDER BY [OrderDate];";
        //    DataTable dataTable = new DataTable();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                dataTable.Load(reader);
        //            }

        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Hata işleme eklenebilir (loglama veya kullanıcıya bilgilendirme)
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}

        public List<WI_DashboardTbl> GetSales()
        {
            List<WI_DashboardTbl> sales = new List<WI_DashboardTbl>();

            // Use the current date and time as a reference

            var data = GetLatestSaleFromDb(referenceDate);

            foreach (DataRow row in data.Rows)
            {
                WI_DashboardTbl sale = new WI_DashboardTbl
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Rn = Convert.ToInt32(row["Rn"]),
                    CustomerName = row["CustomerName"].ToString(),
                    CustomerCode = row["CustomerCode"].ToString(),
                    OrderDate = row["OrderDate"] != DBNull.Value ? Convert.ToDateTime(row["OrderDate"]) : (DateTime?)null,
                    OrderNumber = row["OrderNumber"].ToString(),
                    City = row["City"].ToString(),
                    ProductName = row["ProductName"].ToString().Substring(0, Math.Min(25, row["ProductName"].ToString().Length)),
                    TotalTl = Convert.ToDouble(row["TotalTl"].ToString()),
                    VehicleType = row["VehicleType"].ToString(),
                    VehicleBrand = row["VehicleBrand"].ToString(),
                    PartBrand = row["PartBrand"].ToString(),
                    Category = row["Category"].ToString(),
                    Status = row["Status"].ToString(),

                };
                sales.Add(sale);
            }
            return sales;
        }
        private DataTable GetLatestSaleFromDb(DateTime referenceDate)
        {


            //var query = $" SELECT * FROM WI_DashboardTbl order by rn ";
            var query = $"SELECT * FROM WI_DashboardTbl where OrderDate >  CONVERT(datetime, @ReferenceDate,104) order by rn";
            //var query = "SELECT TOP(1)* FROM WI_DashboardTbl WHERE  OrderDate >  CONVERT(datetime, @ReferenceDate,104) ORDER BY OrderDate desc";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {

                    command.Parameters.AddWithValue("@ReferenceDate", referenceDate);
                    //command.CommandText = "UPDATE WI_DashboardTbl SET RN=T.RN FROM ( SELECT ROW_NUMBER() OVER( ORDER BY ORDERDATE) RN ,Id FROM WI_DashboardTbl)T WHERE T.Id = WI_DashboardTbl.Id";
                    command.CommandText = "UPDATE WI_DashboardTbl SET RN=T.RN FROM ( SELECT ROW_NUMBER() OVER( ORDER BY ORDERDATE) RN ,Id FROM WI_DashboardTbl where OrderDate >  CONVERT(datetime, @ReferenceDate,104) )T WHERE T.Id = WI_DashboardTbl.Id";
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    command.CommandText = query;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }


        //public List<WRH_OUT> MapGazianteptDb()
        //{
        //    List<WRH_OUT> sales = new List<WRH_OUT>();
        //    var data = GetMapGaziantepDbFromDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        WRH_OUT sale = new WRH_OUT
        //        {
        //            WarehouseName = Convert.ToString(row["WarehouseName"]),
        //            NetTotal = Convert.ToDecimal(row["NetTotal"]),
        //            TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

        //        };
        //        sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetMapGaziantepDbFromDb()
        //{
        //    var query = "SELECT *  FROM WI_WRH_OUT WHERE WarehouseName = 'Gaziantep ANA STOK'";
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows) { dataTable.Load(reader); }
        //            }
        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}

        //public List<WRH_OUT> MapSamsunDb()
        //{
        //    List<WRH_OUT> sales = new List<WRH_OUT>();
        //    var data = GetSamsunFromDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        WRH_OUT sale = new WRH_OUT
        //        {
        //            WarehouseName = Convert.ToString(row["WarehouseName"]),
        //            NetTotal = Convert.ToDecimal(row["NetTotal"]),
        //            TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

        //        };
        //        sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetSamsunFromDb()
        //{
        //    var query = "select* from WI_WRH_OUT where WarehouseName='SAMSUN ANA STOK'";
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows) { dataTable.Load(reader); }
        //            }
        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}


        //public List<WRH_OUT> GetIzmirFromDb()
        //{
        //    List<WRH_OUT> sales = new List<WRH_OUT>();
        //    var data = GetIzmirDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        WRH_OUT sale = new WRH_OUT
        //        {
        //            WarehouseName = Convert.ToString(row["WarehouseName"]),
        //            NetTotal = Convert.ToDecimal(row["NetTotal"]),
        //            TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

        //        };
        //        sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetIzmirDb()
        //{
        //    var query = "select* from WI_WRH_OUT where WarehouseName='İZMİR ANA STOK'";
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows) { dataTable.Load(reader); }
        //            }
        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}


        //public List<WRH_OUT> GetAnkaraDb()
        //{
        //    List<WRH_OUT> sales = new List<WRH_OUT>();
        //    var data = GetAnakaraDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        WRH_OUT sale = new WRH_OUT
        //        {
        //            WarehouseName = Convert.ToString(row["WarehouseName"]),
        //            NetTotal = Convert.ToDecimal(row["NetTotal"]),
        //            TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

        //        };
        //        sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetAnakaraDb()
        //{
        //    var query = "select* from WI_WRH_OUT where WarehouseName='ANKARA ANA STOK'";
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows) { dataTable.Load(reader); }
        //            }
        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}


        //public List<WRH_OUT> GetAtasehirDb()
        //{
        //    List<WRH_OUT> sales = new List<WRH_OUT>();
        //    var data = GetAtasehirFromDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        WRH_OUT sale = new WRH_OUT
        //        {
        //            WarehouseName = Convert.ToString(row["WarehouseName"]),
        //            NetTotal = Convert.ToDecimal(row["NetTotal"]),
        //            TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

        //        };
        //        sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetAtasehirFromDb()
        //{
        //    var query = "select* from WI_WRH_OUT where WarehouseName='ATAŞEHİR ANA STOK'";
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows) { dataTable.Load(reader); }
        //            }
        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}
        //public List<WRH_OUT> GetBasakSehirDb()
        //{
        //    List<WRH_OUT> sales = new List<WRH_OUT>();
        //    var data = GetBasakSehirFromDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        WRH_OUT sale = new WRH_OUT
        //        {
        //            WarehouseName = Convert.ToString(row["WarehouseName"]),
        //            NetTotal = Convert.ToDecimal(row["NetTotal"]),
        //            TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

        //        };
        //        sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetBasakSehirFromDb()
        //{
        //    var query = "select* from WI_WRH_OUT where WarehouseName='ATAŞEHİR ANA STOK'";
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows) { dataTable.Load(reader); }
        //            }
        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}




    }
}
