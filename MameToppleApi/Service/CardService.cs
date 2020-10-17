using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Service
{
    public class CardService : ICardService
    {
        private readonly IRepository<Card> _repository;

        public CardService(IRepository<Card> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Card> GetAllCards()
        {
            return _repository.GetAllAsync().Result.OrderBy(x => x.Id).AsEnumerable();
        }

        public Card GetCard(int Id)
        {
            return _repository.GetOne(x => x.Id == Id);
        }
    }
}
