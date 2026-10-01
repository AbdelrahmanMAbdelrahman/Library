import { Injectable } from "@angular/core";
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState
} from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class CopyReturnSignalRService {

  private hubConnection: HubConnection;

  constructor() {

    console.log('[SignalR] Creating connection...');

    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7010/Hubs/CopyNotifier', {
        accessTokenFactory: () => {
          const token = localStorage.getItem('accessToken');

          return token ?? '';
        }
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.onreconnecting(error => {
      console.log('[SignalR] Reconnecting...', error);
    });

    this.hubConnection.onreconnected(connectionId => {
      console.log(
        '[SignalR] Reconnected. ConnectionId:',
        connectionId
      );
    });

    this.hubConnection.onclose(error => {
      console.log('[SignalR] Connection closed.', error);
    });
  }

  async Start(): Promise<void> {

    if (this.hubConnection.state !== HubConnectionState.Disconnected) {
      console.log(
        '[SignalR] Already connected/connecting.'
      );

      return;
    }

    try {

      await this.hubConnection.start();

      
    } catch (error) {

      console.error(
        '[SignalR] Connection Error:',
        error
      );
    }
  }

OnCopyResturn(callback: (copyId: string) => void): void {

  console.log('[SignalR] Registering CopyReturned listener...');

  this.hubConnection.on(
    'CopyReturned',
    (data: { copyId: string }) => {

      callback(data.copyId);
    }
  );
}
}