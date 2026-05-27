# UnityプロジェクトのVRM対応実装ガイド

## VRM (Virtual Reality Model) とは

VRMは、VRコンテンツ用に設計された人型3Dモデルの標準フォーマットです。
- 全身アニメーション対応
- 複数プラットフォーム互換性
- Humanoid rigged
- 自由に使用可能なモデルも多数存在

## VRM 対応方法

### Option 1: UniVRM をパッケージとして使用（推奨）

**セットアップ:**

1. **Package Manager で VRM をインストール**
   ```
   Window → Package Manager
   + → Add package from git URL
   https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM
   ```

2. **サンプルモデルをダウンロード**
   - [VRM 公式サイト](https://vrm.dev/)
   - [Sketchfab VRM](https://sketchfab.com/search?q=vrm)

3. **VRMファイルをAssetsフォルダに配置**
   ```
   Assets/Models/avatar.vrm
   ```

### Option 2: Runtime VRM ローダーを実装

**VRMランタイムロード:**

```csharp
using VRM;
using gltfast;

public class VRMRuntimeLoader : MonoBehaviour
{
    public async void LoadVRM(string filePath)
    {
        var importer = new GltfImport();
        
        // ファイルをロード
        bool success = await importer.Load(filePath);
        
        if (!success)
        {
            Debug.LogError("VRM ロード失敗");
            return;
        }
        
        // VRMメタデータを取得
        var vrm = importer.GetComponent<Vrm>();
        if (vrm != null)
        {
            Debug.Log("VRM ロード成功: " + vrm.Meta.Title);
        }
        
        // ゲームオブジェクトをインスタンス化
        await importer.InstantiateMainScene(transform);
    }
}
```

## VRM + Animator の統合

### VRM ボーンマッピング

VRMモデルを Humanoid Animator と統合するには：

```csharp
public class VRMAnimatorAdapter : MonoBehaviour
{
    private Vrm vrm;
    private Animator animator;
    
    void Start()
    {
        vrm = GetComponent<Vrm>();
        animator = GetComponent<Animator>();
        
        // VRM の骨をHumanoid Avatarに割り当て
        AssignBonesToAnimator();
    }
    
    void AssignBonesToAnimator()
    {
        // 標準的なボーン名の割り当て
        // Hips, Spine, Chest, Neck, Head
        // LeftUpperArm, LeftLowerArm, LeftHand, etc.
        
        // VRM独特の骨構造に対応させる
    }
}
```

## VRM の手のアニメーション

### 指の詳細なアニメーション (ボーン対応)

```csharp
public class VRMFingerAnimation : MonoBehaviour
{
    private Transform[] fingerBones;
    
    void AnimateFingers()
    {
        // 5本の指のアニメーション
        // - Thumb, Index, Middle, Ring, Pinky
        
        float grip = Input.GetAxis("Grip");
        
        foreach (var bone in fingerBones)
        {
            bone.localRotation *= Quaternion.Euler(grip * 90, 0, 0);
        }
    }
}
```

## セミリアル グラフィック設定 (VRM向け)

### URP (Universal Render Pipeline) 設定

```yaml
# Assets/Settings/URPAsset.asset
- Color Space: Linear
- Anti Aliasing: MSAA 4x
- Shadow Distance: 100
- Soft Shadows: Enabled
- Shadow Atlas Resolution: 4096x4096
- Ambient Light Intensity: 1.0
```

### マテリアル設定

```csharp
// VRMモデル用の セミリアルシェーダー
Shader "VRM/SemiRealistic"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white"
        _NormalMap ("Normal Map", 2D) = "bump"
        _RoughnessMap ("Roughness", 2D) = "white"
        _MetalicMap ("Metallic", 2D) = "black"
        _SkinRoughness ("Skin Roughness", Range(0, 1)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" }
        
        Pass
        {
            // PBR シェーディング (セミリアル)
        }
    }
}
```

## VRM マルチプレイヤー同期

### ネットワーク経由でのアニメーション同期

```csharp
[Rpc(SendTo.All)]
public void SyncVRMAnimationRpc(
    float walkSpeed,
    float turnAmount,
    bool jumping
)
{
    animator.SetFloat("Speed", walkSpeed);
    animator.SetFloat("Turn", turnAmount);
    animator.SetBool("Jump", jumping);
}
```

## VRM モデル選定ガイド

### 推奨スペック

| 項目 | 推奨値 |
|---|---|
| ポリゴン数 | 10,000 - 50,000 |
| ボーン数 | 50 - 150 |
| テクスチャ解像度 | 1024 - 2048 |
| ファイルサイズ | 5 - 50 MB |

### 無料VRM モデルソース

1. **VRM 公式ギャラリー**
   - https://vrm.dev/

2. **Sketchfab**
   - VRM で検索
   - CC ライセンス確認

3. **pixiv**
   - VRM タグで検索

4. **VRoid Studio**
   - 3D キャラクター制作ツール
   - VRM エクスポート対応

## トラブルシューティング

### VRM ロード時のエラー

```
エラー: "VRM Meta not found"
→ VRMファイルが正しいか確認
→ UniVRM パッケージが正しくインストールされているか確認
```

### アニメーションがぎこちない

```
→ Animator Controller の ボーンマッピング確認
→ Avatar の設定を確認 (Humanoid type)
→ アニメーション クリップの Humanoid 設定を確認
```

### パフォーマンスが低い

```
→ VRM モデルのポリゴン数削減
→ シェーダーを簡略化
→ テクスチャ解像度を削減
→ LOD (Level of Detail) システムを導入
```

## 参考リンク

- [VRM 公式ドキュメント](https://vrm.dev/)
- [UniVRM GitHub](https://github.com/vrm-c/UniVRM)
- [VRoid Studio](https://vroid.com/studio)
- [Sketchfab VRM](https://sketchfab.com/)
