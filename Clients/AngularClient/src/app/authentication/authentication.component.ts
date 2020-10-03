import { Component, OnInit } from '@angular/core';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from '../auth.config';
import { AuthenticationService } from './authentication.service';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent implements OnInit {

  constructor(private service: AuthenticationService) { }

  ngOnInit(): void {
    this.service.initAuthenticationFlow();
  }

  login() {
    this.service.login();
  }

  logout(){
    this.service.logout();
  }

  public get userName() {
    return this.service.userName;
  }

  public get isAuthenticated(): boolean {
    return this.service.isAuthenticated;
  }

}
