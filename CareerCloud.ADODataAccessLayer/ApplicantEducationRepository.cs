using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantEducationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"Insert into [dbo].[Applicant_Educations]([Id],
                                       [Applicant],[Major],[Certificate_Diploma],
                                       [Start_Date],[Completion_Date],[Completion_Percent])
                      VALUES 
                      (@Id,@Applicant,@Major,@Certificate_Diploma,@Start_Date,@Completion_Date,
                      @Completion_Percent)", conn);

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[Applicant_Educations]",conn);
                
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantEducationPoco[] appPocos = new ApplicantEducationPoco[1000];
                while (rdr.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Major =rdr.GetString(2);
                    poco.CertificateDiploma =(string)(rdr.IsDBNull(3) ? null : rdr[3]);
                    poco.StartDate =(DateTime)(rdr.IsDBNull(4) ? null : rdr[4]);
                    poco.CompletionDate =(DateTime)(rdr.IsDBNull(5) ? null : rdr[5]);
                    poco.CompletionPercent = (byte?)rdr[6];
                    poco.TimeStamp = (byte[])rdr[7];

                    appPocos[x] = poco;
                    x++;

                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
            
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach(ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = $"DELETE Applicant_Educations WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach(ApplicantEducationPoco item in items)
                {
                    cmd.CommandText= @"UPDATE[dbo].[Applicant_Educations]
                    SET [Applicant]= @Applicant
                        ,[Major]= @Major
                        ,[Certificate_Diploma]= @Certificate_Diploma
                        ,[Start_Date]= @Start_Date
                        ,[Completion_Date]= @Completion_Date
                        ,[Completion_Percent]=@Completion_Percent
                        WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("@completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();



                }
            }

           

        }
    }
}
