import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { 
    path: 'login',  
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule)
  },
  { 
    path: 'user',  
    loadChildren: () => import('./user/user.module').then(m => m.UserModule)
  },
  { 
    path: 'author', 
    loadChildren: () => import('./author/author.module').then(m => m.AuthorModule)
  },
  { 
    path: 'book', 
    loadChildren: () => import('./book/book.module').then(m => m.BookModule)
  },
  {
    path : '', 
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
