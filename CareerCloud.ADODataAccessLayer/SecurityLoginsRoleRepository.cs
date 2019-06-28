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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"Insert into [dbo].[Security_Logins_Roles]([Id],[Login],[Role])
                 values (@Id,@Login,@Role)",conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id],[Login],[Role],[Time_stamp]
                FROM [JOB_PORTAL_DB].[dbo].[Security_Logins_Roles]";
                conn.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginsRolePoco[] appPocos = new SecurityLoginsRolePoco[100];
                while (rdr.Read())
                {
                    SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.Role = rdr.GetGuid(2);
                    poco.TimeStamp = (byte[])(rdr.IsDBNull(3)?null:rdr[3]);

                    appPocos[x] = poco;
                    x++;

                }
                return appPocos.Where(a => a != null).ToList();



            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandText = $"DELETE Security_Logins_Roles WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Role", item.Role);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandText = @"UPDATE[dbo].[Security_Logins_Roles]
                    SET [Role]= @Role
                       ,[Login]=@Login
                       
                    WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Login", item.Login);


                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
    }
}
