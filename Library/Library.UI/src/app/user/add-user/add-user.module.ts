import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AddUserRoutingModule } from './add-user-routing.module';
import { AddUserComponent } from './add-user.component';
import { SharedModule } from 'src/app/_shared/shared.module';

@NgModule({
  declarations: [
    AddUserComponent
  ],
  imports: [
    SharedModule,
    AddUserRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class AddUserModule { }
