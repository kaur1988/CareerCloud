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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {

                foreach (CompanyJobPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(
                    @"Insert into [dbo].[Company_Jobs] ([Id],[Company],[Profile_Created],[Is_Inactive],[Is_Company_Hidden])
                   VALUES 
                    (@Id,@Company,@Profile_Created,@Is_Inactive,@Is_Company_Hidden)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id]
                ,[Company]
                ,[Profile_Created]
                ,[Is_Inactive]
                ,[Is_Company_Hidden]
                ,[Time_stamp]
                 FROM [JOB_PORTAL_DB].[dbo].[Company_Jobs]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobPoco[] appPocos = new CompanyJobPoco[5000];
                while (rdr.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.ProfileCreated = rdr.GetDateTime(2);
                    poco.IsInactive = rdr.GetBoolean(3);
                    poco.IsCompanyHidden = rdr.GetBoolean(4);
                    poco.TimeStamp= (byte[])(rdr.IsDBNull(5) ? null : rdr[5]);
                    appPocos[x] = poco;
                    x++;

                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = $"DELETE Company_Jobs WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = @"UPDATE[dbo].[Company_Jobs]
                    SET [Company]= @Company
                        ,[Profile_Created]= @Profile_Created
                        ,[Is_Inactive]= @Is_Inactive
                        ,[Is_Company_Hidden]=@Is_Company_Hidden
                        
                        WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);


                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                                       
                }
            }
        }
    }
}
