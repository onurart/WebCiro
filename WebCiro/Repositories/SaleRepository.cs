using System.Data;
using System.Data.SqlClient;
using WebCiro.Models;

namespace WebCiro.Repositories
{
    public class SaleRepository
    {
        private string connectionString;

        public SaleRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<WI_DashboardTbl> GetSales()
        {
            List<WI_DashboardTbl> sales = new List<WI_DashboardTbl>();

            var data = GetSaleDetailsFromDb();

            foreach (DataRow row in data.Rows)
            {
                WI_DashboardTbl sale = new WI_DashboardTbl
                {
                    Id = Convert.ToInt32(row["Id"]),
                    CustomerName = row["CustomerName"].ToString(),
                    CustomerCode = row["CustomerCode"].ToString(),

                    OrderDate = row["OrderDate"] != DBNull.Value? Convert.ToDateTime(row["OrderDate"]): (DateTime?)null,

                    //OrderDate = Convert.ToDateTime(row["OrderDate"]).ToString("dd-MM-yyyy"),
                    OrderNumber = row["OrderNumber"].ToString(),
                    City = row["City"].ToString(),
                    //ProductName = row["ProductName"].ToString(),
                    ProductName = row["ProductName"].ToString().Substring(0, Math.Min(25, row["ProductName"].ToString().Length)),
                    TotalTl = Convert.ToDecimal(row["TotalTl"].ToString()),
                    VehicleType = row["VehicleType"].ToString(),
                    VehicleBrand = row["VehicleBrand"].ToString(),
                    PartBrand = row["PartBrand"].ToString(),
                    Category = row["Category"].ToString(),
                    Status = row["Status"].ToString(),

                    //ProdutGruop = row["ProdutGruop"].ToString(),
                    //Amount = Convert.ToDecimal(row["Amount"]),
                };

                sales.Add(sale);
            }
            return sales;
        }

        private DataTable GetSaleDetailsFromDb()
        {
            var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS Id, C.Name AS [CustomerName], C.Code AS [CustomerCode], O.CreateDate AS [OrderDate], O.Number[OrderNumber], C.City AS [City], P.Name AS [ProductName], ROUND(D .TotalPrice, 2) AS [TotalTl], IT.AUXCODE AS [VehicleType], IT.AUXCODE2 AS [VehicleBrand], B.Name AS [PartBrand], CT.Name AS [Category], OS.Name AS [Status] FROM            [192.168.180.103].[B2B].[dbo].OrderDetail AS D WITH (NOLOCK) INNER JOIN [192.168.180.103].[B2B].[dbo].OrderHeader AS O WITH (NOLOCK) ON D .OrderHeaderId = O.OrderHeaderId INNER JOIN [192.168.180.103].[B2B].[dbo].Customer AS C WITH (NOLOCK) ON C.CustomerId = O.CustomerId INNER JOIN [192.168.180.103].[B2B].[dbo]. Product AS P WITH (NOLOCK) ON P.ProductId = D .ProductId LEFT JOIN [192.168.180.103].[B2B].[dbo].Currency AS CR WITH (NOLOCK) ON CR.CurrencyId = D .CurrencyId LEFT JOIN [192.168.180.103].[B2B].[dbo].Warehouse AS W WITH (NOLOCK) ON W.WarehouseId = D .WarehouseId LEFT JOIN [192.168.180.103].[B2B].[dbo].CustomerAddress AS CA WITH (NOLOCK) ON CA.CustomerAddressId = O.DeliveryCustomerAddressId LEFT JOIN [192.168.180.103].[B2B].[dbo].OrderHeaderStatus AS OS WITH (NOLOCK) ON OS.OrderHeaderStatusId = O.OrderHeaderStatusId LEFT JOIN [192.168.180.103].[B2B].[dbo].Brand AS B WITH (NOLOCK) ON P.BrandId = B.BrandId LEFT JOIN [192.168.180.103].[B2B].[dbo].ProductCategory AS PC WITH (NOLOCK) ON P.ProductId = PC.ProductId LEFT JOIN [192.168.180.103].[B2B].[dbo].Category AS CT WITH (NOLOCK) ON PC.CategoryId = CT.CategoryId LEFT JOIN [192.168.180.101].[JPLATFORM].[dbo].[U_001_ITEMS] AS IT WITH (NOLOCK) ON P.ProductRef = IT.LOGICALREF WHERE O.IsDeleted = 0 AND D .IsDeleted = 0 AND CAST(O.CreateDate AS Date) = CAST(GETDATE() AS Date)";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
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





        //public List<BanchSale> MapGazianteptDb()
        //{
        //    List<BanchSale> sales = new List<BanchSale>();
        //    var data = GetMapGaziantepDbFromDb();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        BanchSale sale = new BanchSale{Branch = Convert.ToString(row["branchAntep"]),};sales.Add(sale);}
        //    return sales;
        //}
        //private DataTable GetMapGaziantepDbFromDb()
        //{
        //    var query = "SELECT SUM(Amount) AS branchAntep FROM Sale WHERE Branch = 'Gaziantep';";
        //    DataTable dataTable = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows){dataTable.Load(reader);}
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
