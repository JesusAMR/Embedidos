using System;
using System.Collections;
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
		
		public static string findKeyValueinJSon(JObject jObject, string key)
		{
			foreach (var jToken in jObject)
			{
				if (jToken.Key.ToString().Equals(key)) { 
					if(jToken.Value.HasValues)
						return findKeyValueinJSon((JObject)jToken.Value.First, key);
					return jToken.Value.ToString(); 
				}
				else if (jToken.Value.HasValues)
					return findKeyValueinJSon((JObject)jToken.Value.First, key);
			}
			return null;
		}
		public static string findKeyValueinJSon(string json, string key, int valueIndex)
		{
			JObject jObject = JObject.Parse(json);
			if (jObject != null)
				return findKeyValueinJSon(jObject, key);
			else
				return null;
		}

		public static void insertarRegistro(string candidato)
		{
			StringBuilder lsbQuery = new StringBuilder();
			lsbQuery.AppendFormat("insert into registro() values({0}, {1})", candidato, 0);
			Conneccion.Execute(lsbQuery);
		}
		public static int? findKey(string jsonKey, Dictionary<string, int> valores)
		{
			if (valores.ContainsKey(jsonKey))
				return valores[jsonKey];
			else
				return null;
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