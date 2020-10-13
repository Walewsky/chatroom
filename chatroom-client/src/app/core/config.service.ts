import { Injectable } from "@angular/core";

@Injectable()
export class ConfigService {
  constructor() {}

  get identityProviderURI() {
    return "https://localhost:5001/";
  }

  get chatRoomApiURI() {
    return "https://localhost:5011/api";
  }

  get signalRChatHubURI() {
    return "https://localhost:5011/api/chat/notify";
  }
}
