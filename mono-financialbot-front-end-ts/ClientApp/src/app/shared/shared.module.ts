import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { ChatService } from './services/chat.service';



@NgModule({
  declarations: [],
  providers: [AuthService,ChatService],
  imports: [
    CommonModule
  ],
  exports:[SharedModule]
})
export class SharedModule { }
