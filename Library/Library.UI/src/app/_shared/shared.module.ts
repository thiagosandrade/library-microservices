import { MessageNotifierService } from './messageNotifier/messageNotifier.service';
import { SignalRService } from './signalR/signalR.service';
import { MessageNotifierComponent } from './messageNotifier/messageNotifier.component';
import { ToastModule } from 'primeng/toast';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';

@NgModule({
    imports: [
         CommonModule,
         ToastModule,
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
         MessageNotifierService,
         MessageService
     ]
 })
 export class SharedModule { 
   
 }