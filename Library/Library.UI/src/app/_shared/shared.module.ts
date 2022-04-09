import { MessageNotifierService } from './messageNotifier/messageNotifier.service';
import { SignalRService } from './signalR/signalR.service';
import { MessageNotifierComponent } from './messageNotifier/messageNotifier.component';
import { ToastModule } from 'primeng/toast';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { GenericTableComponent } from './genericTable/generic-table.component';
import { JwPaginationComponent } from './genericPagination/generic.pagination.component';

@NgModule({
    imports: [
         CommonModule,
         ToastModule,
         NgMultiSelectDropDownModule.forRoot()
    ],
    declarations: [
         JwPaginationComponent,
         GenericTableComponent,
         MessageNotifierComponent,
    ],
    exports: [
         GenericTableComponent,
         MessageNotifierComponent,
         ReactiveFormsModule,
         CommonModule,
         FormsModule,
         NgMultiSelectDropDownModule
    ],
    providers: [
         SignalRService,
         MessageNotifierService,
         MessageService
     ]
 })
 export class SharedModule { 
   
 }