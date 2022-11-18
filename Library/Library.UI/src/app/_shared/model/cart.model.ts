import { IEntity } from './entity.model';

export interface ICart extends IEntity {
    id: number,
    userId: number,
    createdDate: Date,
    items: IProductCart[]
  }


  export interface IProductCart extends IEntity {
    cartId: number,
    productId: number,
    productName?: string,
    quantity: number
  }