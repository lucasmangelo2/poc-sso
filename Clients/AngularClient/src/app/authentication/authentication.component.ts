import { Component, OnInit } from '@angular/core';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from '../auth.config';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent implements OnInit {

  constructor(private oauthService: OAuthService) { }

  ngOnInit(): void {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
    this.oauthService.setupAutomaticSilentRefresh();
  }

  login() {
    this.oauthService.initCodeFlow();
  }

  logout(){
    this.oauthService.logOut();
  }

  public get userName() {

    var claims : any= this.oauthService.getIdentityClaims();
    if (!claims) return null;

    return claims.name;
  }

  public get isAuthenticated(): boolean {
    return this.oauthService.sessionChecksEnabled;
  }

}
