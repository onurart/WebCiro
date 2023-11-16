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
            var query = $" SELECT * FROM WI_DashboardTbl where OrderDate >  CONVERT(datetime, @ReferenceDate,104) order by rn ";
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


                    // Set the parameter for the reference date

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., logging or informing the user)
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }

































        //private DataTable GetSaleDetailsFromDb()
        //{
        //    ///var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS Id, C.Name AS [CustomerName], C.Code AS [CustomerCode], O.CreateDate AS [OrderDate], O.Number[OrderNumber], C.City AS [City], P.Name AS [ProductName], ROUND(D .TotalPrice, 2) AS [TotalTl], IT.AUXCODE AS [VehicleType], IT.AUXCODE2 AS [VehicleBrand], B.Name AS [PartBrand], CT.Name AS [Category], OS.Name AS [Status] FROM            [192.168.180.103].[B2B].[dbo].OrderDetail AS D WITH (NOLOCK) INNER JOIN [192.168.180.103].[B2B].[dbo].OrderHeader AS O WITH (NOLOCK) ON D .OrderHeaderId = O.OrderHeaderId INNER JOIN [192.168.180.103].[B2B].[dbo].Customer AS C WITH (NOLOCK) ON C.CustomerId = O.CustomerId INNER JOIN [192.168.180.103].[B2B].[dbo]. Product AS P WITH (NOLOCK) ON P.ProductId = D .ProductId LEFT JOIN [192.168.180.103].[B2B].[dbo].Currency AS CR WITH (NOLOCK) ON CR.CurrencyId = D .CurrencyId LEFT JOIN [192.168.180.103].[B2B].[dbo].Warehouse AS W WITH (NOLOCK) ON W.WarehouseId = D .WarehouseId LEFT JOIN [192.168.180.103].[B2B].[dbo].CustomerAddress AS CA WITH (NOLOCK) ON CA.CustomerAddressId = O.DeliveryCustomerAddressId LEFT JOIN [192.168.180.103].[B2B].[dbo].OrderHeaderStatus AS OS WITH (NOLOCK) ON OS.OrderHeaderStatusId = O.OrderHeaderStatusId LEFT JOIN [192.168.180.103].[B2B].[dbo].Brand AS B WITH (NOLOCK) ON P.BrandId = B.BrandId LEFT JOIN [192.168.180.103].[B2B].[dbo].ProductCategory AS PC WITH (NOLOCK) ON P.ProductId = PC.ProductId LEFT JOIN [192.168.180.103].[B2B].[dbo].Category AS CT WITH (NOLOCK) ON PC.CategoryId = CT.CategoryId LEFT JOIN [192.168.180.101].[JPLATFORM].[dbo].[U_001_ITEMS] AS IT WITH (NOLOCK) ON P.ProductRef = IT.LOGICALREF WHERE O.IsDeleted = 0 AND D .IsDeleted = 0 AND CAST(O.CreateDate AS Date) = CAST(GETDATE() AS Date)";
        //    var query = "SELECT [Id], [CustomerName], [CustomerCode], [OrderDate], [OrderNumber], [City], [ProductName], [TotalTl], [VehicleType], [VehicleBrand], [PartBrand], [Category], [Status], DATEPART(HOUR, [OrderDate]) AS OrderHour, DATEPART(MINUTE, [OrderDate]) AS OrderMinute, DATEPART(SECOND, [OrderDate]) AS OrderSecond FROM [WI_DashboardTbl] ORDER BY [OrderDate] DESC;";
        //    //var query = "select * from WI_DashboardTbl order by OrderDate desc";
        //    DataTable dataTable = new DataTable();
        //    string sqlconStr = "Data Source=192.168.181.150,1433;Initial Catalog=JPLATFORM;User Id=onursa;Password=4473634Onur?;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //    using (SqlConnection connection = new SqlConnection(sqlconStr))
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


        public List<WI_WRH_OUT> MapGazianteptDb()
        {
            List<WI_WRH_OUT> sales = new List<WI_WRH_OUT>();
            var data = GetMapGaziantepDbFromDb();
            foreach (DataRow row in data.Rows)
            {
                WI_WRH_OUT sale = new WI_WRH_OUT
                {
                    WarehouseName = Convert.ToString(row["WarehouseName"]),
                    NetTotal = Convert.ToDecimal(row["NetTotal"]),
                    TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

                };
                sales.Add(sale);
            }
            return sales;
        }
        private DataTable GetMapGaziantepDbFromDb()
        {
            var query = "select* from WI_WRH_OUT where WarehouseName='GAZİANTEP ANA STOK'";
            DataTable dataTable = new DataTable();
            string sqlconStr = "Data Source=192.168.181.150,1433;Initial Catalog=JPLATFORM;User Id=onursa;Password=4473634Onur?;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(sqlconStr))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Hata işleme eklenebilir (loglama veya kullanıcıya bilgilendirme)
                    throw new Exception("Veri çekme hatası", ex);
                }
            }            
        }
        
        
        
        
        
        public List<WI_WRH_OUT> MapSamsunDb()
        {
            List<WI_WRH_OUT> sales = new List<WI_WRH_OUT>();
            var data = GetSamsunFromDb();
            foreach (DataRow row in data.Rows)
            {
                WI_WRH_OUT sale = new WI_WRH_OUT
                {
                    WarehouseName = Convert.ToString(row["WarehouseName"]),
                    NetTotal = Convert.ToDecimal(row["NetTotal"]),
                    TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

                };
                sales.Add(sale);
            }
            return sales;
                }
        private DataTable GetSamsunFromDb()
        {
            var query = "select* from WI_WRH_OUT where WarehouseName='SAMSUN ANA STOK'";
            DataTable dataTable = new DataTable();
            string sqlconStr = "Data Source=192.168.181.150,1433;Initial Catalog=JPLATFORM;User Id=onursa;Password=4473634Onur?;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(sqlconStr))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Hata işleme eklenebilir (loglama veya kullanıcıya bilgilendirme)
                    throw new Exception("Veri çekme hatası", ex);
                }
            }            
        }


        public List<WI_WRH_OUT> GetIzmirFromDb()
        {
            List<WI_WRH_OUT> sales = new List<WI_WRH_OUT>();
            var data = GetIzmirDb();
            foreach (DataRow row in data.Rows)
            {
                WI_WRH_OUT sale = new WI_WRH_OUT
                {
                    WarehouseName = Convert.ToString(row["WarehouseName"]),
                    NetTotal = Convert.ToDecimal(row["NetTotal"]),
                    TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

                };
                sales.Add(sale);
            }
            return sales;
        }
        private DataTable GetIzmirDb()
        {
            var query = "select* from WI_WRH_OUT where WarehouseName='İZMİR ANA STOK'";
            DataTable dataTable = new DataTable();
            string sqlconStr = "Data Source=192.168.181.150,1433;Initial Catalog=JPLATFORM;User Id=onursa;Password=4473634Onur?;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(sqlconStr))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Hata işleme eklenebilir (loglama veya kullanıcıya bilgilendirme)
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }
        
        
        
        public List<WI_WRH_OUT> GetAnkaraDb()
        {
            List<WI_WRH_OUT> sales = new List<WI_WRH_OUT>();
            var data = GetAnakaraDb();
            foreach (DataRow row in data.Rows)
            {
                WI_WRH_OUT sale = new WI_WRH_OUT
                {
                    WarehouseName = Convert.ToString(row["WarehouseName"]),
                    NetTotal = Convert.ToDecimal(row["NetTotal"]),
                    TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

                };
                sales.Add(sale);
            }
            return sales;
        }
        private DataTable GetAnakaraDb()
        {
            var query = "select* from WI_WRH_OUT where WarehouseName='ANKARA ANA STOK'";
            DataTable dataTable = new DataTable();
            string sqlconStr = "Data Source=192.168.181.150,1433;Initial Catalog=JPLATFORM;User Id=onursa;Password=4473634Onur?;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(sqlconStr))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Hata işleme eklenebilir (loglama veya kullanıcıya bilgilendirme)
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }
        
        
        public List<WI_WRH_OUT> GetAtasehirDb()
        {
            List<WI_WRH_OUT> sales = new List<WI_WRH_OUT>();
            var data = GetAtasehirFromDb();
            foreach (DataRow row in data.Rows)
            {
                WI_WRH_OUT sale = new WI_WRH_OUT
                {
                    WarehouseName = Convert.ToString(row["WarehouseName"]),
                    NetTotal = Convert.ToDecimal(row["NetTotal"]),
                    TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

                };
                sales.Add(sale);
            }
            return sales;
        }
        private DataTable GetAtasehirFromDb()
        {
            var query = "select* from WI_WRH_OUT where WarehouseName='ATAŞEHİR ANA STOK'";
            DataTable dataTable = new DataTable();
            string sqlconStr = "Data Source=192.168.181.150,1433;Initial Catalog=JPLATFORM;User Id=onursa;Password=4473634Onur?;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(sqlconStr))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Hata işleme eklenebilir (loglama veya kullanıcıya bilgilendirme)
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }
        
        
        
        public List<WI_WRH_OUT> GetBasakSehirDb()
        {
            List<WI_WRH_OUT> sales = new List<WI_WRH_OUT>();
            var data = GetBasakSehirFromDb();
            foreach (DataRow row in data.Rows)
            {
                WI_WRH_OUT sale = new WI_WRH_OUT
                {
                    WarehouseName = Convert.ToString(row["WarehouseName"]),
                    NetTotal = Convert.ToDecimal(row["NetTotal"]),
                    TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),

                };
                sales.Add(sale);
            }
            return sales;
        }
        private DataTable GetBasakSehirFromDb()
        {
            var query = "select* from WI_WRH_OUT where WarehouseName='BAŞAKŞEHİR ANA STOK'";
            DataTable dataTable = new DataTable();
            string sqlconStr = "Data Source=192.168.181.150,1433;Initial Catalog=JPLATFORM;User Id=onursa;Password=4473634Onur?;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(sqlconStr))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Hata işleme eklenebilir (loglama veya kullanıcıya bilgilendirme)
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }

        //public List<SaleCount> MapCustomertDb()
        //{
        //    List<SaleCount> sales = new List<SaleCount>();

        //    var data = GetMapCustomertDbFromDb();

        //    foreach (DataRow row in data.Rows)
        //    {
        //        SaleCount sale = new SaleCount
        //        {
        //            Amount = Convert.ToDecimal(row["TotalAmount"]),
        //        };

        //        sales.Add(sale);
        //    }

        //    return sales;
        //}

        //private DataTable GetMapCustomertDbFromDb()
        //{
        //    var query = "select SUM(Amount) as TotalAmount from Sale";

        //    DataTable dataTable = new DataTable();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    dataTable.Load(reader);
        //                }
        //            }

        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Veri çekme hatası", ex);
        //        }
        //    }
        //}













        //public List<BanchSale> MapSamsunDb()
        //{
        //    List<BanchSale> sales = new List<BanchSale>();
        //    var data = GetSamsunDbFromDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        BanchSale sale = new BanchSale { Branch = Convert.ToString(row["branchSamsun"]), }; sales.Add(sale);
        //    }
        //    return sales;
        //}
        //private DataTable GetSamsunDbFromDb()
        //{
        //    var query = "SELECT SUM(Amount) AS branchSamsun FROM Sale WHERE Branch = 'Samsun';";
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
