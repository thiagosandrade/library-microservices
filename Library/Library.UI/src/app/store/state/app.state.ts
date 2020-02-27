import { IEntity } from 'src/app/_shared/model/entity.model';

export interface IAppState<T extends IEntity> {
    entities: T[];
    selectedEntity: T;
}

export const initialAppState: IAppState<IEntity> = {
    entities: [],
    selectedEntity: null
}
