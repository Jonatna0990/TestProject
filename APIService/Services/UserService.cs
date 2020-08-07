using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace APIService.Services
{
    public class UserService : BaseService<Users>
    {
        public UserService(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> users { get; set; }


        public override void add(Users item)
        {
            this.Add(item);
            this.SaveChanges();
        }

        public override Users get(int id)
        {
            return this.users.Find(id);
        }

        public override IList<Users> get(Users item)
        {
            return this.users.Where(l => l.login == item.login && l.pass == item.pass).ToList();
        }


        public override IList<Users> all()
        {
            return users.ToList();
        }

        public override Users update(int id, Users item)
        {
            var user = this.users.Find(id);
            if (user != null)
            {
                user.login = item.login;
                user.pass = item.pass;
                this.SaveChanges();
                return user;
            }
            return null;

        }

        public override bool delete(int id)
        {
            var user = this.users.Find(id);
            if (user != null)
            {
                users.Remove(user);
                this.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
