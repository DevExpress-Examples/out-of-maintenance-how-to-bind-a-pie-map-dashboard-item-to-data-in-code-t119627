using System;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;

namespace Dashboard_CreatePieMap {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Creates a new dashboard and data source for this dashboard.
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

            // Creates a Pie Map dashboard item and specifies its data source.
            PieMapDashboardItem pieMap = new PieMapDashboardItem();
            pieMap.DataSource = xmlDataSource;
            pieMap.DataMember = "Query 1";

            // Loads the map of the europe.
            pieMap.Area = ShapefileArea.Europe;

            // Provides countries coordinates.
            pieMap.Latitude = new Dimension("Latitude");
            pieMap.Longitude = new Dimension("Longitude");

            // Specifies pie values and argument.
            pieMap.Values.Add(new Measure("Production"));
            pieMap.Argument = new Dimension("EnergyType");

            // Specifies values displayed within pie tooltips.
            pieMap.TooltipDimensions.Add(new Dimension("Country"));
            pieMap.Legend.Visible = true;

            // Adds the Pie Map dashboard item to the dashboard and opens this
            // dashboard in the Dashboard Viewer.
            dashboard.Items.Add(pieMap);
            dashboardViewer1.Dashboard = dashboard;
        }
    }
}
