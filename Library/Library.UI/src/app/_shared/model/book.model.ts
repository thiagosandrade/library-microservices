import { IAuthor } from './author.model';
import { ICategory } from './category.model';
import { IEntity } from './entity.model';

export interface IBook extends IEntity {
    title: string;
    isbn: string;
    pagecount: number;
    publishedDate: string;
    thumbnailUrl: string;
    shortDescription: string;
    longDescription: string;
    status: string;
    authors: IAuthor[]
    categories: ICategory[];
    authorsAsString: string;
  }
