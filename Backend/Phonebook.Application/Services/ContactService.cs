using Phonebook.Application.Interfaces;
using Phonebook.Domain.Entities;
using Phonebook.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts(bool trackChanges, CancellationToken cancellationToken = default)
        {
            return trackChanges
                ? await _contactRepository.GetAllAsync(cancellationToken)
                : await _contactRepository.GetAllAsync(cancellationToken); // هنا ممكن لاحقًا نعمل نسخة بدون `Tracking`
        }

        public async Task<Contact> GetContactById(int id, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return trackChanges
                ? await _contactRepository.GetByIdAsync(id, cancellationToken)
                : await _contactRepository.GetByIdNoTrackingAsync(id, cancellationToken);
        }

        public async Task<Contact> CreateContact(Contact contact, CancellationToken cancellationToken = default)
        {
            return await _contactRepository.AddAsync(contact, cancellationToken);
        }

        public async Task<bool> UpdateContact(Contact contact, CancellationToken cancellationToken = default)
        {
            return await _contactRepository.UpdateAsync(contact, cancellationToken);
        }

        public async Task<bool> DeleteContact(int id, CancellationToken cancellationToken = default)
        {
            return await _contactRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Contact>> SearchContacts(string query, CancellationToken cancellationToken = default)
        {
            return await _contactRepository.SearchAsync(query, cancellationToken);
        }
    }
}
