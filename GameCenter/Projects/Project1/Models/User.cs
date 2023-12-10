using System;

namespace gameCenter.Project1.UserManegment.Models
{
    public class User1
    {
        public static int count = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; } // Added this
        public DateTime LastLogIn { get; set; } // Assuming you wanted this

        public User1(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            Status = UserStatusTypes.Logged_Off.ToString();
            Created = DateTime.Now;
        }
    }

    public enum UserStatusTypes
    {
        Logged_In,
        Logged_Off,
        Freeze
    }
}
