import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";

@Component({
    selector: 'generic-popup',
    templateUrl: './generic-popup.component.html',
    styleUrls: ['./generic-popup.component.scss']
  })
  export class GenericPopupComponent implements OnInit, OnChanges {
    @Input() showInput : boolean;
    show : boolean = false;

    ngOnInit(): void {

    }

    ngOnChanges(changes: SimpleChanges): void {
      if(this.showInput != null && this.showInput != undefined)
        this.show = this.showInput
    }

    toggle(): void {
        this.show = !this.show;
    }
  }