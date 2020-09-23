export class Login {
    Username: string;
    Password: string;
    ReturnUrl: string;
}


export class LoginParams {
    client_id: string;
    code_challenge: string;
    code_challenge_method: string;
    nonce: string;
    redirect_uri: string;
    response_type: string;
    scope: string;
    state: string;
}