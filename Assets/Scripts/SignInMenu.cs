using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;

public class SignInMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userNameInputFieldTMP;
    [SerializeField] private TMP_InputField _userPasswordInputFieldTMP;
    [SerializeField] private Button _signInButton;
    [SerializeField] private Image _signInConnectImage;

    private string _userName;
    private string _userPassword;
    private bool _isConnect = false;
    private bool _isSignIn = false;

    void Start()
    {
        _userNameInputFieldTMP.onEndEdit.AddListener(SetUsername);
        _userPasswordInputFieldTMP.onEndEdit.AddListener(SetUserPassword);
        _signInButton.onClick.AddListener(SignIn);
    }

    private void SetUsername(string name) => _userName = name;
    private void SetUserPassword(string password) => _userPassword = password;

    private void SignIn()
    {
        _isSignIn = true;
        _signInConnectImage.color = Color.white;

        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username =_userName,
            Password = _userPassword,
        }, result =>
        {
            Debug.Log($"Succes: {_userName}");
            _isConnect = true;
            _isSignIn = false;
            _signInConnectImage.color = Color.green;
            _signInConnectImage.fillAmount = 1;
            _isConnect = false;
        }, error =>
        {
            Debug.LogError($"Fail: {error.ErrorMessage}");
            _isConnect = true;
            _isSignIn = false;
            _signInConnectImage.color = Color.red;
            _signInConnectImage.fillAmount = 1;
            _isConnect = false;
        });
       
    }
    private void Update()
    {
        if (_isSignIn == true)
        {
            if (_isConnect == false)
            {
                _signInConnectImage.fillAmount += 0.01f;
                if (_signInConnectImage.fillAmount ==1)
                {
                    _signInConnectImage.fillAmount = 0;
                }
            }
           
        }
    }


}
