using Parse3.Models;
using System.Linq;
using Parse3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Parse3.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Prices.Any())
            {
                return;   // DB has been seeded
            }

            var prices = new Price[]
            {
            new Price{ coderab="201", name="Разборка", cost=100},
            new Price{ coderab="202", name="Сборка", cost=150},
            new Price{ coderab="203", name="Пайка", cost=200},
            new Price{ coderab="204", name="Замена", cost=300},
            new Price{ coderab="205", name="Сушка", cost=250},
            };
              foreach (Price s in prices)
                {
                    context.Prices.Add(s);
                }
                context.SaveChanges();


            var move = new MoveDB[]
            {
            new MoveDB{coderem="1.1", coderab="201", name=1},
            new MoveDB{coderem="1.1", coderab="202", name=1},
            new MoveDB{coderem="1.1", coderab="203", name=1},
            new MoveDB{coderem="1.2", coderab="204", name=1},
            new MoveDB{coderem="2.2", coderab="204", name=2},
            new MoveDB{coderem="2.3", coderab="205", name=2},
            };
            foreach (MoveDB r in move)
            {
                context.MoveDBs.Add(r);
            }
            context.SaveChanges();
        }

        
    }
}
