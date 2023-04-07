using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using PPM.DAL.Models;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using PPM.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace PPM.DAL
{
    public class RoleDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();

        //Get All Roles
        public List<Role> GetAllRoles()
        {
            List<Role> roleList = new List<Role>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllRoles";
                MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                DataTable dtRoles = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRoles);
                connection.Close();

                foreach (DataRow dr in dtRoles.Rows)
                {
                    roleList.Add(new Role
                    {
                        RoleId = Convert.ToInt32(dr["RoleId"]),
                        RoleName = dr["RoleName"].ToString(),
                       
                        
                    });
                }
            }

            return roleList;
        }

        //Insert Role

        public bool InsertRole(Role role)
        {
            int id = 0;
      
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command =  new MySqlCommand("sp_InsertRoles", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_RoleName", role.RoleName);
                
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get Roles By RoleId
        public List<Role> GetRolesById(int RoleId)
        {
            List<Role> roleList = new List<Role>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetRoleById";
                command.Parameters.AddWithValue("@p_RoleId", RoleId);
                MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                DataTable dtRoles = new DataTable();

                connection.Open();
                sqlDA.Fill(dtRoles);
                connection.Close();

                foreach (DataRow dr in dtRoles.Rows)
                {
                    roleList.Add(new Role
                    {
                        RoleId = Convert.ToInt32(dr["RoleId"]),
                        RoleName = dr["RoleName"].ToString(),
                       
                        
                    });
                }
            }

            return roleList;
        }

        //Update Role

        public bool UpdateRole(Role role)
        {
            int i = 0;
      
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command =  new MySqlCommand("sp_UpdateRoles", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_RoleId", role.RoleId);
                command.Parameters.AddWithValue("@p_RoleName", role.RoleName);
                
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Role By RoleId
        
        public string DeleteRole(int roleId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand("sp_DeleteRole", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_RoleId", roleId);
            
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
            
                    if (rowsAffected > 0)
                    {
                        return "Role Deleted Successfully";
                    }
                    else
                    {
                        return "Deletion failed. Role Not Found";
                    }
                }
                catch (Exception ex)
                {
                    return "Error Deleting Role: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
