﻿namespace API.Application.Users.DTO
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
