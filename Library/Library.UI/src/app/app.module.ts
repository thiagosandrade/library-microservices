import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { DatePipe } from '@angular/common';
import { SharedModule } from './_shared/shared.module';
import { AuthModule } from './_guards/auth.module';

import { StoreModule } from '@ngrx/store';
import { appReducers } from './store/reducers/app.reducers';
import { EffectsModule } from '@ngrx/effects';
import { AuthorEffects } from './store/effects/author.effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot([AuthorEffects]),
    StoreDevtoolsModule.instrument({
      maxAge: 25, // Retains last 25 states
      logOnly: environment.production, // Restrict extension to log-only mode
    }),
    AuthModule,
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  exports: [
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
