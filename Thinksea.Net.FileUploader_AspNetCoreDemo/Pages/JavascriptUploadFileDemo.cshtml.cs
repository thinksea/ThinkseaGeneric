using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Thinksea.Net.FileUploader_AspNetCoreDemo.Pages
{
    public class JavascriptUploadFileDemoModel : PageModel
    {
        private readonly ILogger<JavascriptUploadFileDemoModel> _logger;

        public JavascriptUploadFileDemoModel(ILogger<JavascriptUploadFileDemoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
