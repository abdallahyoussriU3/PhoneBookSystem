using Microsoft.AspNetCore.Mvc;
using Phonebook.Domain.Entities;
using Phonebook.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Phonebook.Application.DTOs.Contact;
using DevDash.model;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private APIResponse _response;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
        this._response = new APIResponse();
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<APIResponse>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var contacts = await _contactService.GetAllContacts(trackChanges: false, cancellationToken);

            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = contacts;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                =new List<string> { ex.Message.ToString() };

        }
        return _response;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APIResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var contact = await _contactService.GetContactById(id, trackChanges: false, cancellationToken);
            if (contact == null) return NotFound($"No contact found with ID {id}.");

            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = contact;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                = new List<string> { ex.Message.ToString() };
        }
        return _response;
    }

    [HttpPost("AddContact")]
    public async Task<ActionResult<APIResponse>> Create([FromBody] ContactCreateDto createContact, CancellationToken cancellationToken)
    {
        try
        {
            if (createContact == null) return BadRequest("Contact data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Contact contact = new()
            {
                Name = createContact.Name,
                PhoneNumber = createContact.PhoneNumber,
                Email = createContact.Email,
            };
            var createdContact = await _contactService.CreateContact(contact, cancellationToken);

            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.Created;
            return CreatedAtAction(nameof(GetById), new { id = createdContact.Id }, _response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                = new List<string> { ex.Message.ToString() };
        }
        return _response;
    }

    [HttpGet("search")]
    public async Task<ActionResult<APIResponse>> Search([FromQuery] string query, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(query)) return BadRequest("Search query cannot be empty.");

            var result = await _contactService.SearchContacts(query, cancellationToken);
            if (result == null || !result.Any()) return NotFound("No contacts found matching the search criteria.");

            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = result;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                = new List<string> { ex.Message.ToString() };
        }
        return _response;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] ContactUpdateDto updatedContact, CancellationToken cancellationToken)
    {
        try
        {
            if (updatedContact == null) return BadRequest("Updated contact data is required.");
            Contact contact = await _contactService.GetContactById(id, trackChanges: false, cancellationToken);
            if (contact == null) return NotFound("Contact Not Found");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            contact.Email = updatedContact.Email;
            contact.Name = updatedContact.Name;
            contact.PhoneNumber = updatedContact.PhoneNumber;

            var isUpdated = await _contactService.UpdateContact(contact, cancellationToken);

            if (!isUpdated) return NotFound($"No contact found with ID {id}.");

            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                = new List<string> { ex.Message.ToString() };
        }
        return _response;

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<APIResponse>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var isDeleted = await _contactService.DeleteContact(id, cancellationToken);
            if (!isDeleted) return NotFound($"No contact found with ID {id}.");

            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                = new List<string> { ex.Message.ToString() };
        }
        return _response;
    }
}
