import { Author, PlaceOfBirth } from './author.model';

export class ApiAuthorResponse implements Author {
  id: string;
  name: string;
  surname: string;
  birth: string;
  age: number;
  placeOfBirth: PlaceOfBirth;
  message? : string;
}

export class ApiAuthorListResponse {
  authors : Author[]
  message? : string;
}