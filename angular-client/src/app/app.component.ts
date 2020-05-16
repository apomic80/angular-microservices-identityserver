import { Component, OnInit } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [Location, {provide: LocationStrategy, useClass: PathLocationStrategy}],
})
export class AppComponent {
  constructor(
    location: Location,
    private authService: AuthService,
    private httpClient: HttpClient) {
    location.replaceState('/');
  }

  callMicroservice1() {
    this.httpClient.get(environment.microservice1Url)
      .subscribe(
        data => alert('SUCCESS: ' + JSON.stringify(data)),
        error => alert('ERROR: ' + JSON.stringify(error)));
  }

  callMicroservice2() {
    this.httpClient.get(environment.microservice2Url)
      .subscribe(
        data => alert('SUCCESS: ' + JSON.stringify(data)),
        error => alert('ERROR: ' + JSON.stringify(error)));
  }

  signout() {
    this.authService.signout();
  }
}
