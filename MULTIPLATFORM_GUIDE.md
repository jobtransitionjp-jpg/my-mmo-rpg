# 複数プラットフォーム対応ガイド

## プラットフォーム別設定

### 1. PC (Windows/Mac)

**推奨スペック**
- CPU: i7 以上
- GPU: RTX 3060 以上
- RAM: 16GB

**ビルド設定**
```
File → Build Settings
- Platform: PC, Mac & Linux Standalone
- Target Platform: 
  - Windows (x86_64)
  - Mac (Silicon / Intel)
- Resolution: 1920x1080
- Graphics Quality: Ultra
```

**パフォーマンス最適化**
```
Edit → Project Settings → Quality
- Anti Aliasing: 2x or 4x
- Shadow Distance: 100
- Texture Quality: Full Res
```

### 2. モバイル (iOS/Android)

**最小スペック**
- iOS 11以上
- Android 7.0 (API 24) 以上

**ビルド設定**
```
File → Build Settings
- Platform: iOS または Android
- Resolution: 1080x1920
- Graphics Quality: Medium
- Target Framerate: 30 FPS
```

**最適化設定**
```
Edit → Project Settings
- Physics → Default Solver: 2
- Rendering → URP → Low Quality
- Target FPS: 30
```

**iOS 設定**
```
File → Build Settings
- Target SDK: Device SDK
- Code Stripping: Low
- Create Folder: "iOSBuild"
```

**Android 設定**
```
File → Build Settings
- Target API Level: 31
- Minimum API Level: 24
- Create Folder: "AndroidBuild"
```

### 3. WebGL (Web ブラウザ)

**ビルド設定**
```
File → Build Settings
- Platform: WebGL
- Resolution: 1280x720
- Quality: Low
- Target Framerate: 30 FPS
```

**最適化設定**
```
Edit → Project Settings
- Player → WebGL → Compression Format: Brotli
- Player → WebGL → Memory: 512MB
- Stripping Level: Strip All
```

**サイズ最適化**
```
Build Size: 推奨 50-100MB
方法:
1. Assetバンドルの使用
2. 不要なスクリプトの削除
3. テクスチャ圧縮: ASTC / ETC2
```

## クロスプラットフォーム スクリプト設計

### Input System の統一

```csharp
// 各プラットフォームで同じ入力を処理
#if UNITY_STANDALONE
    // PC: キーボード + マウス
    float horizontal = Input.GetAxis("Horizontal");
#elif UNITY_IOS || UNITY_ANDROID
    // モバイル: ジョイスティック
    float horizontal = Input.GetAxis("Horizontal");
#elif UNITY_WEBGL
    // WebGL: キーボード
    float horizontal = Input.GetAxis("Horizontal");
#endif
```

### グラフィック設定の自動調整

```csharp
void SetGraphicsQuality()
{
    #if UNITY_ANDROID || UNITY_IOS
        QualitySettings.SetQualityLevel(1); // Medium
    #elif UNITY_WEBGL
        QualitySettings.SetQualityLevel(0); // Low
    #else
        QualitySettings.SetQualityLevel(3); // Ultra
    #endif
}
```

### ネットワーク最適化

```csharp
// モバイルではパケット削減
#if UNITY_ANDROID || UNITY_IOS
    syncRate = 20; // 毎秒20回
#else
    syncRate = 60; // 毎秒60回
#endif
```

## ビルドとデプロイ

### Windows ビルド

```bash
# コマンドラインビルド
"C:\Program Files\Unity\Editor\Unity.exe" -quit -batchmode -projectPath . \
  -buildWindowsPlayer "Builds\Windows\game.exe"
```

### macOS ビルド

```bash
# M1/M2 Mac
/Applications/Unity/Hub/Editor/2022.3.*/Unity.app/Contents/MacOS/Unity \
  -quit -batchmode -projectPath . \
  -buildOSXUniversalPlayer "Builds/Mac/MyGame.app"
```

### iOS ビルド

```bash
# Xcode プロジェクト生成
# その後 Xcode で開いてビルド
xcode-select --install  # Xcode Command Line Tools
```

### Android ビルド

```bash
# Android Studio のセットアップ必須
# File → Build Settings → Build Android Bundle
```

### WebGL ビルド

```bash
# File → Build Settings → Build
# ブラウザで Builds/WebGL/index.html を開く
python3 -m http.server 8000  # ローカルサーバー起動
```

## パフォーマンス計測

### フレームレート監視

```csharp
void ShowFPS()
{
    float fps = 1f / Time.deltaTime;
    Debug.Log($"FPS: {fps:F1}");
}
```

### メモリ使用量

```csharp
void ShowMemory()
{
    long memMB = System.GC.GetTotalMemory(false) / (1024 * 1024);
    Debug.Log($"Memory: {memMB} MB");
}
```

### プロファイラの使用

```
Window → Analysis → Profiler
- CPU Usage
- Memory
- GPU Usage
- Network
```

## トラブルシューティング

### iOS でビルド失敗

```
→ Xcode のバージョンを確認 (最新版に更新)
→ iOS Deployment Target を確認 (11以上)
→ Provisioning Profile を確認
```

### Android でアニメーションが遅い

```
→ Target API Level を上げる (28以上推奨)
→ Graphics Quality を下げる
→ Shadow Distance を減らす
```

### WebGL でメモリ不足

```
→ Assetバンドルの分割
→ テクスチャ圧縮の強化
→ 低解像度テクスチャの使用
```

## チェックリスト

- [ ] PC ビルド動作確認
- [ ] モバイル ビルド動作確認
- [ ] WebGL ビルド動作確認
- [ ] FPS が目標値を達成
- [ ] メモリ使用量が許容範囲内
- [ ] 全プラットフォームでアニメーション滑らか
- [ ] ネットワーク遅延許容範囲内
