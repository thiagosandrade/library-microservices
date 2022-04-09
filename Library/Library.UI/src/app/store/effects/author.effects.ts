import { Injectable } from "@angular/core";
import { Effect, ofType, Actions } from '@ngrx/effects';
import { withLatestFrom, switchMap, map, catchError, mergeMap } from 'rxjs/operators';
import { select, Store } from '@ngrx/store';
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';
import { IAppState } from '../state/app.state';
import { ApiAuthorListResponse, ApiAuthorResponse } from 'src/app/_shared/model/api.author.response';
import { of } from 'rxjs';
import { selectAuthorList } from '../selectors/author.selector';
import { IAuthor } from 'src/app/_shared/model/author.model';
import { Get, ActionsEnum, GetSuccess, GetAll, GetAllSuccess, Create, Success, Fail, Delete, Update, EntitiesEnum } from '../actions/app.actions';

@Injectable()
export class AuthorEffects{
    
    constructor(
        private _authorService: ApiAuthorService,
        private _actions$: Actions,
        private _store: Store<IAppState<IAuthor>>) {}

    @Effect()
    getAuthor$ = this._actions$.pipe(
        ofType<Get>(`${EntitiesEnum.Author}_${ActionsEnum.Get}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectAuthorList))),
        switchMap(
            ([id, authors]) => {
                const selectedAuthor = authors.filter(author => author.id === +id)[0];
                return of(new GetSuccess(selectedAuthor, EntitiesEnum.Author));
            })
    );

    @Effect()
    getAuthors$ = this._actions$.pipe(
        ofType<GetAll>(`${EntitiesEnum.Author}_${ActionsEnum.GetAll}`),
        switchMap(() => this._authorService.getAuthors().pipe(
            map((response : ApiAuthorListResponse) => new GetAllSuccess(response.value, EntitiesEnum.Author))
        ))
    );

    @Effect()
    createAuthor$ = this._actions$.pipe(
        ofType<Create>(`${EntitiesEnum.Author}_${ActionsEnum.Create}`),
        map(action => action.payload),
        switchMap((author : IAuthor) => this._authorService.createAuthor(author).pipe(
            map((response : ApiAuthorResponse) => 
            [
                new Success(response.value, EntitiesEnum.Author),
            ]),
            catchError(err => of(new Fail(err, EntitiesEnum.Author)))
        ))
    );

    @Effect()
    updateAuthor$ = this._actions$.pipe(
        ofType<Update>(`${EntitiesEnum.Author}_${ActionsEnum.Update}`),
        map(action => action.payload),
        switchMap(
            (author : IAuthor) => 
            this._authorService.updateAuthor(author)
                .pipe(
                    switchMap((response : ApiAuthorResponse) => 
                    [
                        new Success(response.value, EntitiesEnum.Author),
                    ]),
                    catchError(err => of(new Fail(err, EntitiesEnum.Author)))
                )
        )
    );

    @Effect()
    deleteAuthor$ = this._actions$.pipe(
        ofType<Delete>(`${EntitiesEnum.Author}_${ActionsEnum.Delete}`),
        map(action => action.payload),
        switchMap((author : IAuthor) => this._authorService.deleteAuthor(author.id).pipe(
            mergeMap((response : ApiAuthorResponse) => 
            [
                new Success(response.value, EntitiesEnum.Author),
            ]),
            catchError(err => of(new Fail(err, EntitiesEnum.Author)))
        ))
    );

    
}