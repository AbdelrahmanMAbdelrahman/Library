import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CopyReturnSignalRService } from '../Books/Services/CopyReturnSignalRService';
import { AuthStateService } from '../Auth/Services/AuthStateService';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink,CommonModule],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar implements OnInit{
  showNotifications = false;
toggleNotifications(): void {
  this.showNotifications = !this.showNotifications;
}

closeNotifications(): void {
  this.showNotifications = false;
}
closeNotification(notification: AppNotification): void {

  this.notifications =
    this.notifications.filter(n => n !== notification);

}
  notifications: AppNotification[] = [];
  /**
   *
   */
  constructor(private notificationService: CopyReturnSignalRService,public authState:AuthStateService) {
    
  }
async ngOnInit(): Promise<void> {

  this.notificationService.OnCopyResturn((copyId) => {

    console.log('[NavBar] Notification received:', copyId);

    this.notifications.push({
      message: 'A reserved copy is now available.',
      copyId,
      isRead: false
    });
    console.log("notifications is : ",this.notifications)
  });

  console.log('[NavBar] Starting SignalR...');

  await this.notificationService.Start();

  console.log('[NavBar] SignalR Start finished');
}
 
}
export interface AppNotification {
  message: string;
  copyId: string;
  isRead: boolean;
}
