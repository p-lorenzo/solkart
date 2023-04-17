using System;
using UnityEngine;
using UnityEngine.UI;

namespace Solana.Unity.SDK.Example
{
    public class PhantomLogin : MonoBehaviour
    {
        
        [SerializeField]
        private Button loginBtn;

        private void Start()
        {
            loginBtn.onClick.AddListener(LoginCheckerPhantom);
            
            //if platform android then remove all listeners and add sms login
            if (Application.platform == RuntimePlatform.Android)
            {
                loginBtn.onClick.RemoveAllListeners();
                loginBtn.onClick.AddListener(LoginCheckerSms);
            }
        }
        
        private async void LoginCheckerPhantom()
        {
            var account = await Web3.Instance.LoginPhantom();
            Console.WriteLine(account);
        }
        
        private async void LoginCheckerSms()
        {
            var account = await Web3.Instance.LoginSolanaMobileStack();
            Console.WriteLine(account);
        }
    }
}