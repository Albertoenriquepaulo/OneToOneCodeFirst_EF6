﻿namespace OneToOneExample.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }

        public virtual UserActivation? UserActivation { get; set; }
    }
}
