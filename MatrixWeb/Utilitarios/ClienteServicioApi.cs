using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilitarios.Servicios
{
        public class ClienteServicioApi
        {     

            public  async Task Post<T>(string UrlPrincipal,string url, T contentValue)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlPrincipal);
                    var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(url, content);
                    result.EnsureSuccessStatusCode();
                }
            }

            public  async Task Put<T>(string UrlPrincipal, string url, T stringValue)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlPrincipal);
                    var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8, "application/json");
                    var result = await client.PutAsync(url, content);
                    result.EnsureSuccessStatusCode();
                }
            }

            public  async Task<T> Get<T>(string UrlPrincipal, string url)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlPrincipal);
                    var result = await client.GetAsync(url);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                    return resultContent;
                }
            }

            public  async Task Delete(string UrlPrincipal, string url)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlPrincipal);
                    var result = await client.DeleteAsync(url);
                    result.EnsureSuccessStatusCode();
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