import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // tslint:disable-next-line: new-parens
  @Output() cancelRegister = new EventEmitter;

  model: any = {};

  constructor(private authServuce: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register(){
    this.authServuce.register(this.model).subscribe(() => {
      this.alertify.success('registration successful');
    }, error => {
      this.alertify.error(error);//idk why when I use error it does not work here
    });
  }

  cancle(){
    this.cancelRegister.emit(false);
    console.log("Cancled");
  }

}
