import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ListAuthorComponent} from "./list-author.component";


const routes: Routes = [
  { path: '', component: ListAuthorComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ListAuthorRoutingModule { }
