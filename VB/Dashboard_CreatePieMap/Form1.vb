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

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			Dim dashboard As New Dashboard()

			Dim xmlDataSource As New DashboardSqlDataSource()
			xmlDataSource.ConnectionParameters = New XmlFileConnectionParameters("..\..\Data\DashboardEnergyStatictics.xml")
			Dim sqlQuery As SelectQuery = SelectQueryFluentBuilder.AddTable("Countries").SelectColumns("Latitude", "Longitude", "Production", "EnergyType", "Country").Build("Query 1")
			xmlDataSource.Queries.Add(sqlQuery)
			dashboard.DataSources.Add(xmlDataSource)

			Dim pieMap As PieMapDashboardItem = CreatePieMap(xmlDataSource)

			dashboard.Items.Add(pieMap)
			dashboardViewer1.Dashboard = dashboard
		End Sub

		Private Shared Function CreatePieMap(ByVal xmlDataSource As DashboardSqlDataSource) As PieMapDashboardItem
			Dim pieMap As New PieMapDashboardItem()
			pieMap.DataSource = xmlDataSource
			pieMap.DataMember = "Query 1"

			pieMap.Area = ShapefileArea.Europe

			pieMap.Latitude = New Dimension("Latitude")
			pieMap.Longitude = New Dimension("Longitude")

			pieMap.Values.Add(New Measure("Production"))
			pieMap.Argument = New Dimension("EnergyType")

			pieMap.TooltipDimensions.Add(New Dimension("Country"))
			pieMap.Legend.Visible = True
			Return pieMap
		End Function
	End Class
End Namespace
