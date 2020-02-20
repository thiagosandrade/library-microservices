import { Component } from '@angular/core';
import { Message, MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { MessageNotifierService } from './messageNotifier.service';

@Component({
    selector: 'message-notifier',
    templateUrl: './messageNotifier.component.html'
})
export class MessageNotifierComponent {

    messageSubscription: Subscription;

    constructor(private messageNotifier: MessageNotifierService, private messageService: MessageService) {
        this.subscribeToMessageNotifications();
    }

    subscribeToMessageNotifications() {
        this.messageSubscription = this.messageNotifier.messageNotificationChange
            .subscribe((notification: Message) => {
                this.sendMessage(notification.severity, notification.summary, notification.detail);
            });
    }

    sendMessage(severity: string, summary: string, detail: string) {
        
        this.messageService.add(
            {
                severity:severity, 
                summary:summary, 
                detail:detail,
                life: 2000,
                sticky: false
            });
    }

    ngOnDestroy() {
        this.messageSubscription.unsubscribe();
    }
}