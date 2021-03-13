//////////////////////////// SignalR Service
import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";  // or from "@microsoft/signalr" if you are using a new library

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    public data: ChartModel[];
    private hubConnection: signalR.HubConnection
    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:53355/signalServer')
            .build();
        this.hubConnection
            .start()
            .then(() =>
                this.hubConnection.send("MyNotificaton"))
            .catch(err => console.log('Error while starting connection: ' + err))

    }
    public addTransferChartDataListener = () => {
        this.hubConnection.on('transferchartdata', (data) => {
            this.data = data;
            console.log(data);
        });
    }
}

export interface ChartModel {
    data: [],
    label: string
}
////////////////////////////App Compinent
import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signal-r.service';
import { HttpClient } from '@angular/common/http';
@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(public signalRService: SignalRService, private http: HttpClient) { }
    ngOnInit() {
        this.signalRService.startConnection();
        this.signalRService.addTransferChartDataListener();
        this.startHttpRequest();
    }
    private startHttpRequest = () => {
        this.http.get('http://localhost:53355/api/Values/GetEmployees')
            .subscribe(res => {
                console.log(res);
            })
    }
}