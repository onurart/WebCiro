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
        public async Task MapGaziantep()
        {
            var sales = saleRepository.MapGazianteptDb();
            await Clients.All.SendAsync("ReceivedMapGaziantep", sales);

        }
        
        public async Task MapSamsun()
        {
            var sales = saleRepository.MapSamsunDb();
            await Clients.All.SendAsync("ReceivedMapSamsun", sales);

        }
        
        public async Task MapIzmir()
        {
            var sales = saleRepository.GetIzmirFromDb();
            await Clients.All.SendAsync("ReceivedMapIzmir", sales);

        }
        
        public async Task MapAnkara()
        {
            var sales = saleRepository.GetAnkaraDb();
            await Clients.All.SendAsync("ReceivedMapAnkara", sales);

        }
        
        public async Task MapAtasehir()
        {
            var sales = saleRepository.GetAtasehirDb();
            await Clients.All.SendAsync("ReceivedMapAtasehir", sales);

        }   public async Task MapBasakSehir()
        {
            var sales = saleRepository.GetBasakSehirDb();
            await Clients.All.SendAsync("ReceivedMapBasakSehir", sales);

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
