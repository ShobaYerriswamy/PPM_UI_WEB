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
    public class ProjectDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();

        //Get All Projects
        public List<Project> GetAllProjects()
        {
            List<Project> projecteList = new List<Project>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProjects";
                MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                DataTable dtProjects = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProjects);
                connection.Close();

                foreach (DataRow dr in dtProjects.Rows)
                {
                    projecteList.Add(new Project
                    {
                        ProjectId = Convert.ToInt32(dr["ProjectId"]),
                        ProjectName = dr["ProjectName"].ToString(),
                        StartDate = dr["StartDate"].ToString(),
                        EndDate = dr["EndDate"].ToString(),
                        
                    });
                }
            }

            return projecteList;
        }

        //Insert Projects

        public bool InsertProject (Project project)
        {
            int id = 0;
      
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command =  new MySqlCommand("sp_InsertProjects", connection);
                command.CommandType = CommandType.StoredProcedure;
                // command.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                command.Parameters.AddWithValue("@p_ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@p_StartDate", project.StartDate);
                command.Parameters.AddWithValue("@p_EndDate", project.EndDate);
                
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

        //Get Projects By ProjectId
        public List<Project> GetProjectsById(int ProjectId)
        {
            List<Project> projecteList = new List<Project>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProjectById ";
                command.Parameters.AddWithValue("@p_ProjectId", ProjectId);
                MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                DataTable dtProjects = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProjects);
                connection.Close();

                foreach (DataRow dr in dtProjects.Rows)
                {
                    projecteList.Add(new Project
                    {
                        ProjectId = Convert.ToInt32(dr["ProjectId"]),
                        ProjectName = dr["ProjectName"].ToString(),
                        StartDate = dr["StartDate"].ToString(),
                        EndDate = dr["EndDate"].ToString(),
                        
                    });
                }
            }

            return projecteList;
        }

        //Update Projects

        public bool UpdateProject (Project project)
        {
            int i = 0;
      
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command =  new MySqlCommand("sp_UpdateProject ", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_ProjectId", project.ProjectId);
                command.Parameters.AddWithValue("@p_ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@p_StartDate", project.StartDate);
                command.Parameters.AddWithValue("@p_EndDate", project.EndDate);
                
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

        //Delete Project By ProjectId
    
        public string DeleteProject(int projectId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand("sp_DeleteProject", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_ProjectId", projectId);
            
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
            
                    if (rowsAffected > 0)
                    {
                        return "Project Deleted Successfully";
                    }
                    else
                    {
                        return "Deletion failed. Project Not Found";
                    }
                }
                catch (Exception ex)
                {
                    return "Error Deleting Project: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

    
