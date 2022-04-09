import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";

@Component({
    selector: 'generic-table',
    templateUrl: './generic-table.component.html',
    styleUrls: ['./generic-table.component.scss']
  })
  export class GenericTableComponent implements OnInit {

    @Input() items: Array<any>;
    @Input() tableColumns: string[];

    @Output() rowEditAction: EventEmitter<any> = new EventEmitter();
    @Output() rowDeleteAction: EventEmitter<any> = new EventEmitter();

    pageOfItems: Array<any>;
    
    ngOnInit(): void {
        this.tableColumns.push('Edit');
        this.tableColumns.push('Delete');
    }

    mySortingFunction = (a, b) => {
        let columns = this.tableColumns.map(item => item.toLowerCase())
        let aKey = columns.indexOf(a.key)
        let bKey = columns.indexOf(b.key)
        
        return aKey > bKey ? 1 : -1;
    }

    onChangePage(pageOfItems: Array<any>) {
        this.pageOfItems = pageOfItems
    }

    onRowEditAction(item: any){
        this.rowEditAction.emit(item)
    }

    onRowDeleteAction(item: any){
        this.rowDeleteAction.emit(item)
    }
}