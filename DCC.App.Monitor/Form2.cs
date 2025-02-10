using DevExpress.DataAccess.ConnectionParameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.XtraCharts;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.DashboardCommon.DataProcessing;
using DevExpress.XtraGrid.Views.Base;
using System.Configuration;
//using System.Data.Linq.SqlClient;
//using DCC.App.Monitor.SqlHelper;


namespace DCC.App.Monitor
{
    public partial class Form2 : Form
    {
        public int i = 1;
        string sChangedRows = string.Empty;
        public Form2()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void dashboardViewer1_Load(object sender, EventArgs e)
        //{



        //    // dashboardViewer1.ConfigureDataConnection
        //     dashboardViewer1.LoadDashboard(@"E:\Vaibhav\DCC.IntegrationServiceMonitor\DCC.App.Monitor\Integratation.xml");
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {
            if (e.DataSourceName == "LAPTOP-KM8CH2O9")
            {
                // ExtractDataSourceConnectionParameters parameters = e.ConnectionParameters as ExtractDataSourceConnectionParameters;

                Access97ConnectionParameters parameters =
                   (Access97ConnectionParameters)e.ConnectionParameters;
                //  e.ConnectionParameters{ < Parameter Name = "userid" Value = "sa" />
                //  < Parameter Name = "password" Value = "GLF@2018" />}
                parameters.UserName = "sa";
                parameters.Password = "1234";



            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        public void GetData()
        {
            SqlHelper oSql = new SqlHelper();
            var table = new DataTable();
            table = oSql.ExecuteDataQuery("Select * from int_details", System.Data.CommandType.Text).Tables[0];
            //table = oSql.hanaConnection("CALL \"" + ConfigurationManager.AppSettings["SAPDB"].ToString() + "\".\"DCC_App_GetIntegrationSeriveData\" ", System.Data.CommandType.Text).Tables[0];


            DataTable oDatable = table;
            gridControl1.DataSource = oDatable;

            //gridView2.Columns.

            //ColumnView Column = gridView2 as GridView;

            //gridView2.Columns["SourceTypeNew"].Width = 150;
            //gridView2.Columns["ErrorMessage"].Width = 400;
            ////  g
            //  gridColumn1.GetBestWidth()
            // gridView2.BestFitColumns();
            // gridView2.BestFitColumns();
            gridView2.ExpandAllGroups();
            //  gridView2.Wid
            // Column oColun = 

            int visibleEventsCount = gridView2.DataRowCount;
            textBox1.Text = visibleEventsCount.ToString();




        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            GridView View = sender as GridView;

            GetData();
            
          
        }

        private DataTable CreateChartData(int rowCount)
        {
            SqlHelper oSql = new SqlHelper();
            var table1 = new DataTable();
            //table1 = oSql.ExecuteDataQuery("EXEC  IntegrationData", System.Data.CommandType.Text).Tables[0];

           // table1 = oSql.hanaConnection("CALL \"" + ConfigurationManager.AppSettings["SAPDB"].ToString() + "\".\"IntegrationData\" ", System.Data.CommandType.Text).Tables[0];


            DataTable table = table1;
            // Add two columns to the table.
            table.Columns.Add("Argument", typeof(Int32));
            table.Columns.Add("Value", typeof(Int32));
            Random rnd = new Random();
            DataRow row = null;
            for (int i = 0; i < rowCount; i++)
            {
                row = table.NewRow();
                row["Argument"] = i;
                row["Value"] = rnd.Next(100);
                table.Rows.Add(row);
            }


            return table;
        }

        public void GetSourceType()
        {
            SqlHelper oSql = new SqlHelper();
            DataTable table = new DataTable();
            table = oSql.ExecuteDataQuery("select * from [DCC_IntF].[dbo].[int_details]", System.Data.CommandType.Text).Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (i == 1)
            {
                this.WindowState = FormWindowState.Maximized;
                i = 2;
            }
            else if (i == 2)
            {
                this.WindowState = FormWindowState.Normal;
                i = 1;
            }
        }

        // public void 

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void Expand_Click(object sender, EventArgs e)
        {
            gridView2.ExpandAllGroups();
        }

        private void Collapse_Click(object sender, EventArgs e)
        {
            gridView2.CollapseAllGroups();
        }

        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {

        }

        

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView View = sender as GridView;
            if (View == null)
                return;

            string sValue = e.Value.ToString();
            if (e.Column.Caption == "Retry" && e.Value.ToString() == "True" )
            {
                if (sChangedRows.Length > 0)
                    sChangedRows = sChangedRows + "," + View.GetRowCellValue(e.RowHandle, View.Columns["IntegrationId"]).ToString();
                else
                    sChangedRows = View.GetRowCellValue(e.RowHandle, View.Columns["IntegrationId"]).ToString();

            }

            int visibleEventsCount = View.DataRowCount;
            textBox1.Text = visibleEventsCount.ToString();
        }

        

        private void Save_Click(object sender, EventArgs e)
        {
            if (sChangedRows.Length > 0)
            {
                SqlHelper oSql = new SqlHelper();
                //oSql.hanaConnection("UPDATE \"IntegrationAudit\" SET \"Status\" = 0,\"ErrorMessage\" = '' WHERE \"IntegrationId\" in (" + sChangedRows + ") ", CommandType.Text);
                //_sqlHelper.hanaConnection("UPDATE " + sDatabaseName + ".\"IntegrationAudit\" SET \"Status\" = '" + iFlag + "', \"Status\" = '" + iStatus + "',\"ErrorMessage\" = '" + message + "' where \"IntegrationId\" in ( " + IntegrationId + ")", System.Data.CommandType.Text);


                sChangedRows = string.Empty;
            }
            GetData();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void gridView2_CustomRowFilter(object sender, RowFilterEventArgs e)
        {
            GridView View = sender as GridView;
            int m = 0;
            int visibleDataRowCount = 0;
            //for (int i = 0; i < gridView2.DataRowCount; i++)
            //{
            //    bool sValue = View.GetVisibleRowLevel

            //}



            textBox1.Text = View.ToString();
        }
    }
}



