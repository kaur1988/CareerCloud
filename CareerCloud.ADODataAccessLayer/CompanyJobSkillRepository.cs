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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"Insert into [dbo].[Company_Job_Skills]([Id],[Job],[Skill],[Skill_Level],[Importance])
                 values (@Id,@Job,@Skill,@Skill_Level,@Importance)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", poco.Importance);
                   
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id],[Job],[Skill],[Skill_Level],[Importance],[Time_stamp]
                FROM [JOB_PORTAL_DB].[dbo].[Company_Job_Skills]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobSkillPoco[] appPocos = new CompanyJobSkillPoco[6000];
                while (rdr.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.Skill = rdr.GetString(2);
                    poco.SkillLevel = rdr.GetString(3);
                    poco.Importance = rdr.GetInt32(4);
                    poco.TimeStamp = (byte[])rdr[5];

                    appPocos[x] = poco;
                    x++;

                }
                return appPocos.Where(a => a != null).ToList();



            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }


        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = $"DELETE Company_Job_Skills WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = @"UPDATE[dbo].[Company_Job_Skills]
                    SET [Job]= @Job
                       ,[Skill]=@Skill
                       ,[Skill_Level]=@Skill_Level
                       ,[Importance]=@Importance
                       
                    WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);


                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
    }
}
