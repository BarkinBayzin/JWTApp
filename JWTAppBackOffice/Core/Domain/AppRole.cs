﻿namespace JWTAppBackOffice.Core.Domain
{
    public class AppRole
    {
        public int Id { get; set; }

        public string Definition { get; set; }

        public virtual List<AppUser> AppUsers { get; set; } = new List<AppUser>();
    }
}