public static List<Topic> SearchTopics(string keyword)
{
    var topics = new List<Topic>();
    string connectionString = GetConnectionString();
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        string query = "SELECT Id, Title, Description, Teacher, Student, CreatedAt FROM Topics WHERE Title LIKE @Keyword";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
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