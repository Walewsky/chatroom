import { BrowserModule } from "@angular/platform-browser";
import { NgModule, APP_INITIALIZER } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AuthGuard } from "./authentication/auth.guard";
import { AuthService } from "./authentication/auth.service";
import { ChatRoomComponent } from "./chat-room/chat-room.component";
import { ChatRoomService } from "./chat-room/chat-room.service";
import { HttpClientModule } from "@angular/common/http";
import { ConfigService } from "./core/config.service";
import { LoginComponent } from "./account/login/login.component";
import {
  AuthModule,
  LogLevel,
  OidcConfigService,
} from "angular-auth-oidc-client";
import { FormsModule } from "@angular/forms";

export function configureAuth(oidcConfigService: OidcConfigService) {
  return () =>
    oidcConfigService.withConfig({
      stsServer: "https://localhost:5001",
      redirectUrl: "http://localhost:4200/",
      postLogoutRedirectUri: "http://localhost:4200/",
      clientId: "ChatRoomWeb",
      scope: "openid profile email ChatRoom",
      responseType: "id_token token",
      silentRenew: true,
      silentRenewUrl: `http://localhost:4200/silent-refresh.html`,
      logLevel: LogLevel.Debug,
    });
}

@NgModule({
  declarations: [AppComponent, ChatRoomComponent, LoginComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot(),
    FormsModule,
  ],
  providers: [
    AuthService,
    AuthGuard,
    ChatRoomService,
    ConfigService,
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
