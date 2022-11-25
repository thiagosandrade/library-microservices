import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { ShowUserComponent } from './show-user.component';
import { SharedModule } from 'src/app/_shared/shared.module';

@NgModule({
  declarations: [
    ShowUserComponent
  ],
  imports: [
    SharedModule,
    HttpClientModule
  ],
  exports:[
    ShowUserComponent
  ],
  providers: [], 
  bootstrap: []
})
export class ShowUserModule { }
