import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
  { 
    path: 'add-author', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./add-author/add-author.module').then(m => m.AddAuthorModule)
  },
  { 
    path: 'list-author', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./list-author/list-author.module').then(m => m.ListAuthorModule)
  },
  { 
    path: 'edit-author', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./edit-author/edit-author.module').then(m => m.EditAuthorModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorRoutingModule { }
