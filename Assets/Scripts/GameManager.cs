using UnityEngine;

namespace MMO.Managers
{
    /// <summary>
    /// ゲーム全体のマネージャー
    /// シーン初期化とマルチプレイ接続を管理します
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private bool autoSpawnPlayer = true;
        
        private GameObject localPlayer;
        
        private static GameManager instance;
        
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
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
        
        private void Start()
        {
            if (autoSpawnPlayer && playerPrefab != null)
            {
                SpawnPlayer();
            }
        }
        
        /// <summary>
        /// プレイヤーをスポーン
        /// </summary>
        private void SpawnPlayer()
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            localPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            localPlayer.name = "LocalPlayer";
            
            Debug.Log("プレイヤーをスポーンしました: " + spawnPos);
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
