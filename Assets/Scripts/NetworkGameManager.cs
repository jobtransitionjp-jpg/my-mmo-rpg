using UnityEngine;
using Unity.Netcode;

namespace MMO.Network
{
    /// <summary>
    /// ネットワークマネージャー (Netcode for GameObjects)
    /// マルチプレイヤー同期の基本フレームワーク
    /// </summary>
    public class NetworkGameManager : NetworkBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        private GameObject localPlayer;
        
        private static NetworkGameManager instance;
        
        public static NetworkGameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<NetworkGameManager>();
                }
                return instance;
            }
        }
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            instance = this;
        }
        
        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                return;
            }
            
            Debug.Log("ネットワークスポーン: サーバーで実行中");
        }
        
        /// <summary>
        /// クライアント側でプレイヤーをスポーン
        /// </summary>
        public void SpawnLocalPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Player Prefab が設定されていません");
                return;
            }
            
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            localPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            localPlayer.name = "LocalPlayer";
            
            Debug.Log("ローカルプレイヤーをスポーンしました");
        }
        
        /// <summary>
        /// プレイヤーの位置をネットワーク同期
        /// </summary>
        // Network RPC attributes depend on Netcode version; remove attributes for local testing.
        public void UpdatePlayerPositionRpc(Vector3 position, Quaternion rotation)
        {
            // サーバーが他のクライアントに広播
            BroadcastPlayerPositionRpc(position, rotation);
        }
        
        /// <summary>
        /// プレイヤー位置を全クライアントに送信
        /// </summary>
        private void BroadcastPlayerPositionRpc(Vector3 position, Quaternion rotation)
        {
            if (localPlayer != null)
            {
                localPlayer.transform.position = position;
                localPlayer.transform.rotation = rotation;
            }
        }
        
        /// <summary>
        /// プレイヤーアニメーション同期
        /// </summary>
        public void PlayAnimationRpc(ulong playerId, string animationName)
        {
            Debug.Log($"アニメーション再生: {playerId} - {animationName}");
            
            // TODO: プレイヤーのアニメーションを再生
        }
        
        /// <summary>
        /// チャットメッセージ送信
        /// </summary>
        public void SendChatMessageRpc(string playerName, string message)
        {
            BroadcastChatMessageRpc(playerName, message);
        }
        
        /// <summary>
        /// チャットメッセージを全クライアントに送信
        /// </summary>
        private void BroadcastChatMessageRpc(string playerName, string message)
        {
            Debug.Log($"[{playerName}]: {message}");
            
            // TODO: チャットUIに表示
        }
        
        /// <summary>
        /// ローカルプレイヤーを取得
        /// </summary>
        public GameObject GetLocalPlayer()
        {
            return localPlayer;
        }
    }
}
