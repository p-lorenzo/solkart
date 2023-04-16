using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using codebase.utility;
using Cysharp.Threading.Tasks;
using Solana.Unity.Extensions;
using Solana.Unity.Rpc.Types;

namespace Solana.Unity.SDK.Example
{
    public class GameScreen : SimpleScreen
    {
        [SerializeField]
        private TextMeshProUGUI lamports;
        
        private CancellationTokenSource _stopTask;

        public void Start()
        {
            _stopTask = new CancellationTokenSource();

            RefreshWallet();
            Web3.WsRpc.SubscribeAccountInfo(
                Web3.Instance.Wallet.Account.PublicKey,
                (_, accountInfo) =>
                {
                    Debug.Log("Account changed!, updated lamport: " + accountInfo.Value.Lamports);
                    RefreshWallet();
                },
                Commitment.Confirmed
            );

        }
        
        private void OnEnable()
        {
            Loading.StopLoading();
        }

        private void RefreshWallet()
        {
            UpdateWalletBalanceDisplay().AsUniTask().Forget();
        }
        
        private async Task UpdateWalletBalanceDisplay()
        {
            if (Web3.Instance.Wallet.Account is null) return;
            var sol = await Web3.Base.GetBalance(Commitment.Confirmed);
            MainThreadDispatcher.Instance().Enqueue(() =>
            {
                //log
                Debug.Log("Wallet balance: " + sol);
                lamports.text = $"{sol}";
            });
        }
        
        private void OnDestroy()
        {
            if (_stopTask is null) return;
            _stopTask.Cancel();
        }
    }
}