import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {EditAuthorComponent} from "./edit-author.component";


const routes: Routes = [
  { path: '', component: EditAuthorComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EditAuthorRoutingModule { }
