import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppService } from './app.service';
import { AuthInterceptor } from './auth.interceptor';

export function initApp(appService: AppService) {
  return () => appService.initApp();
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: APP_INITIALIZER, useFactory: initApp, deps: [AppService], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
