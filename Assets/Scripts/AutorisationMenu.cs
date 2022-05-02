using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutorisationMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputFieldUserName;
    [SerializeField] private TMP_InputField _inputFieldUserMail;
    [SerializeField] private TMP_InputField _inputFieldPassword;
    [SerializeField] private Button _registrationButton;

    private string _userName;
    private string _userMail;
    private string _userPassword;
    void Start()
    {
        _inputFieldUserName.onEndEdit.AddListener(SetUserName);
        _inputFieldUserMail.onEndEdit.AddListener(SetUserMail);
        _inputFieldPassword.onEndEdit.AddListener(SetPassword);
        _registrationButton.onClick.AddListener(RegistrationAccount);
    }
    private void SetUserName(string userName)=> _userName = userName;
    private void SetUserMail(string userMail) =>_userMail = userMail;
    private void SetPassword(string password) => _userPassword = password;

    private void RegistrationAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _userName,
            Password = _userPassword,
            Email = _userMail,
            RequireBothUsernameAndEmail = true

        }, result =>
        {
            Debug.Log($"Succes: {result.Username}");
        }, error =>
        {
            Debug.LogError($"Fail: {error.ErrorMessage}");
        });
    }
}
