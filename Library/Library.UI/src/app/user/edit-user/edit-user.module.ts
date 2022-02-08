import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { EditUserRoutingModule } from './edit-user-routing.module';
import { EditUserComponent } from './edit-user.component';
import { SharedModule } from 'src/app/_shared/shared.module';

@NgModule({
  declarations: [
    EditUserComponent
  ],
  imports: [
    SharedModule,
    EditUserRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class EditUserModule { }
