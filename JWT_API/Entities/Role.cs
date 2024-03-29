﻿namespace JWT_API.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
