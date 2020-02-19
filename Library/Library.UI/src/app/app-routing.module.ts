import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { 
    path: 'login',  
    loadChildren: './login/login.module#LoginModule'
  },
  { 
    path: 'author', 
    loadChildren: './author/author.module#AuthorModule'
  },
  {
    path : '', 
    loadChildren: './login/login.module#LoginModule'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
