# 🎮 My MMO RPG - Unityプロジェクト

## ✨ 完成内容

Unityを使用したセミリアルMMO RPG風メタバースプロジェクトが完成しました！

### プロジェクト構成

```
my-mmo-rpg/
├── Assets/
│   ├── Scripts/
│   │   ├── PlayerController.cs          ← プレイヤー操作
│   │   ├── CharacterAnimator.cs         ← 手足アニメーション＆IK
│   │   ├── CameraController.cs          ← カメラシステム
│   │   ├── GameManager.cs               ← ゲーム管理
│   │   ├── AvatarManager.cs             ← VRM/アバター管理
│   │   └── NetworkGameManager.cs        ← マルチプレイ同期
│   ├── Scenes/                          ← ゲームシーン
│   ├── Prefabs/                         ← プレハブ
│   ├── Models/                          ← 3Dモデル (VRM/FBX)
│   ├── Materials/                       ← マテリアル
│   └── Animations/                      ← アニメーション
├── Packages/
│   └── manifest.json                    ← 依存パッケージ
├── README.md                            ← プロジェクト説明
├── SETUP_GUIDE_JA.md                    ← 詳細セットアップ
├── MULTIPLATFORM_GUIDE.md               ← 複数プラットフォーム対応
├── VRM_GUIDE.md                         ← VRM実装ガイド
└── .gitignore
```

## 🚀 今すぐ始める

### ステップ 1: Unityで開く

```bash
# Unityパスが設定されている場合:
open -a "Unity" /Users/tsuyosiito/my-mmo-rpg

# または Unity Hub から:
1. Open → Add project from disk
2. /Users/tsuyosiito/my-mmo-rpg を選択
```

### ステップ 2: プロジェクト初期化（3-5分）

- Unityが必要なパッケージを自動インストール
- Cinemachine, Input System, Netcode, URP などが追加される

### ステップ 3: セットアップガイド実行

詳細は以下を参照：

- **セットアップ手順**: [SETUP_GUIDE_JA.md](SETUP_GUIDE_JA.md)
- **キャラクター設定**: 3Dモデルのインポートとアニメーション設定
- **シーン構築**: MainScene の作成

### ステップ 4: テスト実行

```
Unity Play ボタン → W/A/S/D で移動 → マウスで視点操作
```

## 🎯 主要機能

### ✅ 完成機能

| 機能 | 説明 | ファイル |
|---|---|---|
| **プレイヤー操作** | WASD移動、マウス視点、SPACE ジャンプ | PlayerController.cs |
| **キャラクターアニメーション** | 歩行、走行、ジャンプ、IK対応 | CharacterAnimator.cs |
| **手足の動き** | 自然な腕振りと足の動き | CharacterAnimator.cs |
| **カメラシステム** | 第三者視点で追従 | CameraController.cs |
| **マルチアバター対応** | VRM、FBX、glTF など複数形式 | AvatarManager.cs |
| **ゲーム管理** | シーン初期化、プレイヤースポーン | GameManager.cs |

### 🔄 準備中機能

| 機能 | 説明 | 予定 |
|---|---|---|
| **マルチプレイ同期** | Netcode for GameObjects | Phase 3 |
| **チャットシステム** | プレイヤー間通信 | Phase 3 |
| **セミリアルグラフィック** | URP + PostProcessing | Phase 4 |
| **インタラクション** | オブジェクト操作、NPCとの会話 | Phase 2 |
| **キャラクター作成** | アバターカスタマイズ画面 | Future |

## 📋 実装済みスクリプト説明

### PlayerController.cs
```csharp
// プレイヤーの移動と入力制御
- HandleInput() : キー入力処理
- UpdateMovement() : 物理演算を使用した移動
- UpdateAnimation() : アニメーションパラメータ設定
- Jump() : ジャンプ処理
```

### CharacterAnimator.cs
```csharp
// キャラクター全身のアニメーション管理
- OnAnimatorIK() : IK計算 (手足の自動調整)
- PlayJumpAnimation() : ジャンプアニメーション
- PlayWaveAnimation() : 挨拶アニメーション
- PlayDanceAnimation() : ダンスアニメーション
```

### CameraController.cs
```csharp
// Cinemachine を使用したカメラシステム
- プレイヤー追従
- マウス視点操作
- スムーズなフォロー
```

### NetworkGameManager.cs
```csharp
// Netcode for GameObjects を使用したマルチプレイ
- UpdatePlayerPositionRpc() : 位置同期
- PlayAnimationRpc() : アニメーション同期
- SendChatMessageRpc() : チャット送信
```

## 🎨 グラフィック設定

### セミリアルスタイル

```
品質: リアルと映画的なバランス
- テクスチャ: 2K - 4K解像度
- シェーディング: PBR (物理ベースレンダリング)
- ライティング: リアルタイム + ベイク
- PostProcessing: ブルーム、SSAO、色補正
```

### パフォーマンス目標

| プラットフォーム | FPS | 解像度 |
|---|---|---|
| PC (Windows/Mac) | 60+ | 1440p |
| モバイル (iOS/Android) | 30+ | 1080p |
| WebGL | 30+ | 720p |

## 🌍 マルチプラットフォーム対応

このプロジェクトは、以下のプラットフォームに対応可能です：

- ✅ **PC** (Windows, macOS, Linux)
- ✅ **モバイル** (iOS, Android)
- ✅ **WebGL** (ブラウザ)
- ✅ **VR** (Meta Quest, HTC Vive など)

詳細は [MULTIPLATFORM_GUIDE.md](MULTIPLATFORM_GUIDE.md) を参照

## 👾 VRM アバター対応

VRM (Virtual Reality Model) をサポート！

### VRM 導入方法

1. **UniVRM パッケージをインストール**
   ```
   Package Manager → git URL から追加
   https://github.com/vrm-c/UniVRM.git?path=/Assets/VRM
   ```

2. **VRM ファイルを配置**
   ```
   Assets/Models/avatar.vrm
   ```

3. **Animator 設定**
   - Humanoid rig を選択
   - Avatar を自動生成

詳細は [VRM_GUIDE.md](VRM_GUIDE.md) を参照

## 🔧 トラブルシューティング

### Q: Unityが起動しない
```
A: Library フォルダを削除して再度開く
   rm -rf Library
```

### Q: パッケージが見つからない
```
A: Package Manager を開いて手動でインストール
   Window → Package Manager
```

### Q: アニメーションが再生されない
```
A: Animator Controller が正しく割り当てられているか確認
   - Avatar が Humanoid 設定か確認
   - Animation clips が正しい形式か確認
```

### Q: FPS が低い
```
A: グラフィック品質を下げる
   Edit → Project Settings → Quality
```

## 📚 参考資料

### 公式ドキュメント
- [Unity マニュアル](https://docs.unity3d.com/)
- [Netcode for GameObjects](https://github.com/Unity-Technologies/netcode.gameobjects)
- [Cinemachine](https://docs.unity3d.com/Packages/com.unity.cinemachine@latest/manual/)
- [Universal Render Pipeline](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)

### コミュニティ
- [Unity Learn](https://learn.unity.com/)
- [Brackeys (YouTube)](https://www.youtube.com/c/Brackeys)
- [VRM 公式](https://vrm.dev/)

## 📄 ライセンス

このプロジェクトはMITライセンス下で公開されています。
自由に使用、改変、配布できます。

## 🎉 次のステップ

1. ✅ Unityでプロジェクトを開く
2. ✅ [SETUP_GUIDE_JA.md](SETUP_GUIDE_JA.md) に従ってセットアップ
3. ✅ メインシーンを作成
4. ✅ 3Dキャラクターモデルをインポート
5. ✅ Play で動作テスト
6. ✅ マルチプレイ機能を実装 (オプション)
7. ✅ VRM サポートを追加 (オプション)

---

**プロジェクト作成日**: 2026年5月27日  
**Unityバージョン**: 2022.3 LTS 以上  
**リアリティレベル**: セミリアル (URP対応)
