import { Component, OnInit } from '@angular/core';
import { ClientService } from '../services/client.service';
import { Client } from '../models/client.model';
import { DataStateChangeEvent, GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { State } from "@progress/kendo-data-query";
import { Observable, take } from 'rxjs';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent implements OnInit {
  public clients: GridDataResult = { data: [], total: 0 };
  public pageSize = 10;
  public skip = 0;
  public filteredData: any[] = []; // Filtered data
  public searchTerm: string = '';
  public data: any[] = [];
  public selectedFilter: string = '';

  public state: State = {
    skip: 0,
    take: 10,
    filter: {
      logic: 'and',
      filters: []
    }
  };

  constructor(private clientService: ClientService) {  }

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.clientService.GetClientsPaginAsync(this.state).subscribe(data => {
      this.clients = {
        data: data.clients,
        total: data.total
      };
      console.log(this.clients)
    });
  }

  dataStateChange(event: DataStateChangeEvent): void {
    this.state = event;
    this.loadClients();
  }

  addClient(client: Client): void {
    this.clientService.addClient(client).subscribe(() => this.loadClients());
  }

  updateClient(client: Client): void {
    this.clientService.updateClient(client).subscribe(() => this.loadClients());
  }

  deleteClient(id: number): void {
    this.clientService.deleteClient(id).subscribe(() => this.loadClients());
  }
}
