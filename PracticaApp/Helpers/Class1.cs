using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration; 
using PracticaWpfApp.Models;

namespace PracticaWpfApp.Helpers
{
    public static class DatabaseHelper
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PracticaDB"].ConnectionString;
        }

        public static List<Topic> GetTopics()
        {
            var topics = new List<Topic>();
            string connectionString = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Title, Description, Teacher, Student, CreatedAt FROM Topics";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    topics.Add(new Topic
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Teacher = reader.GetString(3),
                        Student = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CreatedAt = reader.GetDateTime(5)
                    });
                }
            }
            return topics;
        }

        public static void AddTopic(Topic topic)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Topics (Title, Description, Teacher, Student, CreatedAt) 
                                 VALUES (@Title, @Description, @Teacher, @Student, @CreatedAt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", topic.Title);
                cmd.Parameters.AddWithValue("@Description", (object)topic.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Teacher", topic.Teacher);
                cmd.Parameters.AddWithValue("@Student", (object)topic.Student ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", topic.CreatedAt);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateTopic(Topic topic)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Topics 
                                 SET Title = @Title, Description = @Description, Teacher = @Teacher, Student = @Student 
                                 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", topic.Id);
                cmd.Parameters.AddWithValue("@Title", topic.Title);
                cmd.Parameters.AddWithValue("@Description", (object)topic.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Teacher", topic.Teacher);
                cmd.Parameters.AddWithValue("@Student", (object)topic.Student ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteTopic(int id)
        {
            string connectionString = GetConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Topics WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}