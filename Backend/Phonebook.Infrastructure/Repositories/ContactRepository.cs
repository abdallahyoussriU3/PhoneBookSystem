using Microsoft.EntityFrameworkCore;
using Phonebook.Domain.Entities;
using Phonebook.Domain.Interfaces;
using Phonebook.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            await _context.Contacts.AddAsync(contact, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return contact;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var contact = await _context.Contacts.FindAsync(new object[] { id }, cancellationToken);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Contacts.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Contact> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Contacts.AsNoTracking()
                                          .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Contact>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            query = query.ToLower(); 

            return await _context.Contacts
                .Where(c => EF.Functions.Like(c.Name.ToLower(), $"%{query}%") || 
                            EF.Functions.Like(c.Email.ToLower(), $"%{query}%") || 
                            EF.Functions.Like(c.PhoneNumber.ToLower(), $"%{query}%")) 
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }


        public async Task<bool> UpdateAsync(Contact contact, CancellationToken cancellationToken = default)
        {
            var existingContact = await _context.Contacts.FindAsync(new object[] { contact.Id }, cancellationToken);
            if (existingContact == null) return false;

            _context.Entry(existingContact).CurrentValues.SetValues(contact);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
