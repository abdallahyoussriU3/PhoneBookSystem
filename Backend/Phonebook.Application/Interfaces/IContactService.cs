using Phonebook.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContacts(bool trackChanges, CancellationToken cancellationToken = default);
        Task<Contact> GetContactById(int id, bool trackChanges, CancellationToken cancellationToken = default);
        Task<Contact> CreateContact(Contact contact, CancellationToken cancellationToken = default);
        Task<bool> UpdateContact(Contact contact, CancellationToken cancellationToken = default);
        Task<bool> DeleteContact(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Contact>> SearchContacts(string query, CancellationToken cancellationToken = default);
    }
}
