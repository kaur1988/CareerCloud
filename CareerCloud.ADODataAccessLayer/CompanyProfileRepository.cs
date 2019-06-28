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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"Insert into [dbo].[Company_Profiles]
                  ([Id],[Registration_Date],[Company_Website],[Contact_Phone],
                                       [Contact_Name],[Company_Logo])
                     VALUES 
                      (@Id,@Registration_Date,@Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id],[Registration_Date],[Company_Website],[Contact_Phone],
                                       [Contact_Name],[Company_Logo],[Time_stamp]   FROM [dbo].[Company_Profiles]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyProfilePoco[] appPoco = new CompanyProfilePoco[1000];
                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    poco.CompanyWebsite = (string)(rdr.IsDBNull(2) ? null : rdr[2]);
                    poco.ContactPhone = rdr.GetString(3);
                    poco.ContactName = (string)(rdr.IsDBNull(4) ? null : rdr[4]);
                    poco.CompanyLogo = (byte[])(rdr.IsDBNull(5) ? null : rdr[5]);
                    poco.TimeStamp= (byte[])(rdr.IsDBNull(6) ? null : rdr[6]);


                    appPoco[x] = poco;
                    x++;
                }
                return appPoco.Where(a => a != null).ToList();





            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = $"DELETE Company_Profiles WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                     SET[Registration_Date]=@Registration_Date
                    ,[Company_Website]=@Company_Website
                    ,[Contact_Phone]=@Contact_Phone
                    ,[Contact_Name]=@Contact_Name
                    ,[Company_Logo]=@Company_Logo
                   
                     Where[Id]=@Id ";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();


                }
            }
        }
    }
}

