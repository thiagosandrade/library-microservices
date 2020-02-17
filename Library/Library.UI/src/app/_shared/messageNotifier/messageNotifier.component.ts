import { Component } from '@angular/core';
import { Message } from 'primeng/api';
import { Subscription } from 'rxjs';
import { MessageNotifierService } from './messageNotifier.service';

@Component({
    selector: 'message-notifier',
    templateUrl: './messageNotifier.component.html'
})
export class MessageNotifierComponent {

    messageSubscription: Subscription;
    message: Message[] = [];

    constructor(private messageNotifier: MessageNotifierService) {
        this.subscribeToMessageNotifications();
    }

    subscribeToMessageNotifications() {
        this.messageSubscription = this.messageNotifier.messageNotificationChange
            .subscribe((notification: Message) => {
                this.sendMessage(notification.severity, notification.summary, notification.detail);
            });
    }

    sendMessage(severity: string, summary: string, detail: string) {
        this.message = [];
        this.message.push({
            sticky: false,
            life: 2000,
            severity: severity,
            summary: summary,
            detail: detail
        });
    }

    ngOnDestroy() {
        this.messageSubscription.unsubscribe();
    }
}