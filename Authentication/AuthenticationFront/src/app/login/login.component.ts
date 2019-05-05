import { Component, OnInit } from '@angular/core';
import { LoginModel } from './login-model';
import { AuthenticationService } from '../authentication-service'
import { AuthenticationToken } from '../authentication-token';
import { CookieHandler } from '../cookie-handler';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  mensagem: string;
  model = new LoginModel("", "");
  constructor(private authenticationService: AuthenticationService, private cookieHandler: CookieHandler) { }

  ngOnInit() {
  }

  SendData(dados: LoginModel) {
    this.authenticationService.GenerateToken(dados).subscribe((token: AuthenticationToken) => {
      this.cookieHandler.SetCookie("JWT", token.token);
      this.mensagem = "Success";
    }, (error: HttpErrorResponse) => {
      if(error.status == 404){
        this.mensagem = "User Not Found!";
      }
      if(error.status == 401){
        this.mensagem = "Login failed!";
      }
    });
  }

}
