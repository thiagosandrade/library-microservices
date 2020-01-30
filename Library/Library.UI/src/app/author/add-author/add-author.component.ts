import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';

@Component({
  selector: 'app-add-author',
  templateUrl: './add-author.component.html',
  styleUrls: ['./add-author.component.scss']
})
export class AddAuthorComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private apiService: ApiAuthorService) { }

  addForm: FormGroup;

  ngOnInit() {
    this.addForm = this.formBuilder.group({
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
  }

  onSubmit() {
    this.apiService.createAuthor(this.addForm.value)
      .subscribe( data => {
        this.router.navigate(['list-author']);
      });
  }
}
