using MameToppleApi.Models;
using MameToppleApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Service
{
    public class DollService
    {
        public IEnumerable<Doll> GetAllDolls()
        {
            using(var _context = new ToppleDBContext())
            {
                var repo = new GenericRepository<Doll>(_context).GetAllAsync();
                var dolls = repo.Result.OrderBy(x => Guid.NewGuid()).AsEnumerable();
                return dolls;
            }
        }

        public IEnumerable<Doll> GetMissonDolls()
        {
            using(var _context = new ToppleDBContext())
            {
                var dolls = new GenericRepository<Doll>(_context).GetAllAsync().Result.OrderBy(x => Guid.NewGuid()).AsEnumerable();
                IEnumerable<Doll> result = dolls.Take(3);
                return result;
            }
        }
    }
}
