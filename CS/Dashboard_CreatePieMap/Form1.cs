using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.XtraEditors;

namespace Dashboard_CreatePieMap {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Creates a new dashboard and data source for this dashboard.
            Dashboard dashboard = new Dashboard();
            dashboard.AddDataSource("Data Source 1", GetData());

            // Creates a Pie Map dashboard item and specifies its data source.
            PieMapDashboardItem pieMap = new PieMapDashboardItem();
            pieMap.DataSource = dashboard.DataSources[0];

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

        public DataTable GetData() {
            DataSet xmlDataSet = new DataSet();
            xmlDataSet.ReadXml(@"..\..\Data\Sochi2014.xml");
            return xmlDataSet.Tables[0];
        }
    }
}
