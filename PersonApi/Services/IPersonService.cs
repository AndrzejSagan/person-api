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
        Task<Person> Add(Person newPerson);
        Task<Person> GetById(long id);
        Task<bool> Update(long id, Person person);
        Task<bool> Remove(long id);
    }
}
