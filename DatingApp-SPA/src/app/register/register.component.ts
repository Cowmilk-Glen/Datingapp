import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter;

  model: any = {};

  constructor(private authServuce: AuthService) { }

  ngOnInit() {
  }

  register(){
    this.authServuce.register(this.model).subscribe(() => {
      console.log('registration successful');
    }, error => {
      console.log(error);
    });
  }

  cancle(){
    this.cancelRegister.emit(false);
    console.log("Cancled");
  }

}
