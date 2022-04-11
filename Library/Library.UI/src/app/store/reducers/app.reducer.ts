import { IAppState } from '../state/app.state';
import { ActionsEnum, EntitiesEnum } from '../actions/app.actions';
import { IEntity } from 'src/app/_shared/model/entity.model';

export const appReducer = <T extends IAppState<IEntity>>(entityType: EntitiesEnum, initialState: IAppState<IEntity>, defaultAction: any) =>
    (state = initialState, action = defaultAction): T => {
    
    switch(action.type){
        case `${entityType}_${ActionsEnum.GetAllSuccess}`: {
            return <T> {
                ...state,
                entities: action.payload,
            };
        }
        case `${entityType}_${ActionsEnum.GetSuccess}`: {
            return <T> {
                ...state,
                selectedEntity: action.payload
            };
        }
        case `${entityType}_${ActionsEnum.SetSelected}`: {
            let entitySelected = state.entities.find(item => action.payload.id === item.id);
            if(entitySelected == null)
                entitySelected = action.payload

            return <T> {
                ...state,
                selectedEntity: entitySelected
            }
        }
        case `${entityType}_${ActionsEnum.Create}`: {
            state.entities.push(action.payload);
            return <T> {
                ...state,
                entities: state.entities
            }
        }
        case `${entityType}_${ActionsEnum.Update}`: {
            const entitiesUpdated = state.entities.map(item => action.payload.id === item.id ? action.payload : item)
            return <T> {
                ...state,
                entities: entitiesUpdated
            }
        }
        case `${entityType}_${ActionsEnum.Delete}`: {
            const entitiesUpdated = state.entities.filter(item => action.payload.id !== item.id)
            return <T> {
                ...state,
                entities: entitiesUpdated
            }
        }

        default:
            return <T> state;
    }
};