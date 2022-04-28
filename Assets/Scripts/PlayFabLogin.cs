using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Text _loginInformationText;
    public void Start()
    {
        _loginButton.onClick.AddListener(Login);
    }
    private void Login()
    {
        // Here we need to check whether TitleId property is configured in settings ornot
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            * If not we need to assign it to the appropriate variable manually
            * Otherwise we can just remove this if statement at all
            */
            PlayFabSettings.staticSettings.TitleId = " 25F68";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "GeekBrainsLesson3",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        string message = "Congratulations, you made successful API call!";
        Debug.Log(message);
        _loginInformationText.text = message;
        _loginInformationText.color = Color.green;
    }
    private void OnLoginFailure(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
        _loginInformationText.text = $"Something went wrong: {errorMessage}";
        _loginInformationText.color = Color.red;
    }
    private void OnDisable()
    {
        _loginButton.onClick.RemoveListener(Login); 
    }
}
