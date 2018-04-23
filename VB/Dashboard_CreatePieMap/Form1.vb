Imports System
Imports DevExpress.DashboardCommon
Imports DevExpress.XtraEditors
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql

Namespace Dashboard_CreatePieMap
    Partial Public Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' Creates a new dashboard and data source for this dashboard.
            Dim dashboard As New Dashboard()

            Dim dataSource As New DashboardSqlDataSource()
            dataSource.ConnectionParameters = New XmlFileConnectionParameters("..\..\Data\sochi2014.xml")
            Dim sqlQuery As New TableQuery("Medals")
            sqlQuery.AddTable("Table1").SelectColumns("CapitalLat", "CapitalLon", _
                                                      "Quantity", "MedalClass", "Name")
            dataSource.Queries.Add(sqlQuery)
            dashboard.DataSources.Add(dataSource)

            ' Creates a Pie Map dashboard item and specifies its data source.
            Dim pieMap As New PieMapDashboardItem()
            pieMap.DataSource = dataSource
            pieMap.DataMember = "Medals"

            ' Loads the map of the world.
            pieMap.Area = ShapefileArea.WorldCountries

            ' Provides cities' coordinates.
            pieMap.Latitude = New Dimension("CapitalLat")
            pieMap.Longitude = New Dimension("CapitalLon")

            ' Specifies pie values and argument.
            pieMap.Values.Add(New Measure("Quantity"))
            pieMap.Argument = New Dimension("MedalClass")

            ' Specifies values displayed within pie tooltips.
            pieMap.TooltipDimensions.Add(New Dimension("Name"))

            ' Adds the Pie Map dashboard item to the dashboard and opens this
            ' dashboard in the Dashboard Viewer.
            dashboard.Items.Add(pieMap)
            dashboardViewer1.Dashboard = dashboard
        End Sub
    End Class
End Namespace
