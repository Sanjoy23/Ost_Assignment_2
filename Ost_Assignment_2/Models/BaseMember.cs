using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ost_Assignment_2.Models
{
    public class BaseMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DataTable ValidateMemberAsDataTable(string UserName, string password)
        {
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            //string conStirngApp = ConfigurationManager.AppSettings["connStringApp"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            string CommandText = "select * from OSTMembers where UserName= '" + UserName + "' and Password = '"+password +"'";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            //table data
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            cmd.Dispose();
            sqlConnection.Close();
            return dataTable;
        }

        public List<BaseMember> ValidateMemberAsList(string UserName, string password)
        {
            List<BaseMember> listMember = new List<BaseMember>();
            string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            //string conStirngApp = ConfigurationManager.AppSettings["connStringApp"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            string CommandText = "select * from OSTMembers";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            //table data
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseMember member = new BaseMember();
                    member.Name = reader["Name"].ToString();
                    member.Password = reader["Password"].ToString();
                    listMember.Add(member);
                }
            }

            cmd.Dispose();
            sqlConnection.Close();
            return listMember;   
        }

        public string FindMemberByUserName(string UserName)
        {
            //BaseMember member = new BaseMember();
            string member = null;
            string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            string commandText = "FindMember";
			SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", UserName);

            SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					member = reader["UserName"].ToString();
				}
			}
			cmd.Dispose();
			sqlConnection.Close();
			return member;
        }

        public void ResetPassword(string UserName, string newPassowrd)
        {
			string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
			SqlConnection sqlConnection = new SqlConnection(connString);
			sqlConnection.Open();

			string commandText = "ResetPassword";
			SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserName", UserName);
			cmd.Parameters.AddWithValue("@NewPassword", newPassowrd);

			SqlDataReader reader = cmd.ExecuteReader();
			cmd.Dispose();
			sqlConnection.Close();
		}

	}
}