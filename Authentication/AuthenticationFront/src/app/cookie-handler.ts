import { Injectable } from '@angular/core';
import { Cookie } from './cookie';

@Injectable({
    providedIn: 'root'
})
export class CookieHandler {

    SetCookie(name: string, value: string) {
        let values: Array<Cookie> = [];
        if (document.cookie == "")
            document.cookie = JSON.stringify(values);

        values = JSON.parse(document.cookie);

        let cookie: Cookie;

        for(let x in values){
            if(values[x].name == name){
                values[x].name = name;
                values[x].value = value;

                document.cookie = JSON.stringify(values);
                return;
            }
        }

        if(!cookie)
            cookie = {name: name, value: value};

        values.push(cookie);

        document.cookie = JSON.stringify(values);
    }

}
