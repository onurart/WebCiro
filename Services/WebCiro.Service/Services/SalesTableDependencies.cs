using Microsoft.AspNetCore.SignalR;
using TableDependency.SqlClient;
using WebCiro.Core.Entities;
using WebCiro.Core.Service;

namespace WebCiro.Service.Services
{
    public class SalesTableDependencies : ISubscribeTableDependency
    {
        SqlTableDependency<Sale> tableDependency;
        private readonly IHubContext<DashboardHub> _hubContext;
    
        DashboardHub dashboardHub;
        public SalesTableDependencies(DashboardHub dashboardHub, IHubContext<DashboardHub> hubContext)
        {
            this.dashboardHub = dashboardHub;
            _hubContext = hubContext;
        }
        
        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Sale>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Sale> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                _hubContext.Clients.All.SendAsync("ReceivedSales");
            }
        }
    }
}
