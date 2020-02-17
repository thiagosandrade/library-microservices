import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Message } from 'primeng/api';

export enum Severities {
  SUCCESS = 'success', 
  INFO = 'info',
  WARN = 'warn',
  ERROR = 'error'
} 

@Injectable()
export class MessageNotifierService {

  messageNotificationChange: Subject<Object> = new Subject<Object>();
  static singletonInstance: MessageNotifierService;

  constructor() {
    if(!MessageNotifierService.singletonInstance){
      MessageNotifierService.singletonInstance = this;
    }

    return MessageNotifierService.singletonInstance;
 }


  messageNotify(severity: Severities, summary: string, detail: string) {
    this.messageNotificationChange.next({ severity, summary, detail });
  }

}