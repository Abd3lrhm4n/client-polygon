import { Component, OnInit } from '@angular/core';
import { ClientService } from '../services/client.service';
import { Client } from '../models/client.model';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';

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

  constructor(private clientService: ClientService) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.clientService.getClients().subscribe(data => {
      this.clients = {
        data: data.slice(this.skip, this.skip + this.pageSize),
        total: data.length
      };
    });
  }

    search() {
    this.applyFilter();
  }

  applyFilter() {
    if (!this.searchTerm) {
      this.filteredData = this.data;
    } else {
      this.filteredData = this.data.filter(client => {
        const value = client[this.selectedFilter]?.toLowerCase() || '';
        return value.includes(this.searchTerm.toLowerCase());
      });
    }
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
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
