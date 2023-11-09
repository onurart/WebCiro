using Microsoft.AspNetCore.SignalR;
using WebCiro.Repositories;

namespace WebCiro.Hubs
{
    public class DashboardHub : Hub
    {
        SaleRepository saleRepository;

        public DashboardHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            saleRepository = new SaleRepository(connectionString);
        }
        public async Task SendSales()
        {
            var sales = saleRepository.GetSales();
            await Clients.All.SendAsync("ReceivedSales", sales);

        }
        
        //public async Task MapCustomer()
        //{
        //    var sales = saleRepository.MapCustomertDb();
        //    await Clients.All.SendAsync("ReceivedMapCusmert", sales);

        //}
        
        //public async Task MapAntep()
        //{
        //    var sales = saleRepository.MapGazianteptDb();
        //    await Clients.All.SendAsync("ReceivedMapAntep", sales);

        //}
        
        //public async Task MapSamsun()
        //{
        //    var sales = saleRepository.MapSamsunDb();
        //    await Clients.All.SendAsync("ReceivedMapSamsun", sales);

        //}
    }
}
