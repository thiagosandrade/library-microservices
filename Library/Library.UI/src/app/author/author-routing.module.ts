import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
  { 
    path: 'add-author', 
    canActivate: [AuthGuard],
    loadChildren: './add-author/add-author.module#AddAuthorModule'
  },
  { 
    path: 'list-author', 
    canActivate: [AuthGuard],
    loadChildren: './list-author/list-author.module#ListAuthorModule'
  },
  { 
    path: 'edit-author', 
    canActivate: [AuthGuard],
    loadChildren: './edit-author/edit-author.module#EditAuthorModule'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorRoutingModule { }
