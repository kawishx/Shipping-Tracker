using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Shipping_Tracker_V1
{
    public partial class UserPages : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadusers();

                loadpages();

                loaddata();

                lblMessage.Text = "";
                lblMessage.Visible = false;
            }
        }

        public void checkconnection()
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        protected void loadusers()
        {
            checkconnection();

            SqlCommand command = new SqlCommand("SELECT [username] FROM [dbo].[Login]", sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DropDownList2.DataSource = ds;
            DropDownList2.DataTextField = "username";
            DropDownList2.DataValueField = "username";
            DropDownList2.DataBind();
        }

        protected void loadpages()
        {
            checkconnection();
            DataGrid2.DataSource = null;
            DataGrid2.DataBind();

            SqlCommand command = new SqlCommand("SELECT * FROM dbo.pages WHERE Proj='SHT'", sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataGrid2.DataSource = ds;
            DataGrid2.DataBind();
        }

        protected void loaddata()
        {
            checkconnection();

            SqlCommand command = new SqlCommand("SELECT parent FROM Pages WHERE Proj='SHT'", sqlConn);
            SqlDataReader Dr;
            sqlConn.Open();
            Dr = command.ExecuteReader();

            while (Dr.Read())
            {
                for (int i = 0; i < DataGrid2.Items.Count; i++)
                {
                    DataGridItem dg = DataGrid2.Items[i];
                    CheckBox cx = dg.FindControl("frontcheck") as CheckBox;
                    if (Dr[0].ToString() == dg.Cells[2].Text.ToString())
                    {
                        cx.Checked = true;
                    }
                }
            }
            sqlConn.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int chval;

            for (int j = 0; j < DataGrid2.Items.Count; j++)
            {
                DataGridItem dg = DataGrid2.Items[j];
                CheckBox cx = dg.FindControl("frontcheck") as CheckBox;
                chval = cx.Checked ? 1 : 0;

                SqlCommand command2 = new SqlCommand("set_page_access_sht", sqlConn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command2.Parameters.Clear();
                command2.Parameters.AddWithValue("@chvalue", chval);
                command2.Parameters.AddWithValue("@username", DropDownList2.Text);
                command2.Parameters.AddWithValue("@parent", dg.Cells[1].Text.ToString());

                checkconnection();
                sqlConn.Open();
                command2.ExecuteNonQuery();
                sqlConn.Close();
            }

            loadpages();
            loadusers();

            // Show success message
            lblMessage.Text = "User Pages allocated successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Visible = true;
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadpages();
            loaddata();
        }
    }
}
