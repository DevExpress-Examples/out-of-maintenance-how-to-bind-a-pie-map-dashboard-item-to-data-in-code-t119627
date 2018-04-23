Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
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
			dashboard.AddDataSource("Data Source 1", GetData())

			' Creates a Pie Map dashboard item and specifies its data source.
			Dim pieMap As New PieMapDashboardItem()
			pieMap.DataSource = dashboard.DataSources(0)

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

		Public Function GetData() As DataTable
			Dim xmlDataSet As New DataSet()
			xmlDataSet.ReadXml("..\..\Data\Sochi2014.xml")
			Return xmlDataSet.Tables(0)
		End Function
	End Class
End Namespace
