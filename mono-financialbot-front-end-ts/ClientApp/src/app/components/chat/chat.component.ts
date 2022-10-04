import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Chatgroup } from '../../shared/models/chatgroup';
import { Message } from '../../shared/models/message';
import { User } from '../../shared/models/user';
import { AuthService } from '../../shared/services/auth.service';
import { ChatService } from '../../shared/services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  @HostListener('window:unload', ['$event']) unloadHandler(event: any) {
    this.disconnectCurrentUser();
  };
  @ViewChild('panel', { static: false }) private chatElement!: ElementRef;
  connectedUsers: User[] = [];
  connectedUsersSubscription!: Subscription;
  actualMessages: Message[] = [];
  actualFilterMessages: Message[] = [];
  actualGroup: Chatgroup[] = [];
  actualGroupSuscription!: Subscription;
  actualMessagesSubscription!: Subscription;
  newMessageSubscription!: Subscription;
  actualUserName!: string;
  message = new FormControl('');
  selectedGroup: string = 'Global';
  constructor(private hub: ChatService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigateByUrl('/');
    }
    this.actualUserName = this.authService.getCurrentUser().username;
    this.suscribeToEvents();
  }
  filterMessages() {
    this.actualFilterMessages = this.actualMessages.filter(x => x.group == this.selectedGroup);
    this.chatScrollToBottom();
  }
  disconnectCurrentUser() {
    this.connectedUsersSubscription.unsubscribe();
    this.actualMessagesSubscription.unsubscribe();
    this.hub.closeConnectionForCurrentClient();
  }
  suscribeToEvents() {
    this.connectedUsersSubscription = this.hub.onlineUsers.subscribe((HubUsers: User[]) => {
      if (HubUsers !== undefined) {
        this.connectedUsers = HubUsers;
      }
    });

    this.actualMessagesSubscription = this.hub.actualMessages.subscribe((actualMessages: Message[]) => {
      console.log(actualMessages)
      if (actualMessages !== undefined) {
        this.actualMessages = actualMessages;
        this.filterMessages();
        this.chatScrollToBottom();
      }
    });
    this.actualGroupSuscription = this.hub.actualGroup.subscribe((groups: Chatgroup[]) => {
      if (groups !== undefined) {
        this.actualGroup = groups;
      }
    });

    this.newMessageSubscription = this.hub.newMessage.subscribe((newMessage: Message) => {

      if (newMessage !== undefined) {
        this.actualMessages.push(newMessage);
        this.filterMessages();
        this.chatScrollToBottom();

        if (newMessage.username === "#BOT") {
          this.hub.saveBotMessage(newMessage);
        }
      }
    });
  }
  send() {
    if (this.message.value.trim() === "") {
      return;
    }

    const newMessage: Message = {
      username: this.actualUserName,
      sendedDateUtc: new Date(),
      message: this.message.value,
      group: this.selectedGroup
    };

    this.hub.sendNewMessage(newMessage).then(() => {
      this.message.setValue("");
    }).catch((error: HttpErrorResponse) => {
      console.log(error);
    });
  }
  private chatScrollToBottom() {
    setTimeout(() => {
      this.chatElement.nativeElement.scrollTop = this.chatElement.nativeElement.scrollHeight;
    }, 100);
  }
  checkIsEnterKey(event: any) {
    const enterKeyCode = 13;
    if (event.keyCode === enterKeyCode) {
      this.send();
    }
  }
  addNewGroup() {
    let groupName = prompt("Please enter the name of the group");
    if (groupName != null) {
      this.hub.addNewGroup(groupName);
    }
  }
  getStyleClassByUserName(userName: string, category: string) {

    if (userName === "#BOT" && category == 'bot') {
      return true;
    } else if (userName === this.actualUserName && category == 'me') {
      return true
    } else if (userName !== this.actualUserName && category == 'other' && userName !== "#BOT") {
      return true;
    }

    return false;
  }

}
