using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {

                foreach (SystemCountryCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(
                    @"Insert into [dbo].[System_Country_Codes] ([Code],[Name])
                   VALUES 
                    (@Code,@Name)", conn);
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Code]
                ,[Name]
                
                 FROM [JOB_PORTAL_DB].[dbo].[System_Country_Codes]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SystemCountryCodePoco[] appPocos = new SystemCountryCodePoco[1000];
                while (rdr.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);
                  
                    appPocos[x] = poco;
                    x++;

                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = $"DELETE System_Country_Codes WHERE Code=@Code";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"UPDATE[dbo].[System_Country_Codes]
                    SET [Name]= @Name
                       
                    WHERE [Code]=@Code";

                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                   

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();



                }
            }
        }
    }
}
