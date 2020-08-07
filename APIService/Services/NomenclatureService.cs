using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace APIService.Services
{

    public class NomenclatureService : BaseService<Nomenclature>
    {
        public NomenclatureService(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Nomenclature> nomenclature { get; set; }

        public override void add(Nomenclature item)
        {
            this.Add(item);
            this.SaveChanges();
        }

        public override Nomenclature get(int id)
        {
            return this.nomenclature.Find(id);
        }
        public override IList<Nomenclature> get(Nomenclature item)
        {
            return this.nomenclature.Where(l => l.name == item.name).ToList();
        }


        public override IList<Nomenclature> all()
        {
            return nomenclature.ToList();
        }

        public override Nomenclature update(int id, Nomenclature item)
        {
            var nom = this.nomenclature.Find(id);
            if (nom != null)
            {
                nom.name = item.name;
                nom.price = item.price;
                nom.fromDate = item.fromDate;
                nom.toDate = item.toDate;
                this.SaveChanges();
                return nom;
            }
            return null;

        }

        public override bool delete(int id)
        {
            var user = this.nomenclature.Find(id);
            if (user != null)
            {
                nomenclature.Remove(user);
                this.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
