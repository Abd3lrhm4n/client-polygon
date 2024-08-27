import { DataItem } from './../../../node_modules/@progress/kendo-angular-grid/data/data-item.interface.d';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ClientService } from '../services/client.service';
import { Client } from '../models/client.model';
import { DataStateChangeEvent, GridComponent, GridDataResult, RemoveEvent, SaveEvent } from '@progress/kendo-angular-grid';
import { State } from "@progress/kendo-data-query";
import { CreateFormGroupArgs } from "@progress/kendo-angular-grid";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { CustomEditService } from '../services/custom-edit.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css'],
  providers: [CustomEditService]
})
export class ClientComponent implements OnInit {
  public clients: GridDataResult = { data: [], total: 0 };
  public pageSize = 10;
  public skip = 0;
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

  public formGroup: FormGroup = this.formBuilder.group({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    id: 0
  });

  @ViewChild(GridComponent) grid!: GridComponent;

  constructor(private clientService: ClientService, private formBuilder: FormBuilder, public editService: CustomEditService) {
    this.createFormGroup = this.createFormGroup.bind(this);
   }

  ngOnInit(): void {
    this.loadClients();
  }

  public createFormGroup(args: CreateFormGroupArgs): FormGroup {
    const item: Client = !args.isNew ? args.dataItem : {};

    this.formGroup = this.formBuilder.group({
      id: [item.id],
      firstName: [item.firstName, Validators.required],
      lastName: [item.lastName, Validators.required],
      email: [item.email, [Validators.required, Validators.email]],
      phoneNumber: [item.phoneNumber, Validators.required]
    });

    return this.formGroup;
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

  public saveHandler({ formGroup }: SaveEvent): void {
    const client: Client = formGroup.value;
    console.log('save', client);

    if (client.id) {
      this.updateClient(client);
    } else {
      this.grid.closeRow()
      this.addClient(client);
    }
  }

  public removeHandler({ dataItem }: RemoveEvent): void {
    this.deleteClient(dataItem.id);
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
