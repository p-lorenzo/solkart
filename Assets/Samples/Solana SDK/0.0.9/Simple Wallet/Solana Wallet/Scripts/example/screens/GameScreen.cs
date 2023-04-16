using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Solana.Unity.Rpc.Types;
using TMPro;
using UnityEngine;

namespace Solana.Unity.SDK.Example
{
    public class GameScreen : SimpleScreen
    {
        [SerializeField]
        private TextMeshProUGUI walletAddress;

        public void Start()
        {
            //log public key
            walletAddress.text = Web3.Instance.Wallet.Account.PublicKey;
        }
    }
}