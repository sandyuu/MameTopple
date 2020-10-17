using MameToppleApi.Models;
using MameToppleApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Interfaces;

namespace MameToppleApi.Service
{
    public class CardService : ICardService
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
