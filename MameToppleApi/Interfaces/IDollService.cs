using MameToppleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Interface
{
    public interface IDollService
    {
        ICollection<Doll> GetAllDolls();

        ICollection<Doll> GetMissonDolls();
    }
}
