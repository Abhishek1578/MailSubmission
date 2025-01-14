using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Data.Entity;

namespace MailSubmission.Models
{
    public class UserDbContext:DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UserDbContext() : base("myconn")
        {

        }
        
    }
}