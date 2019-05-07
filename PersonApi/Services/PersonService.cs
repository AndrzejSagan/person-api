using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonApi.Models;

namespace PersonApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonContext _context;
        public PersonService(PersonContext context)
        {
            _context = context;

            if (_context.People.Count() == 0)
            {
                _context.People.Add(new Person
                {
                    name = "Jan",
                    birthdate = new DateTime(1997, 1, 28),
                    email = "jan@gmail.com",
                    phone = 123456789
                });

                _context.SaveChanges();
            }

        }

        public async Task<Person> Add(Person newPerson)
        {

            _context.Add(newPerson);
            await _context.SaveChangesAsync();

            return newPerson;
           
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var people =  await _context.People.ToListAsync();

            return people;
        }

        public async Task<Person> GetById(long id)
        {
            var person = await _context.People.FindAsync(id);

            return person;
        }

        public async Task<bool> Remove(long id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return false;
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(long id, Person person)
        {
            if (id != person.id || !_context.People.Any(p => p.id == id))
            {
                return false;
            }

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
