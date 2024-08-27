import {Client} from './client.model'

export interface GridClient {
  clients: Array<Client>
  total: number
}
