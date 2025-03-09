using Phonebook.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Contact> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Contact> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken = default);
        Task<Contact> AddAsync(Contact contact, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Contact contact, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Contact>> SearchAsync(string query, CancellationToken cancellationToken = default);
    }
}
