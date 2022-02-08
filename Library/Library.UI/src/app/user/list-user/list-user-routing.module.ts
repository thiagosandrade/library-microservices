import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ListUserComponent} from "./list-user.component";


const routes: Routes = [
  { path: '', component: ListUserComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ListUserRoutingModule { }
