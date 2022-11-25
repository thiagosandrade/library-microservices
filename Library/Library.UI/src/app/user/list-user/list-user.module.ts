import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { ListUserRoutingModule } from './list-user-routing.module';
import { ListUserComponent } from './list-user.component';
import { SharedModule } from 'src/app/_shared/shared.module';
import { ShowUserModule } from '../show-user/show-user.module';

@NgModule({
  declarations: [
    ListUserComponent
  ],
  imports: [
    SharedModule,
    ShowUserModule,
    ListUserRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class ListUserModule { }
