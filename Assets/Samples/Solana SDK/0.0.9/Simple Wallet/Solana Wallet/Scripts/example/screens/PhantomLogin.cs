using System;
using UnityEngine;
using UnityEngine.UI;
using Solana.Unity.Wallet;

namespace Solana.Unity.SDK.Example
{
    public class PhantomLogin : MonoBehaviour
    {
        
        [SerializeField]
        private Button loginBtn;

        private void OnEnable()
        {
            if (Web3.Base != null)
            {
                loginBtn.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            loginBtn.onClick.AddListener(LoginCheckerPhantom);
        }
        
        private async void LoginCheckerPhantom()
        {
            var account = await Web3.Instance.LoginPhantom();
            Console.WriteLine(account);
        }
    }
}