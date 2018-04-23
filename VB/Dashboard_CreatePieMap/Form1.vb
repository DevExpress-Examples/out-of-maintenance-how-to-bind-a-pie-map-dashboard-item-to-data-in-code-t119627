Imports System
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors

Namespace Dashboard_CreatePieMap
    Partial Public Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' Creates a new dashboard and data source for this dashboard.
            Dim dashboard As New Dashboard()

            Dim xmlDataSource As New DashboardSqlDataSource()
            xmlDataSource.ConnectionParameters =
                New XmlFileConnectionParameters("..\..\Data\DashboardEnergyStatictics.xml")
            Dim sqlQuery As SelectQuery = SelectQueryFluentBuilder.AddTable("Countries"). _
                SelectColumns("Latitude", "Longitude", "Production", "EnergyType", "Country").Build("Query 1")
            xmlDataSource.Queries.Add(sqlQuery)
            dashboard.DataSources.Add(xmlDataSource)

            ' Creates a Pie Map dashboard item and specifies its data source.
            Dim pieMap As New PieMapDashboardItem()
            pieMap.DataSource = xmlDataSource
            pieMap.DataMember = "Query 1"

            ' Loads the map of the europe.
            pieMap.Area = ShapefileArea.Europe

            ' Provides countries coordinates.
            pieMap.Latitude = New Dimension("Latitude")
            pieMap.Longitude = New Dimension("Longitude")

            ' Specifies pie values and argument.
            pieMap.Values.Add(New Measure("Production"))
            pieMap.Argument = New Dimension("EnergyType")

            ' Specifies values displayed within pie tooltips.
            pieMap.TooltipDimensions.Add(New Dimension("Country"))
            pieMap.Legend.Visible = True

            ' Adds the Pie Map dashboard item to the dashboard and opens this
            ' dashboard in the Dashboard Viewer.
            dashboard.Items.Add(pieMap)
            dashboardViewer1.Dashboard = dashboard
        End Sub
    End Class
End Namespace
