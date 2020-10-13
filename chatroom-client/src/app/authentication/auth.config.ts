import { InjectionToken } from "@angular/core";

export interface IAuthConfigOptions {
  isLoaded?: Promise<void>;
  identityProviderUrl: string;
  signInCallback: string;
  postLogOutUrl: string;
  identityProviderClientId: string;
  identityProviderScopes: string;
  silentSignInCallback: string;
}

export const AUTH_CONFIG_VALUES = new InjectionToken<IAuthConfigOptions>(
  "AUTHTORIZATION CONFIGURATION VALUES TOKEN"
);
