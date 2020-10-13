import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { UserManager, UserManagerSettings, User } from "oidc-client";
import { BehaviorSubject, Observable, of } from "rxjs";

import { ConfigService } from "../core/config.service";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { IUserProfile } from "./user-profile";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private configService: ConfigService,
    private oidcSecurityService: OidcSecurityService
  ) {}

  /**Call IDP endpoint to confirm if the user is authenticated */
  checkAuth(): Observable<boolean> {
    return this.oidcSecurityService.checkAuth();
  }

  /**Start the authentication hand shake with the IDP server */
  startAuthentication(): void {
    return this.oidcSecurityService.authorize();
  }

  /**Get string authorization token */
  get token(): string {
    return this.oidcSecurityService.getToken();
  }

  /**User profile information */
  get userData(): Observable<IUserProfile> {
    return this.oidcSecurityService.userData$;
  }

  /**End the user session and free the resources */
  signout(): void {
    this.oidcSecurityService.logoff();
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: "https://localhost:5001",
    client_id: "ChatRoomWeb",
    redirect_uri: "http://localhost:4200/auth-callback",
    post_logout_redirect_uri: "http://localhost:4200/",
    response_type: "id_token token",
    scope: "openid profile email ChatRoom",
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    silent_redirect_uri: "http://localhost:4200/silent-refresh.html",
  };
}
