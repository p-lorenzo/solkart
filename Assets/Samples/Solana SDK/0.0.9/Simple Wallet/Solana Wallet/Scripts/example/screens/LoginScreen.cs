using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Solana.Unity.Wallet;

// ReSharper disable once CheckNamespace

namespace Solana.Unity.SDK.Example
{
    public class LoginScreen : SimpleScreen
    {
        [SerializeField]
        private TMP_InputField passwordInputField;
        [SerializeField]
        private TextMeshProUGUI passwordText;
        [SerializeField]
        private Button loginBtn;
        [SerializeField]
        private Button loginBtnGoogle;
        [SerializeField]
        private Button loginBtnTwitter;
        [SerializeField]
        private Button loginBtnPhantom;
        [SerializeField]
        private Button loginBtnSms;
        [SerializeField]
        private Button loginBtnXNFT;
        [SerializeField]
        private TextMeshProUGUI messageTxt;
        [SerializeField]
        private TMP_Dropdown dropdownRpcCluster;

        private void OnEnable()
        {
            dropdownRpcCluster.interactable = true;
            passwordInputField.text = string.Empty;

            if (Web3.Base != null)
            {
                dropdownRpcCluster.interactable = false;
                manager.ShowScreen(this, "wallet_screen");
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            passwordText.text = "";

            passwordInputField.onSubmit.AddListener(delegate { LoginChecker(); });

            loginBtn.onClick.AddListener(LoginChecker);
            loginBtnGoogle.onClick.AddListener(delegate{LoginCheckerWeb3Auth(Provider.GOOGLE);});
            loginBtnTwitter.onClick.AddListener(delegate{LoginCheckerWeb3Auth(Provider.TWITTER);});
            loginBtnPhantom.onClick.AddListener(LoginCheckerPhantom);
            loginBtnSms.onClick.AddListener(LoginCheckerSms);
            loginBtnXNFT.onClick.AddListener(LoginCheckerXnft);
            
            loginBtnXNFT.gameObject.SetActive(false);
            
            if (Application.platform == RuntimePlatform.Android)
            {
                loginBtnPhantom.gameObject.SetActive(false);
                loginBtnSms.gameObject.SetActive(true);
            }

            if(messageTxt != null)
                messageTxt.gameObject.SetActive(false);
        }

        private async void LoginChecker()
        {
            var password = passwordInputField.text;
            var account = await Web3.Instance.LoginInGameWallet(password);
            CheckAccount(account);
        }
        
        private async void LoginCheckerPhantom()
        {
            var account = await Web3.Instance.LoginPhantom();
            CheckAccount(account);
        }
        
        private async void LoginCheckerSms()
        {
            var account = await Web3.Instance.LoginSolanaMobileStack();
            CheckAccount(account);
        }
        
        private async void LoginCheckerWeb3Auth(Provider provider)
        {
            var account = await Web3.Instance.LoginInWeb3Auth(provider);
            CheckAccount(account);
        }

        public void TryLoginBackPack()
        {
            LoginCheckerXnft();
        }

        private async void LoginCheckerXnft()
        {
            if(Web3.Instance == null) return;
            var account = await Web3.Instance.LoginXNFT();
            messageTxt.text = "";
            CheckAccount(account);
        }


        private void CheckAccount(Account account)
        {
            if (account != null)
            {
                dropdownRpcCluster.interactable = false;
                manager.ShowScreen(this, "wallet_screen");
                messageTxt.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                passwordInputField.text = string.Empty;
                messageTxt.gameObject.SetActive(true);
            }
        }

        public void OnClose()
        {
            var wallet = GameObject.Find("wallet");
            wallet.SetActive(false);
        }
    }
}

