import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({providedIn: 'root'})
export class AppService {

  constructor(
    private authService: AuthService) { }

  initApp(): Promise<any> {
    return new Promise(async (resolve, reject) => {
      if (!this.authService.isAuthenticated()) {
        if (window.location.href.indexOf('id_token') >= 0) {
          await this.authService.completeAuthentication();
          resolve();
        } else if (window.location.href.indexOf('error') >= 0) {
          reject();
        } else {
          return this.authService.login();
        }
      }
    });
  }
}
