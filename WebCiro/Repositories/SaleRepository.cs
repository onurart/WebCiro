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

        public List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();

            var data = GetSaleDetailsFromDb();

            foreach (DataRow row in data.Rows)
            {
                Sale sale = new Sale
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Branch = row["Branch"].ToString(),
                    Customer = row["Customer"].ToString(),
                    ProductName = row["ProductName"].ToString().Substring(0, Math.Min(25, row["ProductName"].ToString().Length)),
                    ProdutGruop = row["ProdutGruop"].ToString(),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    PurchasedOn = Convert.ToDateTime(row["PurchasedOn"]).ToString("dd-MM-yyyy")
                };

                sales.Add(sale);
            }
            return sales;
        }

        private DataTable GetSaleDetailsFromDb()
        {
            var query = "SELECT Id, Branch, Customer, ProductName, ProdutGruop, Amount, PurchasedOn FROM Sale";
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



        public List<SaleCount> MapCustomertDb()
        {
            List<SaleCount> sales = new List<SaleCount>();

            var data = GetMapCustomertDbFromDb();

            foreach (DataRow row in data.Rows)
            {
                SaleCount sale = new SaleCount
                {
                    Amount = Convert.ToDecimal(row["TotalAmount"]),
                };

                sales.Add(sale);
            }

            return sales;
        }

        private DataTable GetMapCustomertDbFromDb()
        {
            var query = "select SUM(Amount) as TotalAmount from Sale";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }





        public List<BanchSale> MapGazianteptDb()
        {
            List<BanchSale> sales = new List<BanchSale>();
            var data = GetMapGaziantepDbFromDb();
            foreach (DataRow row in data.Rows)
            {
                BanchSale sale = new BanchSale{Branch = Convert.ToString(row["branchAntep"]),};sales.Add(sale);}
            return sales;
        }
        private DataTable GetMapGaziantepDbFromDb()
        {
            var query = "SELECT SUM(Amount) AS branchAntep FROM Sale WHERE Branch = 'Gaziantep';";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows){dataTable.Load(reader);}
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }







        public List<BanchSale> MapSamsunDb()
        {
            List<BanchSale> sales = new List<BanchSale>();
            var data = GetSamsunDbFromDb();
            foreach (DataRow row in data.Rows)
            {
                BanchSale sale = new BanchSale { Branch = Convert.ToString(row["branchSamsun"]), }; sales.Add(sale);
            }
            return sales;
        }
        private DataTable GetSamsunDbFromDb()
        {
            var query = "SELECT SUM(Amount) AS branchSamsun FROM Sale WHERE Branch = 'Samsun';";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) { dataTable.Load(reader); }
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Veri çekme hatası", ex);
                }
            }
        }

    }

}
