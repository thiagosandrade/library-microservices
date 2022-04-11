import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';

import paginate from 'jw-paginate';

@Component({
    moduleId: module.id,
    selector: 'jw-pagination',
    template:
        `
        <div style="display: flex;">
            <div>
                <label for="pageSizeOption" style="margin-bottom: 1rem; margin-top: 0.5rem; margin-right: 5px;">Page Size:</label>
                <select #pageSizeOption name="pageSizeOption" id="pageSizeOption" (change)='onPageSizeChanged(pageSizeOption.value)'
                    style="margin-bottom: 1rem; margin-top: 0.5rem; border-radius: 0.25rem; border-color: lightgrey;"
                >
                    <option value="5" selected>5</option>
                    <option value="10">10</option>
                    <option value="15">15</option>
                    <option value="20">20</option>
                    <option value="40">40</option>
                </select>
            </div>
            <ul *ngIf="pager.pages && pager.pages.length" class="pagination" style="margin-left: auto;">
                <li [ngClass]="{disabled:pager.currentPage === 1}" class="page-item first-item">
                    <a (click)="setPage(1)" class="page-link">First</a>
                </li>
                <li [ngClass]="{disabled:pager.currentPage === 1}" class="page-item previous-item">
                    <a (click)="setPage(pager.currentPage - 1)" class="page-link">Previous</a>
                </li>
                <li *ngFor="let page of pager.pages" [ngClass]="{active:pager.currentPage === page}" class="page-item number-item">
                    <a (click)="setPage(page)" class="page-link">{{page}}</a>
                </li>
                <li [ngClass]="{disabled:pager.currentPage === pager.totalPages}" class="page-item next-item">
                    <a (click)="setPage(pager.currentPage + 1)" class="page-link">Next</a>
                </li>
                <li [ngClass]="{disabled:pager.currentPage === pager.totalPages}" class="page-item last-item">
                    <a (click)="setPage(pager.totalPages)" class="page-link">Last</a>
                </li>
            </ul>
        </div>
    `
})

export class JwPaginationComponent implements OnInit, OnChanges {
    @Input() items: Array<any>;
    @Output() changePage = new EventEmitter<any>(true);

    pager: any = {};
    pageSize: number = 5;
    initialPage = 1;
    maxPages = 5;

    ngOnInit() {
        // set page if items array isn't empty
        if (this.items && this.items.length) {
            this.setPage(this.initialPage);
        }
    }

    ngOnChanges(changes: SimpleChanges) {
        // reset page if items array has changed
        if (changes.items.currentValue !== changes.items.previousValue) {
            this.setPage(this.initialPage);
        }
    }

    setPage(page: number) {
        // get new pager object for specified page
        this.pager = paginate(this.items.length, page, this.pageSize, this.maxPages);

        // get new page of items from items array
        var pageOfItems = this.items.slice(this.pager.startIndex, this.pager.endIndex + 1);

        // call change page function in parent component
        this.changePage.emit(pageOfItems);
    }

    onPageSizeChanged(value: string) {
        this.pageSize = +value;
        this.setPage(this.pager.currentPage)
    }
}