import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {AddAuthorComponent} from "./author/add-author/add-author.component";
import {ListAuthorComponent} from "./author/list-author/list-author.component";
import {EditAuthorComponent} from "./author/edit-author/edit-author.component";


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'add-author', component: AddAuthorComponent },
  { path: 'list-author', component: ListAuthorComponent },
  { path: 'edit-author', component: EditAuthorComponent },
  {path : '', component : LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
