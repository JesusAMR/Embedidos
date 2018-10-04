using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace test.Util
{
    public static class Utilerias
    {
        public static string findKeyValueinJSon(string json, string key, int valueIndex)
        {
            string status, value;
            JObject jObject = JObject.Parse(json);
            value = jObject[key].ToString();
            return value;
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