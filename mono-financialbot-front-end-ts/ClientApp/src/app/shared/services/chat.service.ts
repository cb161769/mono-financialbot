import { EventEmitter, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
import { User } from '../models/user';
import { Message } from '../models/message';
import { Chatgroup } from '../models/chatgroup';
import { environment } from '../../../environments/environment';
import { AuthService } from './auth.service';
import { getToken } from '../helpers/token';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private connection: signalR.HubConnection;
  onlineUsers = new EventEmitter<User[]>();
  actualMessages = new EventEmitter<Message[]>();
  newMessage = new EventEmitter<Message>();
  actualGroup = new EventEmitter<Chatgroup[]>();
  private baseUrl: string = environment.BASE_URL;
  constructor(private authService: AuthService) {
    const token = getToken();
    const options: signalR.IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return "" + token
      }
    };
    this.connection = new signalR.HubConnectionBuilder().withUrl(`${this.baseUrl}hub/chat`, options).build();
    this.startConnection();
  }
  private startConnection() {
    this.connection.serverTimeoutInMilliseconds = 36000000;
    this.connection.keepAliveIntervalInMilliseconds = 1800000;

    this.connection.start().then(() => {
      this.receiveonlineUsers();
      this.receiveactualMessages();
      this.receiveMessage();
      this.receiveGroup();
      this.addNewGroup("Global");

    }).catch((error: HttpErrorResponse) => {

    });
  }
  private receiveGroup() {
    this.connection.on("GroupChanged", (group: Chatgroup[]) => {
      this.actualGroup.emit(group);
    });
  }
  private receiveMessage() {
    this.connection.on("NewMessage", (message: Message) => {
      this.newMessage.emit(message);
    });
  }

  private receiveonlineUsers() {
    this.connection.on("UsersChanged", (response: User[]) => {

      this.onlineUsers.emit(response);
    });
  }

  private receiveactualMessages() {
    this.connection.on("actualMessages", (messages: Message[]) => {
      this.actualMessages.emit(messages)
    });
  }

  closeConnectionForCurrentClient() {
    const userName = this.authService.getCurrentUser().username;
    this.connection.invoke("DisconnectUser", userName).then(() => {
      this.authService.logout();
    }).catch(() => {
      /*  this.toastr.error("An error occurred while loging you out.", "Error", {
            positionClass: 'toast-bottom-right'
        });*/
    });
  }

  sendNewMessage(message: Message) {
    return this.connection.invoke("SendMessage", message);
  }
  addNewGroup(groupName: string) {
    return this.connection.invoke("AddNewGroup", groupName);
  }

  saveBotMessage(message: Message) {
    return this.connection.invoke("SaveBotMessage", message);
  }
}
