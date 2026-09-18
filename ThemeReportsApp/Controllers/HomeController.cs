using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace ThemeReportsApp
{
    // アプリケーションのトップページと、レポートViewerが使用するリソースを提供するコントローラーです。
    [Route("/")]
    public class HomeController : Controller
    {
        // ルートURL（/）で簡易Viewerページを表示します。
        public object Index() => View("Index");

        // wwwroot配下の静的ファイルを読み込むためのホスティング環境です。
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // JS Viewerが必要とするCSS、JavaScript、faviconなどのファイルを返します。
        [HttpGet("{file}")]
        public object Resource(string file)
        {
            // WebRootPath（wwwroot）を基準に、要求されたファイルのパスを組み立てます。
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, file);

            // 指定されたファイルが存在しない場合は404を返します。
            if (!System.IO.File.Exists(filePath))
                return new NotFoundResult();

            // HTMLファイルはテキストとして返します。
            if (Path.GetExtension(file) == ".html")
                return new ContentResult() { Content = System.IO.File.ReadAllText(filePath), ContentType = "text/html" };

            // 画像やCSS、JavaScriptなどのバイナリファイルをバイト列として読み込みます。
            var resFile = System.IO.File.ReadAllBytes(filePath);

            // faviconはブラウザーが認識できるMIMEタイプを明示して返します。
            if (Path.GetExtension(file) == ".ico")
                return new FileContentResult(resFile, "image/x-icon") { FileDownloadName = file };

            // 拡張子に応じたMIMEタイプを設定してファイルを返します。
            return new FileContentResult(resFile, GetType(file)) { FileDownloadName = file };
        }

        // Viewer用リソースの拡張子から、レスポンスのMIMEタイプを判定します。
        private string GetType(string file)
        {
            if (file.EndsWith(".css"))
                return "text/css";

            if (file.EndsWith(".js"))
                return "text/javascript";

            return "text/html";
        }

        // ReportsフォルダーにあるActiveReportsのレポート定義一覧を返します。
        [HttpGet("reports")]
        public ActionResult Reports()
        {
            // Viewerで扱えるレポート定義の拡張子だけを対象にします。
            string[] validExtensions = { ".rdl", ".rdlx", ".rdlx-master" };

            // 実行ファイルの配置場所を基準にReportsフォルダーを参照します。
            string pathToReport = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            var reportsList = Directory.GetFiles(pathToReport);

            // 対象拡張子のファイルだけを抽出し、JSONとして返します。
            return new ObjectResult(reportsList
                .Where(x => validExtensions.Any(ext => x.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)))
                .Select(x => x)
                .ToArray());
        }

        // 業務支援システムの帳票1画面を表示します。
        [HttpGet("report1")]
        public IActionResult Report1()
        {
            return View("Report1");
        }

    }
}
