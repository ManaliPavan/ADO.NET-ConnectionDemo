using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ADO.NET_ConnectionDemo
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strConnection);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "insert into Course values (@name,@fees)";
                cmd = new SqlCommand(str, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@fees", txtFees.Text);
                //open DB Connection
                con.Open();
                //fire query insert, update, delete
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record Inserted...", "Message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "update Course set Cname=@name,Fees=@fees where Cid=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@fees", txtFees.Text);
                //open DB Connection
                con.Open();
                //fire query insert, update, delete
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record Updated...", "Message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Course where Cid=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));

                //open DB Connection
                con.Open();
                //fire query insert, update, delete
                dr = cmd.ExecuteReader();
                if (dr.HasRows)//check that record is present in dr object or not
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["Cname"].ToString();
                        txtFees.Text = dr["Fees"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not Found..", "Message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtName.Clear();
            txtFees.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "delete from Course where Cid=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));

                //open DB Connection
                con.Open();
                //fire query insert, update, delete
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record Deleted...", "Message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Course";
                cmd = new SqlCommand(str, con);
                //open DB Connection
                con.Open();
                //fire query insert, update, delete
                dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
