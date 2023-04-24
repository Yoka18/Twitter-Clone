using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    
    public class SQLCommands
    {
        string GetTweets = "SELECT * FROM tweet";
        string GetComments =  "";
        string GetUserTweets = "SELECT * FROM tweet WHERE username = @username";
        // KODUN KULLANILIŞI
        // string username = "verilen_username_değeri";
        // string query = "SELECT * FROM tweet WHERE username = @username";
        // SqlCommand command = new SqlCommand(query, connection);
        // command.Parameters.AddWithValue("@username", username);
        string GetTweetContent = "";
    }
}