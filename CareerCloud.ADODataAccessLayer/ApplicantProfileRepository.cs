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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {


        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using(conn)
            {
                foreach(ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"Insert into [dbo].[Applicant_Profiles]
                    ([Id],[Login],[Current_Salary],[Current_Rate],[Currency],
                     [Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])
                    VALUES 
                     (@Id,@Login,@Current_Salary,@Current_Rate,@Currency,@Country_Code,@State_Province_Code,
                     @Street_Address,@City_Town,@Zip_Postal_Code)", conn);

                    cmd.Parameters.AddWithValue("@Id",poco.Id);
                    cmd.Parameters.AddWithValue("@Login",poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary",poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate",poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency",poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code",poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code",poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address",poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town",poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code",poco.PostalCode);

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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id]
                 ,[Login]
                  ,[Current_Salary]
                 ,[Current_Rate]
                 ,[Currency]
                 ,[Country_Code]
                 ,[State_Province_Code]
                 ,[Street_Address]
                 ,[City_Town]
                 ,[Zip_Postal_Code]
                 ,[Time_stamp]

                  FROM [JOB_PORTAL_DB].[dbo].[Applicant_Profiles]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantProfilePoco[] appPocos = new ApplicantProfilePoco[3000];
                while (rdr.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.CurrentSalary = (decimal)(rdr.IsDBNull(2) ? null : rdr[2]);
                    poco.CurrentRate = (decimal)(rdr.IsDBNull(3) ? null : rdr[3]);
                    poco.Currency = (string)(rdr.IsDBNull(4) ? null : rdr[4]);
                    poco.Country = (string)(rdr.IsDBNull(5) ? null : rdr[5]);
                    poco.Province = (string)(rdr.IsDBNull(6) ? null : rdr[6]);
                    poco.Street = (string)(rdr.IsDBNull(7) ? null : rdr[7]);
                    poco.City = (string)(rdr.IsDBNull(8) ? null : rdr[8]);
                    poco.PostalCode = (string)(rdr.IsDBNull(9) ? null : rdr[9]);
                    poco.TimeStamp = (byte[])rdr[10];


                    appPocos[x] = poco;
                    x++;

                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = $"DELETE Applicant_Profiles WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE[dbo].[Applicant_Profiles]
                     SET [Login]= @Login
                         ,[Current_Salary]= @Current_Salary
                         ,[Current_Rate]= @Current_Rate
                         ,[Currency]= @Currency
                         ,[Country_Code]= @Country_Code
                         ,[State_Province_Code]=@State_Province_Code
                         ,[Street_Address]=@Street_Address
                         ,[City_Town]=@City_Town
                         ,[Zip_Postal_Code]=@Zip_Postal_Code


                         WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();



                }
            }
        }
    }

}
