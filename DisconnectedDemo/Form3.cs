using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ADO.NET_ConnectionDemo
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;

        public Form3()
        {
            InitializeComponent();
            string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strConnection);
        }
        private DataSet GetEmpData()
        {
            da = new SqlDataAdapter("select * from Employee", con);
            // add PK constraint to the col (id) which is in DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // build the command for DataAdpater 
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            // Fill() fire the select qry in DB & load data in DataSet
            da.Fill(ds, "employee"); // emp is the name given to the table which get loaded in the DataSet
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["employee"].NewRow();
                row["Name"] = txtName.Text;
                row["Salary"] = txtSalary.Text;
                ds.Tables["employee"].Rows.Add(row);
                // reflect the changes from DataSet to DB
                int result = da.Update(ds.Tables["employee"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

