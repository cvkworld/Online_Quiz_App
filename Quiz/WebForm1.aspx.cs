using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quiz
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm5.aspx");
        }


        public string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {


            String email = TextBox1.Text.ToString();  
            String password = TextBox2.Text;  
            string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;  
            SqlConnection connection = new SqlConnection(con);  
            connection.Open();   
            string passwords = encryption(password);  
            String query = "SELECT email, password FROM users WHERE (email = '" + email + "') AND (password = '"+passwords+"');";  
  
          SqlCommand cmd = new SqlCommand(query, connection);  
           SqlDataReader sqldr = cmd.ExecuteReader();  
             if (sqldr.Read())  
        {
                Session["email"] = TextBox1.Text;
                if (TextBox1.Text == "ben@gmail.com")
                {
                    Response.Redirect("WebForm2.aspx");

                }
                else
                {
                    Response.Redirect("WebForm3.aspx");
                }
                 
        }  
            else  
            {
                Response.Write("Incorrect credetials!!!"); 
                 
            }  
          
    connection.Close();  
}  
            /*
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            con.Open();
            string insertSql = "select COUNT(*)FROM users WHERE email='" + TextBox1.Text + "' and password='" + TextBox2.Text + "'";

            SqlCommand cmd = new SqlCommand(insertSql,con);

            
            string output = cmd.ExecuteScalar().ToString();
            
            if (output == "1")
            {
                Session["email"] = TextBox1.Text;
                if (TextBox1.Text == "ben@gmail.com")
                {
                    Response.Redirect("WebForm2.aspx");

                }
                else
                {
                    Response.Redirect("WebForm3.aspx");
                }
                Response.Redirect("WebForm3.aspx");
            }
            else
            {
                Response.Write("Invalid username or password");
                
            }

        */


        }
    }
