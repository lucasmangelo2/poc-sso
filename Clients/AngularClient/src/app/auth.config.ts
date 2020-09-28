import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from 'src/environments/environment';
 
export const authCodeFlowConfig: AuthConfig = {
    issuer: environment.url_identity_provider,
    redirectUri: window.location.origin,
    clientId: 'client_application',
    dummyClientSecret: 'super-secret',
    responseType: 'code',
    scope: 'openid profile email address',
    showDebugInformation: true,
    requireHttps: false,
    sessionChecksEnabled: true,
};