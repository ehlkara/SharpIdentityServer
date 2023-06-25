import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CallbackComponent } from './callback/callback.component';
import { AuthService } from './auth.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CallbackComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
