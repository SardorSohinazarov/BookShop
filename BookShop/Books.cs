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

namespace BookShop
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void populate()
        {
            conn.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query,conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void filter()
        {
            conn.Open();
            string query = "select * from BookTbl where BCat ='"+CatCbSearchCb.SelectedItem.ToString()+"'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            user.Show();
            this.Hide();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(BTitleTb.Text == "" || BPriceTb.Text == "" || BAuther.Text == "" || QtyTb.Text == "" || BCbTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = @"insert into BookTbl values('"+BTitleTb.Text+"','"+BAuther.Text+"','"+BCbTb.SelectedItem.ToString()+"','"+QtyTb.Text+"','"+BPriceTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Saved Successfully");
                    conn.Close();
                    populate();
                    Reset();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void CatCbSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filter();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
            CatCbSearchCb.SelectedIndex = -1;
        }
        private void Reset()
        {
            BTitleTb.Text = "";
            BAuther.Text = "";
            BCbTb.SelectedIndex = -1;
            BPriceTb.Text = "";
            QtyTb.Text = "";
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitleTb.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            BAuther.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            BCbTb.SelectedItem = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            QtyTb.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            BPriceTb.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            if (BTitleTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (key == 0 )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = @"delete from  BookTbl where BId ="+key+";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted Successfully");
                    conn.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BPriceTb.Text == "" || BAuther.Text == "" || QtyTb.Text == "" || BCbTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = @"update BookTbl set BTitle = '"+BTitleTb.Text+"',BAuther= '"+BAuther.Text+"',BCat='"+BCbTb.SelectedItem.ToString()+"',BQty="+QtyTb.Text+",BPince= "+BPriceTb.Text+" where BId = "+key+";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted Successfully");
                    conn.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //DashBoard dashBoard = new DashBoard();
            //dashBoard.Show();
            //this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
