import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { DatePipe } from '@angular/common';
import { SharedModule } from './_shared/shared.module';
import { AuthModule } from './_guards/auth.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AuthModule,
    SharedModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  exports: [
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
