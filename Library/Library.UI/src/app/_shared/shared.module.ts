import { MessageNotifierService } from './messageNotifier/messageNotifier.service';
import { SignalRService } from './signalR/signalR.service';
import { MessageNotifierComponent } from './messageNotifier/messageNotifier.component';
import { GrowlModule } from 'primeng/growl';
import { NgModule } from '@angular/core';

@NgModule({
    imports: [
         GrowlModule,
    ],
    declarations: [
         MessageNotifierComponent,
    ],
    exports: [
         MessageNotifierComponent,
    ],
    providers: [
         SignalRService,
         MessageNotifierService
     ]
 })
 export class SharedModule { 
   
 }