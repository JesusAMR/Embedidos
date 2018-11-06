using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Consultar()
        {
			StringBuilder lsbQuery = new StringBuilder();
			DataTable valores = new DataTable();
			lsbQuery.Append("select r.iCodUsuario, r.vchNombre, u.dtFecha, r.bValido from registros r inner join usuarios u on r.iCodUsuario = u.iCodUsuario");
			ViewBag.valores = Conneccion.ExecuteDataTable(lsbQuery);
			return View();
		}
		/*
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                byte[] array;
                StringBuilder lsbQuery = new StringBuilder();
                
                // file is uploaded

                // save the image path path to the database or you can send image
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                    lsbQuery.Append(@"insert into imagenes(vbImagen) values (convert(varbinary, '");
                    foreach(var value in array)
                    {
                        lsbQuery.Append(value);
                    }
                    lsbQuery.Append(@" '))");
                }
                
                
                Conneccion.Execute(lsbQuery);
            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index", "Home");
        }
		*/
        public ActionResult ConsultarImagenes()
        {
            StringBuilder lsbQuery = new StringBuilder();
            DataTable valores = new DataTable();
            lsbQuery.Append("select * from registros r inner join usuarios u on r.iCodUsuario = u.iCodUsuario");
            ViewBag.valores = Conneccion.ExecuteDataTable(lsbQuery);
            return View();
        }
    }
}