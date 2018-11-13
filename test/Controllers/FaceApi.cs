using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http.Headers;
using test.Util;
using Newtonsoft.Json;
using System.Text;
using System.Web.Http;

namespace test.Controllers
{
    public static class FaceApi
    {
        private static Dictionary<string, int> Candidatos = new Dictionary<string, int>()
		{
			{ "a44b483c-addd-4e93-a3c5-53868d7daa78", 1}//Jesus
            
        };

        private const int grupoPersonas = 2;
        private const string faceApiKey = "";
		private const string baseUrl = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/";

        public static async Task<string> MakeAnalysis(Byte[] file)
        {
            try
            {
                StringBuilder lsbQuery = new StringBuilder();
				int? id;

                if (file == null)
                    throw new ArgumentNullException("Archivo vacio");
                string idCara, json, idCandidato;

                json = await MakeAnalysisRequest(file);
                idCara = Utilerias.findKeyValueinJSon(json, "faceId", 0);

                if (idCara == null)
                    throw new ArgumentNullException("No se encontro cara");

                json = await IdentifyImagePersonGroup(idCara, grupoPersonas, null);
                idCandidato = Utilerias.findKeyValueinJSon(json, "personId", 1);
				if(idCandidato == null)
				{
					lsbQuery.AppendFormat("insert into registros(bValido) values(1)");
					Conneccion.Execute(lsbQuery);
					return null;
				}
				id = Utilerias.findKey(idCandidato, Candidatos);
				if(id != null)
				{
					lsbQuery.AppendFormat("insert into registros(iCodUsuario, bValido) values({0}, {1})", id, 0);
					Conneccion.Execute(lsbQuery);
				}
                else
                {
                    lsbQuery.AppendFormat("insert into registros(bValido) values({0})", 1);
                    Conneccion.Execute(lsbQuery);
                }
                return idCandidato;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
 
        private static async Task<string> IdentifyImagePersonGroup(string faceID, int? personGroupID, double? confidenceThreshold)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response;
                NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
                Candidato candidato = new Candidato();
                string responseContent;

                var uri = baseUrl + "identify?" + queryString;

                if (personGroupID == null)
                    personGroupID = grupoPersonas;
                if (confidenceThreshold == null)
                    confidenceThreshold = 0.6;

                candidato.faceIds = new string[] { faceID };
                candidato.confidenceThreshold = confidenceThreshold;
                candidato.maxNumOfCandidatesReturned = 1;
                candidato.personGroupId = personGroupID;

                string output = JsonConvert.SerializeObject(candidato);

                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", faceApiKey);
                byte[] byteData = Encoding.UTF8.GetBytes(output);

                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = await client.PostAsync(uri, content);
                }

                responseContent = await response.Content.ReadAsStringAsync();
				return responseContent.Trim('[', ']');

			}
            catch (Exception e)
            {
                throw e;
            }


        }

        private static async Task<string> MakeAnalysisRequest(Byte[] file)
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
				var url = baseUrl + "detect?" + queryString;

				HttpResponseMessage response;

				byte[] byteData = file;

				using (ByteArrayContent content = new ByteArrayContent(byteData))
				{
					content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
					content.Headers.ContentLength = byteData.Length;

					response = await client.PostAsync(url, content);
				}

				string contentString = await response.Content.ReadAsStringAsync();
				
				//Regresa JSON
				return contentString.Trim('[', ']');

			}
            catch (Exception e)
            {

                throw e;
            }
        }
        /*
        private static byte[] GetBytesFromHttpFile(Byte[] file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                BinaryReader binaryReader = new BinaryReader(ms);
				binaryReader.BaseStream.Position = 0;
				return binaryReader.ReadBytes((int)file.InputStream.Length);
            }
        }*/
    }
}