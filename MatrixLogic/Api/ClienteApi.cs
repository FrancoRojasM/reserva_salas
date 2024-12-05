using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace ApiLogic
{
    public class ClienteApi 
    {
        //
        // Resumen:
        //     Ejecuta un HttpClient
        //
        // Parámetros:
        //   url:
        //     url que contiene el web service o api.
        //   ParametroEnJson:
        //      el parametro debe ser enviado en formato Json Ejemplo:{"usuario":"usuario"}.
        //
        // Devuelve:
        //     Objeto un objeto dinámico.   
        public async Task<dynamic> EjecutarDataDinamicaPostAsync(string url, object parametro = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["HostMatrixApi"]);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["ClaveMatrixApi"]);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var parametroExtraEnjson = JsonConvert.SerializeObject(parametro);
                var content = new StringContent(parametroExtraEnjson, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(resultContentString, new ExpandoObjectConverter());
                return data;
            }
        }
        //public async Task<T> EjecutarDataDinamicaPostAsync<T>(string url, object parametro = null)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["HostMatrixApi"]);
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["ClaveMatrixApi"]);
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        var parametroExtraEnjson = JsonConvert.SerializeObject(parametro);
        //        var content = new StringContent(parametroExtraEnjson, Encoding.UTF8, "application/json");
        //        var result = await client.PostAsync(url, content);
        //        result.EnsureSuccessStatusCode();
        //        string resultContentString = await result.Content.ReadAsStringAsync();
        //       return JsonConvert.DeserializeObject<T>(resultContentString, new ExpandoObjectConverter());
        //      //  return data;
        //    }
        //}
        public async Task<dynamic> TraerDataDinamicaGetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["HostMatrixApi"]);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["ClaveMatrixApi"]);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(resultContentString, new ExpandoObjectConverter());
                return data;
            }
        }     
    }
}


/*
 
 Para publicar usa algo como esto:

await HttpHelper.Post<Setting>($"/api/values/{id}", setting);

Ejemplo para eliminar:
await HttpHelper.Delete($"/api/values/{id}");

Ejemplo para obtener la lista:
List<ClaimTerm> claimTerms = await HttpHelper.Get<List<ClaimTerm>>("/api/values/");

Ejemplo para obtener solo uno:
ClaimTerm processedClaimImage = await HttpHelper.Get<ClaimTerm>($"/api/values/{id}");

*/