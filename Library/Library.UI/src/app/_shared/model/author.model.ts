export interface IAuthor {
    id: number;
    name: string;
    surname: string;
    birth: string;
    age: number;
    placeOfBirth : IPlaceOfBirth;
  }

  export interface IPlaceOfBirth{
    id: number;
    city: string;
    state: string;
    country: string;
  }