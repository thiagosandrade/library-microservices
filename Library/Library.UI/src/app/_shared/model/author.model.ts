export class Authors {
  authors: Author[]
}

export class Author {
    id: string;
    name: string;
    surname: string;
    birth: string;
    age: number;
    placeOfBirth : PlaceOfBirth;
  }

  export class PlaceOfBirth{
    id: string;
    city: string;
    state: string;
    country: string;
  }