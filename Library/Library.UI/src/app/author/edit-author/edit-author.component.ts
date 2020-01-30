import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {first} from "rxjs/operators";
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';
import { Author } from 'src/app/_shared/model/author.model';
import { ApiAuthorResponse } from 'src/app/_shared/model/api.author.response';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-edit-author',
  templateUrl: './edit-author.component.html',
  styleUrls: ['./edit-author.component.scss']
})
export class EditAuthorComponent implements OnInit {

  author: Author;
  editForm: FormGroup;
  constructor(private formBuilder: FormBuilder,private router: Router, private apiService: ApiAuthorService, private datePipe : DatePipe) { }

  ngOnInit() {
    let userId = window.localStorage.getItem("editAuthorId");
    if(!userId) {
      alert("Invalid action.")
      this.router.navigate(['list-user']);
      return;
    }
    this.editForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      birth: ['', Validators.required],
      age: ['', Validators.required],
      placeOfBirthId: ['', Validators.required],
      placeOfBirth: this.formBuilder.group({
        id: ['', Validators.required],
        city: ['', Validators.required],
        state: ['', Validators.required],
        country: ['', Validators.required]
      })
    });
    this.apiService.getAuthorById(userId)
      .subscribe( (data : ApiAuthorResponse) => {
        let test = this.datePipe.transform(data.birth, 'yyyy/MM/dd')
        data.birth = test;
        this.editForm.setValue(data);
      });
  }

  onSubmit() {
    this.apiService.updateAuthor(this.editForm.value)
      .pipe(first())
      .subscribe(
        data => {
          if(data != null) {
            alert('User updated successfully.');
            this.router.navigate(['list-author']);
          }else {
            alert(data.message);
          }
        },
        error => {
          alert(error);
        });
  }

}
