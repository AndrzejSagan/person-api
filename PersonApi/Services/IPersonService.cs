using PersonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAll();
        Task<bool> Add(Person newPerson);
        Task<Person> GetById(long id);
        Task<bool> Update(Person person);
        Task<bool> Remove(long id);

    }
}
