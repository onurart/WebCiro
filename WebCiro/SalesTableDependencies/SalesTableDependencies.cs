using TableDependency.SqlClient;
using WebCiro.Hubs;
using WebCiro.Models;

namespace WebCiro.SalesTableDependencies
{
    public class SalesTableDependencies : ISubscribeTableDependency
    {
        SqlTableDependency<WI_DashboardTbl> tableDependency;
        DashboardHub dashboardHub;
        public SalesTableDependencies(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }
        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<WI_DashboardTbl>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<WI_DashboardTbl> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendSales();
                //dashboardHub.MapGaziantep();
                //dashboardHub.MapSamsun();
                //dashboardHub.MapIzmir();
                //dashboardHub.MapAnkara();
                //dashboardHub.MapAtasehir();
                //dashboardHub.MapBasakSehir();
            }
        }
    }
}
