import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { LoginModel } from './login/login-model';
import { Observable } from 'rxjs';
import { AuthenticationToken } from './authentication-token';


@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    constructor(private http: HttpClient){}

    GenerateToken(model: LoginModel): Observable<AuthenticationToken>{
        var resultado = this.http.post<AuthenticationToken>('http://localhost:9000/api/Authentication/CreateToken',{
            UserName: model.username,
            Senha: model.password
        });        
        return resultado;
    }

}
