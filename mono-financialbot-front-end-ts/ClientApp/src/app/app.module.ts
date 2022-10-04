import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { JwtModule } from '@auth0/angular-jwt';
import { RegistrationComponent } from './components/registration/registration.component';
import { SharedModule } from './shared/shared.module';
import { ChatComponent } from './components/chat/chat.component';
import { AccessComponent } from './components/access/access.component';
import { routes } from './shared/Routes/routes';
import { AuthGuard } from './guards/auth.guard';
import { TokenInterceptor } from './shared/Interceptors/token.interceptor';
import { getToken } from './shared/helpers/token';
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
    SharedModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: getToken
      }
    }),
  ],
  providers: [AuthGuard, {
    provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true ,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
