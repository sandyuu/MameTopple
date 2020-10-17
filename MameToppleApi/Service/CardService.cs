using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Service
{
    public class CardService
    {
        private readonly IRepository<Card> _repository;

        public CardService(IRepository<Card> repository)
        {
            _repository = repository;
        }

        public List<Card> GetAllCards()
        {
            return _repository.GetAllAsync().Result.OrderBy(x => x.Id).ToList();
        }

        public Card GetCard(int Id)
        {
            return _repository.GetOne(x => x.Id == Id);
        }
    }
}
