import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserManager, User } from 'oidc-client';

@Injectable({providedIn: 'root'})
export class AuthService {

  public user: User;
  private userManager: UserManager;

  constructor() {
    this.userManager = new UserManager({
      authority: environment.authority,
      client_id: environment.clientId,
      redirect_uri: environment.redirectUri,
      response_type: environment.responseType,
      scope: environment.scope
    });
  }

  login() {
    return this.userManager.signinRedirect();
  }

  async completeAuthentication() {
    this.user = await this.userManager.signinRedirectCallback();
  }

  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  signout() {
    this.userManager.signoutRedirect();
  }

}
