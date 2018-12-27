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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            /*
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string insertSql = "INSERT INTO users(email,password)values(@email,@password)";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            
            cmd.Connection = con;
            
            cmd.CommandType = CommandType.Text;
            
            cmd.CommandText = insertSql;

            SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 50);
            
             email.Value = TextBox1.Text.ToString();
            
             cmd.Parameters.Add(email);

            SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 50);

            password.Value = TextBox2.Text.ToString();

            cmd.Parameters.Add(password);

            try

        {
                
            con.Open();
                
            cmd.ExecuteNonQuery();

                Response.Redirect("WebForm1.aspx");
                
        }
            
        catch (SqlException ex)

        {
                
            string errorMessage = "Error in registering user";
                
            errorMessage += ex.Message;
               
            throw new Exception(errorMessage);
                



        }
            
        finally

        {
                
            con.Close();
                
        }
            
    
    */


            string email = TextBox1.Text.ToString();
            String password = TextBox2.Text;
            //Get the encrypt the password by using the class  
            string pass = encryption(password);
            Label1.Text = pass;
            //Check whether the UseName and password are Empty  
            if (email.Length > 0 && password.Length > 0)
            {
                //creating the connection string              
                string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connection);
                String passwords = encryption(password);
                con.Open();
                // Check whether the Username Found in the Existing DB  
                String search = "SELECT * FROM users WHERE (email = '" + email + "');";
                SqlCommand cmds = new SqlCommand(search, con);
                SqlDataReader sqldrs = cmds.ExecuteReader();
                if (sqldrs.Read())
                {
                    String passed = (string)sqldrs["Password"];
                    Label1.Text = "Username Already Taken";
                }
                else
                {
                    try
                    {
                        // if the Username not found create the new user accound  
                        string sql = "INSERT INTO users (email, password) VALUES ('" + email + "','" + passwords + "');";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        sqldrs.Close();
                        cmd.ExecuteNonQuery();
                        String Message = "saved Successfully";
                        Label1.Text = Message.ToString();
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        Response.Redirect("WebForm1.aspx");
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = ex.ToString();
                    }
                    con.Close();
                }
            }

            else
            {
                String Message = "Username or Password is empty";
                Label1.Text = Message.ToString();
            }
        }


    }
    }
