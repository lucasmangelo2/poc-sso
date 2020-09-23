import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Login, LoginParams } from './login.model';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: Login = new Login();
  params: LoginParams = new LoginParams();

  constructor(
    private route: ActivatedRoute,
    private service: LoginService) { }

  ngOnInit(): void {
    const queryParamMap = this.route.snapshot.queryParamMap;

    console.log()
    queryParamMap.keys.forEach(key => {
      this.params[key] = queryParamMap.get(key);
    });

    this.login.ReturnUrl = (this.route.snapshot as any)._routerState.url;
  }

  authenticate(){
    this.service
      .authenticate(this.login)
      .subscribe((result) => {
      });
  }

}
