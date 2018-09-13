using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Text;

namespace test.Controllers
{
    public static class FaceApi
    {
		private const string faceApiKey = "";

		private const string baseUrl = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/detect?";

		public static async Task<string> MakeAnalysisRequest(HttpPostedFileBase file)
        {
			try
			{
				HttpClient client = new HttpClient();
				NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

				//Encabezados
				client.DefaultRequestHeaders.Add(
					"Ocp-Apim-Subscription-Key", faceApiKey);

				queryString["returnFaceId"] = "true";
				queryString["returnFaceLandmarks"] = "false";
				queryString["returnFaceAttributes"] = "gender";
				var url = baseUrl + queryString;

				HttpResponseMessage response;

				byte[] byteData = GetBytesFromHttpFile(file);

				using (ByteArrayContent content = new ByteArrayContent(byteData))
				{
					content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
					content.Headers.ContentLength = byteData.Length;

					response = await client.PostAsync(url, content);
				}

				string contentString = await response.Content.ReadAsStringAsync();
				
				//Regresa JSON
				return contentString;

			}
            catch (Exception e)
            {

                throw e;
            }
        }

        public static byte[] GetBytesFromHttpFile(HttpPostedFileBase file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                BinaryReader binaryReader = new BinaryReader(ms);
				binaryReader.BaseStream.Position = 0;
				return binaryReader.ReadBytes((int)file.InputStream.Length);
            }
        }
    }
}