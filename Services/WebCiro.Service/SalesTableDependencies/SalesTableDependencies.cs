﻿using TableDependency.SqlClient;
using WebCiro.Core.Entities;
using WebCiro.Core.Service;
using WebCiro.Service.Hubs;

namespace WebCiro.Service.SalesTableDependencies
{
    public class SalesTableDependencies : ISubscribeTableDependency
    {
        SqlTableDependency<Sale> tableDependency;
        DashboardHub dashboardHub;
        public SalesTableDependencies(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
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
                dashboardHub.SendSales();
                dashboardHub.MapCustomer();
                dashboardHub.MapAntep();
                dashboardHub.MapSamsun();
            }
        }
    }
}
