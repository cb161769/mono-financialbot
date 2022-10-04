import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { RegistrationComponent } from './components/registration/registration.component';
import { ChatComponent } from './components/chat/chat.component';
import { AccessComponent } from './components/access/access.component';
import { routes } from './shared/Routes/routes';
import { AuthGuard } from './guards/auth.guard';
import { TokenInterceptor } from './shared/Interceptors/token.interceptor';
import { ChatService } from './shared/services/chat.service';
@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    ChatComponent,
    AccessComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,

  ],
  providers: [AuthGuard, { provide: JWT_OPTIONS, useValue: JWT_OPTIONS }, {
    provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true ,
  }, JwtHelperService, ChatService],
  bootstrap: [AppComponent]
})
export class AppModule { }
