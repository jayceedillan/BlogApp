﻿using Microsoft.AspNetCore.Identity;

namespace BlogApp.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<BlogPost> BlogPosts { get; set; }
    }

}
