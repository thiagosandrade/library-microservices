import { MessageNotifierService } from './messageNotifier/messageNotifier.service';
import { SignalRService } from './signalR/signalR.service';
import { MessageNotifierComponent } from './messageNotifier/messageNotifier.component';
import { GrowlModule } from 'primeng/growl';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
         CommonModule,
         GrowlModule,
    ],
    declarations: [
         MessageNotifierComponent,
    ],
    exports: [
         MessageNotifierComponent,
         ReactiveFormsModule,
         CommonModule,
         FormsModule
    ],
    providers: [
         SignalRService,
         MessageNotifierService
     ]
 })
 export class SharedModule { 
   
 }