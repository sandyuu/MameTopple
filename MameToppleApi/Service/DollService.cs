using MameToppleApi.Models;
using MameToppleApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Interface;

namespace MameToppleApi.Service
{
    public class DollService : IDollService
    {
        private readonly IRepository<Doll> _repository;

        public DollService(IRepository<Doll> repository)
        {
            _repository = repository;
        }

        public ICollection<Doll> GetAllDolls()
        {
            return _repository.GetAllAsync().Result.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public ICollection<Doll> GetMissonDolls()
        {
            return _repository.GetAllAsync().Result.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
        }
    }
}
