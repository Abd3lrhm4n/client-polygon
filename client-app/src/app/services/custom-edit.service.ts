import { Injectable } from '@angular/core';
import { ClientService } from './client.service';
import { BehaviorSubject, Observable, zip } from 'rxjs';
import { Client } from '../models/client.model';

@Injectable()
export class CustomEditService extends BehaviorSubject<Client[]>  {
  constructor(private clientService: ClientService) {
    super([])
  }

  public create(item: Client): Observable<any> {
    return this.clientService.addClient(item);
  }

  public update(item: Client): Observable<any> {
    return this.clientService.updateClient(item);
  }

  public remove(id: number): Observable<any> {
    return this.clientService.deleteClient(id);
  }
  public assignValues(target: unknown, source: unknown): void {
  //   Object.assign(target, source);
  }
}
