# ActiveReports for .NET JSビューワのテーマカスタマイズ サンプル

ActiveReports for .NET のJSビューワにテーマを適用し、業務システムのデザインに合わせてカスタマイズする方法を紹介するブログ記事のサンプルプロジェクトです。

サイドメニューやヘッダーを持つ業務システム風のレイアウトに、通常（`BusinessLight`）、ダーク（`BusinessDark`）、MESCIUS（`MESCIUS`）のテーマを適用できます。

`/`では基本的なJSビューワ表示例を開き、`/Report1`では業務システム風レイアウトとテーマ切り替え機能を確認できます。`/Report1`の画面上部にあるテーマセレクターからテーマを切り替えると、業務システムの配色とActiveReports JSビューワのテーマが連動して切り替わります。

## ブログ記事

- [ActiveReports for .NETのJSビューワを業務システムのデザインに合わせてカスタマイズする](https://devlog.mescius.jp/activereports-jsviewer-theme/)

## サンプルの内容

このサンプルでは、主に次の内容を確認できます。

- ASP.NET Core MVCアプリケーションへのActiveReports JSビューワの組み込み
- 業務システム風の共通レイアウト
- ActiveReports JSビューワへのカスタムテーマの適用
- システムテーマとJSビューワテーマの連動
- 通常、ダーク、MESCIUSのテーマ切り替え
- `Report.rdlx` のレポート定義をWebブラウザー上に表示
- レポート一覧を返すエンドポイント（`/reports`）

## ファイル構成

```text
.
├─ ThemeReportsApp.slnx
├─ README.md
└─ ThemeReportsApp/
	├─ ClientApp/
	│  ├─ package.json
	│  └─ package-lock.json
	├─ Controllers/
	│  └─ HomeController.cs
	├─ Reports/
	│  └─ Report.rdlx
	├─ Views/
	│  ├─ Home/
	│  │  ├─ Index.cshtml
	│  │  └─ Report1.cshtml
	│  └─ Shared/
	│     └─ _BusinessLayout.cshtml
	├─ wwwroot/
	├─ ActiveReports.config
	├─ Program.cs
	├─ Startup.cs
	└─ ThemeReportsApp.csproj
```

| ファイル／フォルダー | 内容 |
|---|---|
| `Views/Shared/_BusinessLayout.cshtml` | 業務システム風の共通レイアウトとテーマ切り替えUI |
| `Views/Home/Report1.cshtml` | カスタムテーマを適用したJSビューワの表示ページ |
| `Views/Home/Index.cshtml` | JSビューワの基本的な表示例とテーマ設定例 |
| `Reports/Report.rdlx` | JSビューワで表示するレポート定義 |
| `Controllers/HomeController.cs` | ページ、静的リソース、レポート一覧のエンドポイント |
| `Startup.cs` | ActiveReports ViewerとMVCの設定 |
| `ClientApp/` | JSビューワのクライアントアセット生成用npmプロジェクト |

## 動作環境

- .NET SDK 10.0以降
- Node.js 18以降（クライアントアセットを再生成する場合）
- npm
- Webブラウザー
- ActiveReports for .NET

Node.jsはLTSバージョンの利用を推奨します。

## サンプルの実行方法

### 1. リポジトリを取得する

```powershell
git clone <GitHubリポジトリURL>
cd ThemeReportsApp
```

### 2. クライアントアセットを生成する

`ClientApp`の依存パッケージをインストールし、JSビューワのアセットを`wwwroot`へコピーします。

```powershell
Set-Location .\ThemeReportsApp\ClientApp
npm install
npm run build
Set-Location ..\..
```

### 3. アプリケーションを起動してJSビューワのテーマを切り替える

```powershell
dotnet run --project .\ThemeReportsApp\ThemeReportsApp.csproj
```

起動後、Webブラウザーで <http://localhost:5050/> を開くと、基本的なJSビューワアプリが表示されます。

JSビューワのツールバーにあるテーマピッカーを開き、ビューワのテーマを切り替えます。`Index.cshtml`では、`availableThemes`に設定したテーマを選択できます。

### 4. 業務システムとJSビューワのテーマを連動させる

業務システムのレイアウトとJSビューワのテーマを連動させる場合は、<http://localhost:5050/Report1> を開いてください。`/`では基本的なJSビューワ表示例が開くため、業務システム風レイアウトのテーマ切り替え機能は表示されません。

`/Report1`では、画面右上の「テーマ」セレクターから、通常、ダーク、MESCIUSのテーマを選択します。

テーマを変更すると、業務システム側のレイアウト配色とJSビューワのテーマが同じテーマ名で再設定されます。

#### JSビューワ内のテーマピッカーについて

`/Report1`では、業務システム側のテーマセレクターとテーマを連動させるため、JSビューワ内のテーマピッカーを無効化しています。JSビューワ内のテーマピッカーを使用する場合は、`Report1.cshtml`の`themeSelector.enabled`を`true`に変更してください。

このサンプルでは、次のテーマをJSビューワのテーマピッカーから選択できます。

- `default`
- `MESCIUS Theme`
- `defaultDark`
- `darkOled`
- `highContrast`
- `highContrastDark`
- `activeReports`
- `activeReportsDark`

JSビューワ内のテーマピッカーで変更した場合は、JSビューワの表示テーマのみが切り替わります。業務システム全体の配色も切り替える場合は、画面右上のテーマセレクターを使用してください。

## URLと画面の違い

| URL | 内容 |
|---|---|
| <http://localhost:5050/> | `Index.cshtml`による基本的なJSビューワ表示例。テーマセレクターはありません。 |
| <http://localhost:5050/Report1> | `Report1.cshtml`による業務システム風レイアウト。テーマセレクターでテーマを切り替えられます。 |

テーマ切り替えを確認する場合は、<http://localhost:5050/Report1> を開いてください。

## テーマの定義

テーマは`Views/Home/Report1.cshtml`で定義しています。レイアウト側では`body[data-system-theme="BusinessDark"]`のように、`body`要素の`data-system-theme`属性を利用してCSS変数を切り替えています。

## 関連ドキュメント

- [ActiveReports for .NET 製品ページ](https://developer.mescius.jp/activereports)
- [WebデザイナとJSビューワにテーマの適用](https://demo.mescius.jp/activereports/docs/v20/concepts/activereports-web-designer/apply-themes-webdesigner-jsviewer-components)

## ライセンスと注意事項

- このリポジトリは、ブログ記事で紹介するサンプルプロジェクトです。
- ActiveReports for .NETの利用には、MESCIUSのライセンス条件が適用されます。
- NuGet/npmパッケージの利用、再配布、商用利用については、各パッケージおよびMESCIUSのライセンス条項を確認してください。
- `Properties/licenses.licx`はActiveReportsのライセンス設定に関係するファイルです。環境に応じて適切なライセンス設定を行ってください。
- このリポジトリには、個人情報、接続文字列、APIキーなどの機密情報を追加しないでください。
