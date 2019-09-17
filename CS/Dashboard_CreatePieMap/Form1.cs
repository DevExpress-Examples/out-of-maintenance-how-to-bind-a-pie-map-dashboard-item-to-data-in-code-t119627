using System;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;

namespace Dashboard_CreatePieMap {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
            dashboardViewer1.AsyncMode = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();

            DashboardSqlDataSource xmlDataSource = new DashboardSqlDataSource();
            xmlDataSource.ConnectionParameters =
                new XmlFileConnectionParameters(@"..\..\Data\DashboardEnergyStatictics.xml");
            SelectQuery sqlQuery = SelectQueryFluentBuilder
                .AddTable("Countries")
                .SelectColumns("Latitude", "Longitude", "Production", "EnergyType", "Country")
                .Build("Query 1");
            xmlDataSource.Queries.Add(sqlQuery);
            dashboard.DataSources.Add(xmlDataSource);

            PieMapDashboardItem pieMap = CreatePieMap(xmlDataSource);

            dashboard.Items.Add(pieMap);
            dashboardViewer1.Dashboard = dashboard;
        }

        private static PieMapDashboardItem CreatePieMap(DashboardSqlDataSource xmlDataSource)
        {
            PieMapDashboardItem pieMap = new PieMapDashboardItem();
            pieMap.DataSource = xmlDataSource;
            pieMap.DataMember = "Query 1";

            pieMap.Area = ShapefileArea.Europe;

            pieMap.Latitude = new Dimension("Latitude");
            pieMap.Longitude = new Dimension("Longitude");

            pieMap.Values.Add(new Measure("Production"));
            pieMap.Argument = new Dimension("EnergyType");

            pieMap.TooltipDimensions.Add(new Dimension("Country"));
            pieMap.Legend.Visible = true;
            return pieMap;
        }
    }
}
