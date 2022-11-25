import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { select, Store } from "@ngrx/store";
import { Observable } from "rxjs";
import { isUserLoggedAdmin } from "src/app/store/selectors/login.selector";
import { selectUserList } from "src/app/store/selectors/user.selector";
import { IAppState } from "src/app/store/state/app.state";
import { IUser } from "../model/user.model";

@Component({
    selector: 'generic-table',
    templateUrl: './generic-table.component.html',
    styleUrls: ['./generic-table.component.scss']
  })
  export class GenericTableComponent implements OnInit {

    @Input() items: Array<any>;
    @Input() tableColumns: any[];

    @Output() rowDetailsAction: EventEmitter<any> = new EventEmitter();
    @Output() rowEditAction: EventEmitter<any> = new EventEmitter();
    @Output() rowDeleteAction: EventEmitter<any> = new EventEmitter();
    @Output() rowCartAction: EventEmitter<any> = new EventEmitter();
    @Output() rowAddAction: EventEmitter<any> = new EventEmitter();

    hasDetails : boolean = false;
    hasEdit : boolean = false;
    hasDelete : boolean = false;
    hasAdd : boolean = false;
    hasCart : boolean = false;

    pageOfItems: Array<any> = [];

    users$ : Observable<IUser[]> = this.store.pipe(select(selectUserList));
    isAdmin$ : Observable<boolean> = this.store.pipe(select(isUserLoggedAdmin));
    
    constructor(private store: Store<IAppState<IUser>>) { }

    ngOnInit(): void {
        
        this.tableColumns.push({name: '', prop: ''});

        this.hasDetails = this.rowDetailsAction.observed;
        this.hasEdit = this.rowEditAction.observed;
        this.hasDelete = this.rowDeleteAction.observed;
        this.hasCart = this.rowCartAction.observed;
        this.hasAdd = this.rowAddAction.observed;
    }

    onChangePage(pageOfItems: Array<any>) {
        this.pageOfItems = pageOfItems.map(item => {
            let result = {};
            this.tableColumns.forEach(column => {
                if(column.name == 'Add' ||column.name == 'Edit' || column.name == 'Delete')
                    return;

                result[column.prop] = item[column.prop];
            })
            return result;
        })
    }
    onRowDetailsAction(item: any){
        this.rowDetailsAction.emit(item)
    }
    onRowAddAction(){
        this.rowAddAction.emit()
    }

    onRowEditAction(item: any){
        this.rowEditAction.emit(item)
    }

    onRowDeleteAction(item: any){
        this.rowDeleteAction.emit(item)
    }

    onRowCartAction(item: any){
        this.rowCartAction.emit(item)
    }
}