using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using test.Controllers;

namespace test.Util
{
    public static class Utilerias
    {
        public static string findKeyValueinJSon(string json, string key, int valueIndex)
        {
            string value;
            JObject jObject = JObject.Parse(json);
            value = jObject[key].ToString();
            return value;
        }

        public static void insertarRegistro(string candidato)
        {
            StringBuilder lsbQuery = new StringBuilder();
            lsbQuery.AppendFormat("insert into registro() values({0}, {1})", candidato, 0);
            Conneccion.Execute(lsbQuery);
        }
    }
    public class Candidato
    {
        public int? personGroupId { get; set; }
        public string[] faceIds { get; set; }
        public int maxNumOfCandidatesReturned { get; set; }
        public double? confidenceThreshold { get; set; }

    }
}