using MameToppleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Interfaces
{
    public interface ICardService
    {
        IEnumerable<Card> GetAllCards();

        Card GetCard(int Id);
    }
}
