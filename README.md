# My MMO RPG - Unity Project

MMO RPG風のリアルなマルチプレイヤー世界をUnityで構築するプロジェクトです。

## プロジェクト概要

- **エンジン**: Unity 2022.3 LTS以上
- **グラフィック**: セミリアル（URP - Universal Render Pipeline）
- **プラットフォーム**: PC (Windows/Mac)、モバイル (iOS/Android)、WebGL対応予定
- **機能**:
  - アバターシステム（手足の完全なアニメーション）
  - リアルタイムマルチプレイヤー同期（Netcode for GameObjects）
  - 動的なアニメーション（IK対応）
  - キャラクターコントローラー
  - カメラシステム（Cinemachine）

## セットアップ手順

### 必要な環境
- Unity 2022.3 LTS以上
- Visual Studio Code
- Git

### インストール

1. **Unityエディタで開く**
   - Unityを起動
   - Open Project → このディレクトリを選択
   - Unityが依存パッケージを自動ダウンロード（数分かかります）

2. **プロジェクト初期化**
   ```
   git clone <this-repo>
   cd my-mmo-rpg
   ```

3. **Packages確認**
   - Packages/manifest.json が自動で読み込まれます
   - 必要なパッケージが Package Manager から自動インストールされます

## プロジェクト構造

```
my-mmo-rpg/
├── Assets/
│   ├── Scripts/          # C#スクリプト
│   │   ├── Player/       # プレイヤー関連
│   │   ├── Network/      # ネットワーク関連
│   │   ├── Animation/    # アニメーション関連
│   │   └── UI/           # UI関連
│   ├── Scenes/           # シーン
│   │   ├── MainScene.unity
│   │   └── Lobby.unity
│   ├── Prefabs/          # プレハブ
│   │   ├── Player.prefab
│   │   └── RemotePlayer.prefab
│   ├── Models/           # 3Dモデル (FBX, glTF)
│   ├── Materials/        # マテリアル
│   ├── Animations/       # アニメーションファイル
│   └── ...
├── Packages/
│   └── manifest.json     # 依存パッケージ定義
├── ProjectSettings/      # Unityプロジェクト設定
└── README.md
```

## 主要機能実装予定

### Phase 1: 基本システム
- ✅ プロジェクト構造
- [ ] プレイヤーキャラクター（3Dモデル）
- [ ] 手足のアニメーション
- [ ] 歩行・走行モーション
- [ ] カメラコントローラー

### Phase 2: インタラクション
- [ ] キャラクター移動システム
- [ ] アバター操作（手足の動き）
- [ ] アニメーション状態機械
- [ ] IK (逆運動学) システム

### Phase 3: マルチプレイ
- [ ] Netcode統合
- [ ] プレイヤーシンク
- [ ] リモートプレイヤー表示
- [ ] チャットシステム

### Phase 4: グラフィック
- [ ] セミリアルシェーダー
- [ ] ライティング最適化
- [ ] PostProcessing (ブルーム、SSAOなど)
- [ ] 環境ディテール

## スクリプト仕様

### PlayerController.cs
プレイヤーキャラクターの移動と入力制御を担当します。

```csharp
- OnInput() : キーボード/ゲームパッド入力
- Move(direction) : 移動処理
- Rotate(direction) : 回転処理
- Jump() : ジャンプ処理
```

### AnimationController.cs
キャラクターアニメーションの管理。

```csharp
- PlayAnimation(animationName) : アニメーション再生
- SetAnimationSpeed(speed) : アニメーション速度調整
- UpdateIK() : IK計算
```

### NetworkManager.cs (実装予定)
マルチプレイヤー同期。

```csharp
- ConnectToServer()
- SendPlayerState()
- OnRemotePlayerUpdate()
```

## アセット仕様

### キャラクターモデル
- フォーマット: FBX または glTF 2.0
- リグ: Humanoid
- テクスチャ解像度: 2048x2048以上
- 骨数: 20-40本（標準的なヒューマノイド）

### アニメーション要件
- Idle (待機)
- Walk_Forward (前進)
- Walk_Backward (後退)
- Run (走行)
- Jump (ジャンプ)
- Wave (手を振る)
- Dance (ダンス)
- Sit (座る)

## パフォーマンス目標

| プラットフォーム | FPS | 解像度 |
|---|---|---|
| PC | 60+ | 1440p |
| モバイル | 30+ | 1080p |
| WebGL | 30+ | 720p |

## トラブルシューティング

### Unityが起動しない
```bash
# Projectファイルをリセット
rm -rf Library
# Unityを再度開く
```

### パッケージが見つからない
```
Window → TextMesh Pro → Import TMP Essentials
```

### アニメーションが再生されない
- Animator Controllerが正しく割り当てられているか確認
- Animation clipsが Humanoid設定になっているか確認

## 参考リンク

- [Unity公式ドキュメント](https://docs.unity3d.com/)
- [Netcode for GameObjects](https://github.com/Unity-Technologies/netcode.gameobjects)
- [Animation Rigging](https://docs.unity3d.com/Packages/com.unity.animation.rigging@latest)
- [URP Documentation](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)

## ライセンス

このプロジェクトはMITライセンス下で公開されています。

## 連絡先

質問や提案がある場合は、プロジェクトのissueを作成してください。

## オンライン公開 (GitHub Pages)

このリポジトリは GitHub Pages で公開できます。現在の公開先（作成済み）:

- GitHub Pages URL: https://jobtransitionjp-jpg.github.io/my-mmo-rpg

簡単な公開手順（Unity WebGL ビルド後）:

1. Unity で WebGL ビルドを作成し、出力先を `Builds/WebGL/` にする。
2. ビルド出力の中身を `docs/` フォルダへコピーします（リポジトリのルートに `docs/` があればその中身を上書き）。

```bash
cd /Users/tsuyosiito/my-mmo-rpg
cp -r Builds/WebGL/* docs/
git add docs/
git commit -m "Add WebGL build for GitHub Pages"
git push -u origin main
```

3. GitHub のリポジトリ設定 → `Settings` → `Pages` で `Source` を `main / docs` に設定します。数分で公開されます。

レンタルサーバーへ移行する場合は、`Builds/WebGL/` の中身を FTP/SFTP でアップロードしてください。

