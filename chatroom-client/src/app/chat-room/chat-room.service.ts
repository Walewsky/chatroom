import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Message } from "./chat-room.model";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { AuthService } from "../authentication/auth.service";
import { ConfigService } from "../core/config.service";
import * as signalR from "@microsoft/signalr";
import { IHttpConnectionOptions } from "@microsoft/signalr";

@Injectable()
export class ChatRoomService {
  private connection: signalR.HubConnection;
  private messagePublisher = new Subject<Message>();

  constructor(
    private http: HttpClient,
    private configService: ConfigService,
    private authService: AuthService
  ) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(configService.signalRChatHubURI, this.getHubConnectionOptions())
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection.onclose(async () => {
      await this.start();
    });

    this.connection.on("receiveMessage", (message) => {
      this.messagePublisher.next(message);
    });

    this.start();
  }

  // Start the connection
  public async start() {
    try {
      await this.connection.start();
      console.log("connected");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    }
  }

  public postMessage(message: Message) {
    this.http
      .post(
        `${this.configService.chatRoomApiURI}/chat`,
        message,
        this.getHttpOptions()
      )
      .subscribe((data) => console.log(data));
  }

  public getMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(
      `${this.configService.chatRoomApiURI}/chat`,
      this.getHttpOptions()
    );
  }

  public retrieveMessages(): Observable<Message> {
    return this.messagePublisher.asObservable();
  }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${this.authService.token}`,
      }),
    };
  }

  getHubConnectionOptions(): IHttpConnectionOptions {
    return {
      accessTokenFactory: () => {
        return this.authService.token;
      },
    };
  }
}
