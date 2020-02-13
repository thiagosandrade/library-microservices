import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';
import { Author } from 'src/app/_shared/model/author.model';
import { ApiAuthorResponse } from 'src/app/_shared/model/api.author.response';
import { DatePipe } from '@angular/common';
import { ApiResponse } from 'src/app/_shared/model/api.response';

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
    this.apiService.getAuthorById(userId).subscribe
      (async (result : ApiAuthorResponse) => {
        let dateFormatted = this.datePipe.transform(result.value.birth, 'yyyy/MM/dd')
        result.value.birth = dateFormatted;
        this.editForm.setValue(result.value);
      });
  }

  onSubmit() {
    this.apiService.updateAuthor(this.editForm.value).subscribe
      ( async (result : ApiResponse) => {
          if(result.statusCode == 200) {
            alert('User updated successfully.');
            this.router.navigate(['list-author']);
          }else {
            alert(result.message);
          }
        },
        (error: any) => {
          alert(error);
        });
  }
}
