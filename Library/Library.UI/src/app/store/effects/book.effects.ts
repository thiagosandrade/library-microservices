import { Injectable } from "@angular/core";
import { Effect, ofType, Actions } from '@ngrx/effects';
import { withLatestFrom, switchMap, map, catchError, mergeMap } from 'rxjs/operators';
import { select, Store } from '@ngrx/store';
import { ApiBookService } from 'src/app/_shared/service/api.book.service';
import { IAppState } from '../state/app.state';
import { ApiBookListResponse, ApiBookResponse } from 'src/app/_shared/model/api.book.response';
import { of } from 'rxjs';
import { selectBookList } from '../selectors/book.selector';
import { IBook } from 'src/app/_shared/model/book.model';
import { Get, ActionsEnum, GetSuccess, GetAll, GetAllSuccess, Create, Success, Fail, Delete, Update, EntitiesEnum } from '../actions/app.actions';

@Injectable()
export class BookEffects{
    
    constructor(
        private _bookService: ApiBookService,
        private _actions$: Actions,
        private _store: Store<IAppState<IBook>>) {}

    @Effect()
    getBook$ = this._actions$.pipe(
        ofType<Get>(`${EntitiesEnum.Book}_${ActionsEnum.Get}`),
        map(action => action.payload),
        withLatestFrom(this._store.select(selectBookList)),
        switchMap(
            ([id, books]) => {
                const selectedBook = books.filter(book => book.id === +id)[0];
                return of(new GetSuccess(selectedBook, EntitiesEnum.Book));
            })
    );

    @Effect()
    getBooks$ = this._actions$.pipe(
        ofType<GetAll>(`${EntitiesEnum.Book}_${ActionsEnum.GetAll}`),
        switchMap(() => this._bookService.getBooks().pipe(
            map((response : ApiBookListResponse) => new GetAllSuccess(response.value, EntitiesEnum.Book))
        ))
    );

    @Effect()
    createBook$ = this._actions$.pipe(
        ofType<Create>(`${EntitiesEnum.Book}_${ActionsEnum.Create}`),
        map(action => action.payload),
        switchMap(
            (book : IBook) => 
            this._bookService.createBook(book)
                .pipe(
                    switchMap((response : ApiBookResponse) => 
                    [
                        new Success(response.value, EntitiesEnum.Book),
                    ]),
                    catchError(err => of(new Fail(err, EntitiesEnum.Book)))
                )
        )
    );

    @Effect()
    updateBook$ = this._actions$.pipe(
        ofType<Update>(`${EntitiesEnum.Book}_${ActionsEnum.Update}`),
        map(action => action.payload),
        switchMap(
            (book : IBook) => 
            this._bookService.updateBook(book)
                .pipe(
                    switchMap((response : ApiBookResponse) => 
                    [
                        new Success(response.value, EntitiesEnum.Book)
                    ]),
                    catchError(err => of(new Fail(err, EntitiesEnum.Book)))
                )
        )
    );

    

    @Effect()
    deleteBook$ = this._actions$.pipe(
        ofType<Delete>(`${EntitiesEnum.Book}_${ActionsEnum.Delete}`),
        map(action => action.payload),
        switchMap((book : IBook) => this._bookService.deleteBook(book.id).pipe(
            mergeMap((response : ApiBookResponse) => 
            [
                new Success(response.value, EntitiesEnum.Book),
            ]),
            catchError(err => of(new Fail(err, EntitiesEnum.Book)))
        ))
    );

    
}