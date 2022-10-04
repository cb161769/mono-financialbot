import { Routes } from "@angular/router";
import { AccessComponent } from "../../components/access/access.component";
import { ChatComponent } from "../../components/chat/chat.component";
import { RegistrationComponent } from "../../components/registration/registration.component";
import { AuthGuard } from "../../guards/auth.guard";

export const routes: Routes = [
  {
    path: '',
    component: AccessComponent  
  }, {
    path: 'register',
    component: RegistrationComponent
  },
  {
    path: '',
    component: ChatComponent,
    children: [
      {
        path: 'chat',
        canActivate: [AuthGuard],
        component:ChatComponent
      }
    ]
  }
]
