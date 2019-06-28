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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (CompanyJobDescriptionPoco item in items)
                    {
                        cmd.CommandText = @"Insert into [dbo].[Company_Jobs_Descriptions]
                      ([Id],[Job],[Job_Name],[Job_Descriptions])
                        VALUES 
                      (@Id,@Job,@Job_Name,@Job_Descriptions)";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Job", item.Job);
                        cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                        cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                     


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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"Select [Id],[Job],[Job_Name],[Job_Descriptions],[Time_stamp]                                       
                                    FROM [dbo].[Company_Jobs_Descriptions]";
                    conn.Open();
                    int x = 0;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    CompanyJobDescriptionPoco[] appPoco = new CompanyJobDescriptionPoco[4000];
                    while (rdr.Read())
                    {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                        poco.Id = rdr.GetGuid(0);
                        poco.Job = rdr.GetGuid(1);
                        poco.JobName =rdr.GetString(2);
                        poco.JobDescriptions = rdr.GetString(3);
                        poco.TimeStamp = (byte[])rdr[4];

                    appPoco[x] = poco;
                        x++;
                    }
                    return appPoco.Where(a => a != null).ToList();

                }
            }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
                IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
                return pocos.Where(where).FirstOrDefault();
            }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (CompanyJobDescriptionPoco item in items)
                    {
                        cmd.CommandText = $"DELETE Company_Jobs_Descriptions WHERE ID=@ID";
                        cmd.Parameters.AddWithValue("@ID", item.Id);
                        conn.Open();
                        int rowEffected = cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                }
            }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (CompanyJobDescriptionPoco item in items)
                    {
                        cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                       SET[Job]=@Job
                      ,[Job_Name]=@Job_Name
                      ,[Job_Descriptions]=@Job_Descriptions
                                        
                        Where[Id]=@Id ";

                        cmd.Parameters.AddWithValue("Id", item.Id);
                        cmd.Parameters.AddWithValue("@Job", item.Job);
                        cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                        cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                       

                        conn.Open();
                        int rowEffected = cmd.ExecuteNonQuery();
                        conn.Close();


                    }
                }
        }
    }
}
