import { Injectable } from "@angular/core";
import { Effect, ofType, Actions } from '@ngrx/effects';
import { GetAuthor, AuthorActionsEnum, GetAuthorSuccess, GetAuthors, GetAuthorsSuccess, UpdateAuthor, Success, Fail, DeleteAuthor, CreateAuthor } from '../actions/author.actions';
import { withLatestFrom, switchMap, map, mergeMap, catchError } from 'rxjs/operators';
import { select, Store } from '@ngrx/store';
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';
import { IAppState } from '../state/app.state';
import { ApiAuthorListResponse, ApiAuthorResponse } from 'src/app/_shared/model/api.author.response';
import { of } from 'rxjs';
import { selectAuthorList } from '../selectors/author.selector';
import { IAuthor } from 'src/app/_shared/model/author.model';

@Injectable()
export class AuthorEffects{
    
    constructor(
        private _authorService: ApiAuthorService,
        private _actions$: Actions,
        private _store: Store<IAppState>
      ) {}

    @Effect()
    getAuthor$ = this._actions$.pipe(
        ofType<GetAuthor>(AuthorActionsEnum.GetAuthor),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectAuthorList))),
        switchMap(
            ([id, authors]) => {
                const selectedAuthor = authors.filter(author => author.id === +id)[0];
                return of(new GetAuthorSuccess(selectedAuthor));
            })
    );

    @Effect()
    getAuthors$ = this._actions$.pipe(
        ofType<GetAuthors>(AuthorActionsEnum.GetAuthors),
        switchMap(() => this._authorService.getAuthors().pipe(
            switchMap((response : ApiAuthorListResponse) => of(new GetAuthorsSuccess(response.value)))
        ))
    );

    @Effect()
    createAuthor$ = this._actions$.pipe(
        ofType<CreateAuthor>(AuthorActionsEnum.CreateAuthor),
        map(action => action.payload),
        switchMap((author : IAuthor) => this._authorService.createAuthor(author).pipe(
            switchMap((response : ApiAuthorResponse) => of(new Success(response.value))),
            catchError(err => of(new Fail(err)))
        ))
    );

    @Effect()
    updateAuthor$ = this._actions$.pipe(
        ofType<UpdateAuthor>(AuthorActionsEnum.UpdateAuthor),
        map(action => action.payload),
        switchMap((author : IAuthor) => this._authorService.updateAuthor(author).pipe(
            switchMap((response : ApiAuthorResponse) => of(new Success(response.value))),
            catchError(err => of(new Fail(err)))
        ))
    );

    @Effect()
    deleteAuthor$ = this._actions$.pipe(
        ofType<DeleteAuthor>(AuthorActionsEnum.DeleteAuthor),
        map(action => action.payload),
        switchMap((author : IAuthor) => this._authorService.deleteAuthor(author.id).pipe(
            switchMap((response : ApiAuthorResponse) => of(new Success(response.value))),
            catchError(err => of(new Fail(err)))
        ))
    );

    
}