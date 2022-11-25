import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { IUser } from 'src/app/_shared/model/user.model';



@Component({
  selector: 'show-user',
  templateUrl: './show-user.component.html',
  styleUrls: ['./show-user.component.scss']
})
export class ShowUserComponent implements OnInit, OnChanges {

  @Input() userInput: IUser;
  user: IUser;

  constructor() { 
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    if (this.userInput != undefined && this.userInput != null)
      this.user = this.userInput;
  }

  
  ngOnInit() {
   
  }


}
