import { IEntity } from './entity.model';

export interface IAuthor extends IEntity {
    name: string;
    surname: string;
    birth: string;
    age: number;
  }
