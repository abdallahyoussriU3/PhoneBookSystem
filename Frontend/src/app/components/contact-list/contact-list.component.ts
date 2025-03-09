import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ContactService } from "../../services/contact.service";
import { IContact } from "../../models/contact.model";
import { handleApiError } from "../../utils/helpers";

@Component({
  selector: "app-contact-list",
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: "./contact-list.component.html",
  styleUrls: ["./contact-list.component.css"],
})
export class ContactListComponent implements OnInit {
  contacts: IContact[] = [];
  searchQuery: string = "";
  showAddForm: boolean = false;
  editingContact: boolean = false;
  currentContact: Partial<IContact> = {};
  errorMessages: string[] = [];

  constructor(private contactService: ContactService) {}

  ngOnInit() {
    this.loadContacts();
  }

  loadContacts() {
    this.contactService.getAllContacts().subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.contacts = [...response.result];
          this.errorMessages = [];
        } else {
          this.errorMessages = ["Failed to load contacts."];
        }
      },
      error: (error) => {
        this.errorMessages = handleApiError(error);
      },
    });
  }

  onSearchButtonClick() {
    if (this.searchQuery.trim()) {
      this.contactService.searchContacts(this.searchQuery).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.contacts = response.result;
            this.errorMessages = [];
          } else {
            this.errorMessages = ["No contacts found matching the search."];
          }
        },
        error: (error) => {
          this.errorMessages = handleApiError(error);
        },
      });
    } else {
      this.loadContacts();
    }
  }

  showAddContactForm() {
    this.showAddForm = true;
    this.editingContact = false;
    this.currentContact = {};
  }

  editContact(contact: IContact) {
    this.editingContact = true;
    this.showAddForm = false;
    this.currentContact = { ...contact };
  }

  cancelEdit() {
    this.editingContact = false;
    this.showAddForm = false;
    this.currentContact = {};
    this.errorMessages = [];
  }

  addContact() {
    if (
      this.currentContact.name &&
      this.currentContact.phoneNumber &&
      this.currentContact.email
    ) {
      this.contactService
        .addContact(
          this.currentContact.name,
          this.currentContact.phoneNumber,
          this.currentContact.email
        )
        .subscribe({
          next: (response) => {
            if (response.isSuccess) {
              this.loadContacts();
              this.showAddForm = false;
              this.errorMessages = [];
            } else {
              this.errorMessages = ["Failed to add contact."];
            }
          },
          error: (error) => {
            this.errorMessages = handleApiError(error);
          },
        });
    } else {
      this.errorMessages = ["Please fill all fields."];
    }
  }

  updateContact() {
    if (
      this.currentContact.id &&
      this.currentContact.name &&
      this.currentContact.phoneNumber &&
      this.currentContact.email
    ) {
      this.contactService
        .updateContact(this.currentContact.id.toString(), {
          name: this.currentContact.name,
          phoneNumber: this.currentContact.phoneNumber,
          email: this.currentContact.email,
        })
        .subscribe({
          next: (response) => {
            if (response.isSuccess) {
              this.loadContacts();
              this.editingContact = false;
              this.errorMessages = [];
            } else {
              this.errorMessages = ["Failed to update contact."];
            }
          },
          error: (error) => {
            this.errorMessages = handleApiError(error);
          },
        });
    } else {
      this.errorMessages = ["Please fill all fields for update."];
    }
  }

  deleteContact(id: number) {
    if (confirm("Are you sure you want to delete this contact?")) {
      this.contactService.deleteContact(id).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.contacts = this.contacts.filter(
              (contact) => contact.id !== id
            );
            this.errorMessages = [];
          } else {
            this.errorMessages = ["Failed to delete contact."];
          }
        },
        error: (error) => {
          this.errorMessages = handleApiError(error);
        },
      });
    }
  }
}
