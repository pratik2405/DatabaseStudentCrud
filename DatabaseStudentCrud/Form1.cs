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


namespace DatabaseStudentCrud
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            conn=new SqlConnection(ConfigurationManager.ConnectionStrings["defaultCon"].ConnectionString);
        }

        public void ClearStudentForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtPercentage.Clear();
            //comboBoxDepartment.Refresh();
            comboBoxDepartment.ResetText();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "insert into studentinfo values(@name,@department,@percentage)";
                cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@department", comboBoxDepartment.SelectedItem);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToDouble(txtPercentage.Text));

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student Added Successfully");
                    ClearStudentForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           ClearStudentForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select name,department,percentage from studentinfo where id=@sid";
                cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(txtId.Text));

                conn.Open();

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        comboBoxDepartment.SelectedItem = dr["department"].ToString();
                        txtPercentage.Text = dr["percentage"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Student not found !");
                    ClearStudentForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "delete from studentinfo where id=@sid";
                cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(txtId.Text));

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student Deleted Successfully");
                    ClearStudentForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "update studentinfo set name=@name,department=@department,percentage=@percentage where id=@sid";
                cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@department", comboBoxDepartment.SelectedItem);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToDouble(txtPercentage.Text));

                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student Update Successfully");
                    ClearStudentForm();
                }
                else
                {
                    MessageBox.Show("Student not found !");
                    ClearStudentForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from studentinfo";
                cmd = new SqlCommand(str, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }
    }
}
