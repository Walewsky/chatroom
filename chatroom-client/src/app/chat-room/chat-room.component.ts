import { ChangeDetectorRef } from "@angular/core";
import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";
import { AuthService } from "../authentication/auth.service";
import { IUserProfile } from "../authentication/user-profile";
import { Message } from "./chat-room.model";
import { ChatRoomService } from "./chat-room.service";

@Component({
  selector: "chat-room",
  templateUrl: "./chat-room.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatRoomComponent implements OnInit {
  message: Message = new Message();
  msgInbox: Message[] = [];
  user: IUserProfile;

  constructor(
    private chatRoomService: ChatRoomService,
    private chageDetector: ChangeDetectorRef,
    public authService: AuthService
  ) {}

  async ngOnInit() {
    this.authService.userData.subscribe((user) => {
      this.user = user;
      this.message.user = user?.name;
      this.chageDetector.detectChanges();
    });

    this.chatRoomService.getMessages().subscribe((response) => {
      this.msgInbox = response;
      this.chageDetector.detectChanges();
    });

    this.chatRoomService.retrieveMessages().subscribe((message: Message) => {
      this.msgInbox.push(message);
      this.chageDetector.detectChanges();
    });
  }

  send(): void {
    if (this.message) {
      if (this.message.user.length === 0 || this.message.content.length === 0) {
        window.alert("Both fields are required.");
        return;
      } else {
        this.message.timestamp = new Date();
        this.chatRoomService.postMessage(this.message);
      }
    }
  }

  logout() {
    this.authService.signout();
  }
}
