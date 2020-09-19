import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from '../auth.config';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private oauthService: OAuthService) { }

  ngOnInit(): void {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.initImplicitFlow();
    this.oauthService.loadDiscoveryDocumentAndLogin();
  }

  logout(){
    this.oauthService.logOut();
  }

  public get userName() {

    var claims : any= this.oauthService.getIdentityClaims();
    if (!claims) return null;

    return claims.given_name;
  }

}
