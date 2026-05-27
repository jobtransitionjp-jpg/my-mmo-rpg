# Unityプロジェクト セットアップガイド

## 手順1: プロジェクトをUnityで開く

1. **Unity Hub を開く**
   - または、Unityエディタを直接起動

2. **Open → Add project from disk**
   - `/Users/tsuyosiito/my-mmo-rpg` を選択

3. **プロジェクトを開く**
   - Unityが初期化中...（3-5分待機）
   - ライブラリの生成やパッケージのダウンロードが始まります

## 手順2: 依存パッケージをインストール

Unityが起動したら、以下を確認：

1. **Window → Package Manager** を開く

2. **以下のパッケージが自動でインストールされているか確認**
   - Cinemachine 2.9.7以上
   - Input System 1.7.0以上
   - Netcode for GameObjects 1.8.1以上
   - Universal RP 14.0.11以上
   - Animation Rigging 1.2.1以上
   - TextMesh Pro 3.0.6以上

3. **未インストールの場合**
   - 左上の「+」ボタンをクリック
   - 「Add package by name...」を選択
   - 「com.unity.cinemachine」など入力

## 手順3: プレイヤーキャラクターの設定

### 3Dモデルのインポート

1. **キャラクターモデルを用意**
   - FBX形式、またはglTF 2.0形式
   - Humanoid rigged model（人型リグ）推奨

2. **Assets/Models に配置**
   - 自動でインポートされます

3. **モデルインポート設定**
   ```
   - Assets/Models内のモデルを選択
   - Inspector → Rig タブ
   - Animation Type: Humanoid
   - Avatar Definition: Create From This Model
   - Apply → Restart Rig
   ```

### Animator Controllerの作成

1. **Assets/Animations に Animator Controller を作成**
   ```
   右クリック → Create → Animator Controller → "PlayerAnimator"
   ```

2. **ステートマシンを設定**
   ```
   - Idle (待機)
   - Walk (歩行)
   - Run (走行)
   - Jump (ジャンプ)
   - Wave (挨拶)
   - Dance (ダンス)
   ```

3. **Parametersを追加**
   ```
   - Float: "Speed"
   - Float: "Direction"
   - Bool: "IsGrounded"
   - Trigger: "Jump"
   - Trigger: "Wave"
   - Trigger: "Dance"
   ```

## 手順4: シーンの作成

### メインシーン

1. **Assets/Scenes に新しいシーンを作成**
   ```
   右クリック → Create → Scene → "MainScene"
   ```

2. **基本的なオブジェクトを作成**
   ```
   Hierarchy パネルで右クリック:
   - 3D Object → Plane (地面)
   - 3D Object → Directional Light (太陽光)
   - Create Empty → "GameManager" (マネージャー)
   ```

3. **地面の設定**
   ```
   - Plane を選択
   - Scale: (10, 1, 10)
   - Material: グリーン色
   - Add Component → Mesh Collider
   ```

4. **GameManager をセットアップ**
   ```
   - GameManager を選択
   - Add Component → Game Manager スクリプト
   - Player Prefab: プレイヤープレハブを割り当て
   ```

## 手順5: プレイヤープレハブの作成

### キャラクターGameObjectを作成

1. **キャラクターモデルをシーンにドラッグ**

2. **以下のコンポーネントを追加**
   ```
   - Character Controller (移動用)
   - Animator (アニメーション用)
   - Player Controller (操作スクリプト)
   - Character Animator (アニメーション管理)
   ```

3. **IKターゲットを設定**
   ```
   左手用の Empty GameObject → "LeftHandTarget"
   右手用の Empty GameObject → "RightHandTarget"
   左足用の Empty GameObject → "LeftFootTarget"
   右足用の Empty GameObject → "RightFootTarget"
   
   これらをCharacter Animator に割り当て
   ```

4. **プレハブ化**
   ```
   キャラクターをAssets/Prefabs にドラッグ
   → "Player.prefab" として保存
   ```

## 手順6: カメラの設定

### Cinemachine 仮想カメラの作成

1. **Cinemachine → Create Virtual Camera** を選択

2. **設定**
   ```
   - Follow: プレイヤーの Transform
   - Look At: プレイヤーの Transform
   - Camera Distance: 5
   - Height: 2
   ```

3. **アニメーション設定**
   - Damping: 0.3 (滑らかなフォロー)

## 手順7: ライティング設定（セミリアル仕様）

### 高品質ライティング

1. **Window → Rendering → Lighting Settings** を開く

2. **設定**
   ```
   - Skybox: Sky-Default または購入したスカイボックス
   - Ambient Light Color: 白
   - Ambient Intensity: 1.0
   ```

3. **太陽光の調整**
   ```
   - Directional Light の色: 温かい黄色 (#FFE6CC)
   - Intensity: 1.5
   - Shadow: Hard Shadows
   - Shadow Resolution: Very High
   ```

## 手順8: 実行テスト

1. **Game ビューに切り替え**

2. **Play ボタンを押す**

3. **動作確認**
   - W/A/S/D キーで移動
   - マウスで視点操作
   - SPACE キーでジャンプ
   - アニメーションが滑らかに再生される

## よくあるエラーと対処法

### エラー: "Animator parameter not found"
```
→ Animator Controller の Parametersが正しく設定されているか確認
→ PlayerController スクリプトのハッシュ名を確認
```

### エラー: "Character is missing a Rigidbody"
```
→ Character Controller を追加
→ または Rigidbody を追加して Use Gravity をOFFに
```

### アニメーションが再生されない
```
→ Animator の Avatar が正しく割り当てられているか確認
→ Animation clips が Humanoid タイプになっているか確認
→ Animator Controller が正しく割り当てられているか確認
```

### カメラが見えない
```
→ メインカメラが存在するか確認
→ Main Camera タグが割り当てられているか確認
→ カメラの Near Clip Plane を 0.1 に設定
```

## 次のステップ

1. **キャラクターモデルのインポート**
   - 複数のVRM/FBXモデルをテスト

2. **アニメーションセットの拡充**
   - ダンス、座る、など追加モーション

3. **Netcode 統合**
   - マルチプレイヤー同期の実装

4. **UI の実装**
   - ユーザー名表示
   - チャットシステム

## 参考資料

- [Unity Animator Documentation](https://docs.unity3d.com/Manual/class-Animator.html)
- [Character Controller Documentation](https://docs.unity3d.com/ScriptReference/CharacterController.html)
- [Cinemachine User Guide](https://docs.unity3d.com/Packages/com.unity.cinemachine@latest/manual/CinemachineUsageGuide.html)
- [Animation Rigging Documentation](https://docs.unity3d.com/Packages/com.unity.animation.rigging@latest/manual/index.html)
