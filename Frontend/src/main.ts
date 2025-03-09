import { bootstrapApplication } from '@angular/platform-browser';
import { ContactListComponent } from './app/components/contact-list/contact-list.component';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(ContactListComponent, {
  providers: [
    provideHttpClient()
  ]
}).catch(err => console.error(err));