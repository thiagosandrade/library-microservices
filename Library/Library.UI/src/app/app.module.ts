import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from "@angular/forms";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ListAuthorComponent } from './author/list-author/list-author.component';
import { AddAuthorComponent } from './author/add-author/add-author.component';
import { EditAuthorComponent } from './author/edit-author/edit-author.component';
import { DatePipe } from '@angular/common';
import { SharedModule } from './_shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ListAuthorComponent,
    AddAuthorComponent,
    EditAuthorComponent
  ],
  imports: [
    SharedModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
