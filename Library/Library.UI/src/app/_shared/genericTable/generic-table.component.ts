import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";

@Component({
    selector: 'generic-table',
    templateUrl: './generic-table.component.html',
    styleUrls: ['./generic-table.component.scss']
  })
  export class GenericTableComponent implements OnInit {

    @Input() items: Array<any>;
    @Input() tableColumns: any[];

    @Output() rowEditAction: EventEmitter<any> = new EventEmitter();
    @Output() rowDeleteAction: EventEmitter<any> = new EventEmitter();
    @Output() rowAddAction: EventEmitter<any> = new EventEmitter();

    pageOfItems: Array<any> = [];
    
    ngOnInit(): void {
        this.tableColumns.push({name: 'Edit', prop: ''});
        this.tableColumns.push({name: 'Delete', prop: ''});
    }

    mySortingFunction = (a, b) => {
        let columns = this.tableColumns.map(item => item.name.toLowerCase())
        let aKey = columns.indexOf(a.key)
        let bKey = columns.indexOf(b.key)
        
        return aKey > bKey ? 1 : -1;
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

    onRowAddAction(item: any){
        this.rowAddAction.emit(item)
    }

    onRowEditAction(item: any){
        this.rowEditAction.emit(item)
    }

    onRowDeleteAction(item: any){
        this.rowDeleteAction.emit(item)
    }
}