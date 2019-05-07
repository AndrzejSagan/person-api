using PersonApi.Models;
using PersonApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApiTest
{
    class PersonServiceFake : IPersonService
    {

        private readonly List<Person> _peopleList;

        public PersonServiceFake()
        {
            _peopleList = new List<Person>(){

                new Person { id = 1, name = "Adam", birthdate = new DateTime(1998, 3, 14), email = "adam@gmail.com", phone = 123123123},
                new Person{ id = 2, name = "Ewa", birthdate = new DateTime(1995, 3, 10), email = "ewa@gmail.com", phone = 987654321}
            };
        }

        public Task<Person> Add(Person newPerson)
        {
            _peopleList.Add(newPerson);
            return Task.FromResult(newPerson);
        }

        public Task<IEnumerable<Person>> GetAll()
        {
            return Task.FromResult<IEnumerable<Person>>(_peopleList);
        }

        public Task<Person> GetById(long id)
        {
            return Task.FromResult(_peopleList.Find(p => p.id == id));
        }

        public Task<bool> Remove(long id)
        {
            var existing = _peopleList.Find(p => p.id == id);

            if (existing == null)
            {
                return Task.FromResult(false);
            }

            _peopleList.Remove(existing);

            return Task.FromResult(true);
        }

        public Task<bool> Update(long id, Person person)
        {
            if (id != person.id || !_peopleList.Any(p => p.id == id))
            {
                return Task.FromResult(false);
            }

            var personToUpdate = _peopleList.Where(p => p.id == id).First();
            var index = _peopleList.IndexOf(personToUpdate);

            if (index != -1)
            {
                _peopleList[index] = person;
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
