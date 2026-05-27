using UnityEngine;
using System.Collections.Generic;
using MMO.Animation;

namespace MMO.Avatar
{
    /// <summary>
    /// VRM アバター互換のマルチアバター管理システム
    /// VRM、FBX、glTF など複数形式をサポート
    /// </summary>
    public class AvatarManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> avatarPrefabs = new List<GameObject>();
        [SerializeField] private int defaultAvatarIndex = 0;
        
        private Dictionary<int, GameObject> loadedAvatars = new Dictionary<int, GameObject>();
        private static AvatarManager instance;
        
        public static AvatarManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<AvatarManager>();
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
            PreloadAvatars();
        }
        
        /// <summary>
        /// アバターをプリロード
        /// </summary>
        private void PreloadAvatars()
        {
            for (int i = 0; i < avatarPrefabs.Count; i++)
            {
                if (avatarPrefabs[i] != null)
                {
                    loadedAvatars[i] = avatarPrefabs[i];
                    Debug.Log($"アバター {i} をプリロードしました: {avatarPrefabs[i].name}");
                }
            }
        }
        
        /// <summary>
        /// アバターをインスタンス化
        /// </summary>
        public GameObject CreateAvatar(int avatarIndex, Vector3 position, Quaternion rotation)
        {
            if (!loadedAvatars.ContainsKey(avatarIndex))
            {
                Debug.LogWarning($"アバター {avatarIndex} が見つかりません。デフォルトを使用します。");
                avatarIndex = defaultAvatarIndex;
            }
            
            GameObject avatarPrefab = loadedAvatars[avatarIndex];
            GameObject avatar = Instantiate(avatarPrefab, position, rotation);
            
            // Animator と Character Animator を取得
            Animator animator = avatar.GetComponent<Animator>();
            CharacterAnimator charAnimator = avatar.GetComponent<CharacterAnimator>();
            
            if (animator == null)
            {
                animator = avatar.AddComponent<Animator>();
                Debug.LogWarning($"Animator がないため追加しました: {avatar.name}");
            }
            
            if (charAnimator == null)
            {
                charAnimator = avatar.AddComponent<CharacterAnimator>();
                Debug.LogWarning($"Character Animator がないため追加しました: {avatar.name}");
            }
            
            return avatar;
        }
        
        /// <summary>
        /// VRM ファイルから直接ロード (Runtime VRM Loader使用時)
        /// </summary>
        public GameObject LoadVRMAvatar(string filePath, Vector3 position, Quaternion rotation)
        {
            Debug.Log($"VRM を読み込み中: {filePath}");
            
            // TODO: Runtime VRM Loader を使用してアバターをロード
            // このメソッドは UniVRM または UniGLTF パッケージが必要
            
            Debug.LogError("VRM ローダーはまだ実装されていません。Runtime VRM Loader パッケージを追加してください。");
            return null;
        }
        
        /// <summary>
        /// 利用可能なアバター数を取得
        /// </summary>
        public int GetAvatarCount()
        {
            return loadedAvatars.Count;
        }
        
        /// <summary>
        /// アバターのプリセットをリスト取得
        /// </summary>
        public List<string> GetAvatarNames()
        {
            List<string> names = new List<string>();
            foreach (var kvp in loadedAvatars)
            {
                names.Add(kvp.Value.name);
            }
            return names;
        }
    }
}
