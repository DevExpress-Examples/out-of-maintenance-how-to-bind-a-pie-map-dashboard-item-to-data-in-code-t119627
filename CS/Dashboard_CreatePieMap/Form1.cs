using System;
using DevExpress.DashboardCommon;
using DevExpress.XtraEditors;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;

namespace Dashboard_CreatePieMap {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Creates a new dashboard and data source for this dashboard.
            Dashboard dashboard = new Dashboard();

            DashboardSqlDataSource dataSource = new DashboardSqlDataSource();
            dataSource.ConnectionParameters =
                new XmlFileConnectionParameters(@"..\..\Data\sochi2014.xml");
            TableQuery sqlQuery = new TableQuery("Medals");
            sqlQuery.AddTable("Table1").
                SelectColumns("CapitalLat", "CapitalLon", "Quantity", "MedalClass", "Name");
            dataSource.Queries.Add(sqlQuery);
            dashboard.DataSources.Add(dataSource);

            // Creates a Pie Map dashboard item and specifies its data source.
            PieMapDashboardItem pieMap = new PieMapDashboardItem();
            pieMap.DataSource = dataSource;
            pieMap.DataMember = "Medals";

            // Loads the map of the world.
            pieMap.Area = ShapefileArea.WorldCountries;

            // Provides cities' coordinates.
            pieMap.Latitude = new Dimension("CapitalLat");
            pieMap.Longitude = new Dimension("CapitalLon");

            // Specifies pie values and argument.
            pieMap.Values.Add(new Measure("Quantity"));
            pieMap.Argument = new Dimension("MedalClass");

            // Specifies values displayed within pie tooltips.
            pieMap.TooltipDimensions.Add(new Dimension("Name"));

            // Adds the Pie Map dashboard item to the dashboard and opens this
            // dashboard in the Dashboard Viewer.
            dashboard.Items.Add(pieMap);
            dashboardViewer1.Dashboard = dashboard;
        }
    }
}
