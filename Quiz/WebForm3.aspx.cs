using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quiz
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
        int score = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Hi  ");
            Response.Write(Session["email"]);
            Response.Write("\n");
            Response.Write("Mark the correct option and hit on submit to view results! 1 point for each correct answer!");
           
            con.Open();
            string s1 = "SELECT ques FROM ques WHERE id = 4";

            SqlCommand cmd = new SqlCommand(s1, con);
            string output = cmd.ExecuteScalar().ToString();
            TextBox1.Text = output;

           
            string s2 = "SELECT ques FROM ques WHERE id = 2";

            SqlCommand cmd1 = new SqlCommand(s2, con);
            string output1 = cmd1.ExecuteScalar().ToString();
            TextBox2.Text = output1;


            string s3 = "SELECT ques FROM ques WHERE id = 3";

            SqlCommand cmd2 = new SqlCommand(s3, con);
            string output2 = cmd2.ExecuteScalar().ToString();
            TextBox3.Text = output2;

            con.Close();

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

                      
            if (RadioButtonList1.SelectedItem.Text == "False")
            {
                Response.Write("   ");
                Response.Write("Correct");
                score++;
            }
            else
            {
                Response.Write("   ");
                Response.Write("Wrong");

            }

            if (RadioButtonList2.SelectedItem.Text == "True")
            {
                Response.Write("   ");
                Response.Write("Correct");
                score++;
            }
            else
            {
                Response.Write("   ");
                Response.Write("Wrong");
            }
            
            if (RadioButtonList3.SelectedItem.Text == "True")
            {
                Response.Write("   ");
                Response.Write("Correct");
                score++;
            }
            else
            {
                Response.Write("   ");
                Response.Write("Wrong");
            }

            Response.Write("    ");
            Response.Write("Your score :");
            Response.Write(score);


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm3.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("WebForm1.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename = add_lab.pdf");
            Response.TransmitFile(Server.MapPath("~/add_lab.pdf"));
            Response.End();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            String savePath = @"E:/games";
            if (FileUpload1.HasFile)
            {
                String fileName = FileUpload1.FileName;
                savePath += fileName;
                FileUpload1.SaveAs(savePath);
                Response.Write("File saved!");

            }
            else
            {
                Response.Write("You did not select a file to save!");
            }
        }
    }
}