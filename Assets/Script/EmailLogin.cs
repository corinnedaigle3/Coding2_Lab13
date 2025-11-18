using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class EmailLogin : ILogin
{
    private string email;
    private string password;
    public EmailLogin(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
    public void Login(Action<LoginResult> onSuccess, Action<PlayFabError> onError)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, onSuccess, onError);
    }
}
