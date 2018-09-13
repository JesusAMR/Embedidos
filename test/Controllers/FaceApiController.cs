using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;


namespace test.Controllers
{
    public class FaceApiController : Controller
    {
		// GET: FaceApi
		public ActionResult ConsultarCara()
		{
			return View();
		}

		[HttpPost]
        public async Task<ActionResult> FileUpload(HttpPostedFileBase file)
        {
			if (file != null)
			{
				ViewBag.valores = await FaceApi.MakeAnalysisRequest(file);
				return View("ConsultarCara");
			}
			else
				return View("Index");
        }
    }
}