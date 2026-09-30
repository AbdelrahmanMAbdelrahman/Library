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

          console.log(
            '[SignalR] accessToken exists:',
            !!token
          );

          return token ?? '';
        }
      })
      .withAutomaticReconnect()
      .build();

    console.log(
      '[SignalR] Initial state:',
      this.hubConnection.state
    );

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

    console.log(
      '[SignalR] Start called. State:',
      this.hubConnection.state
    );

    if (this.hubConnection.state !== HubConnectionState.Disconnected) {
      console.log(
        '[SignalR] Already connected/connecting.'
      );

      return;
    }

    try {

      await this.hubConnection.start();

      console.log(
        '[SignalR] Connected successfully!'
      );

      console.log(
        '[SignalR] Connection ID:',
        this.hubConnection.connectionId
      );

      console.log(
        '[SignalR] State:',
        this.hubConnection.state
      );

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

      console.log('[SignalR] CopyReturned EVENT RECEIVED');
      console.log('[SignalR] Data:', data);
      console.log('[SignalR] CopyId:', data.copyId);

      callback(data.copyId);
    }
  );
}
}