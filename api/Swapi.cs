using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api
{

    public class ApiSw
    {
        public class Character
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("height")]
            public string Height { get; set; }
            [JsonPropertyName("birth_year")]
            public string BirthDate { get; set; }
            [JsonPropertyName("gender")]
            public string Gender { get; set; }

            [JsonPropertyName("skin_color")]
            public string SkinColor { get; set; }
        }

        public class ApiResponse
        {
            [JsonPropertyName("results")]
            public Character[] Results { get; set; }

            [JsonPropertyName("next")]
            public string Next { get; set; }
        }
        //Metodo para obtener los datos especificos de el personaje elegido a traves de una api que consta de varias paginas
        public static async Task<Character> ApiStarWars(string nombre)
        {
            string urlBase = "https://swapi.dev/api/people/?page=";
            int pagina = 1;

            using (HttpClient client = new HttpClient())
            {
                while (true)
                {
                    // Construir la URL para la página actual
                    string url = urlBase + pagina;

                    // Hacer la solicitud HTTP
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Leer la respuesta como cadena
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserializar la respuesta JSON
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    ApiResponse json = JsonSerializer.Deserialize<ApiResponse>(responseBody, options);

                    // Buscar el personaje en la página actual
                    foreach (var resultado in json.Results)
                    {
                        if (resultado.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                        {
                            return resultado;
                        }
                    }

                    // Verificar si hay más páginas y si no hay mas paginas, sale del bucle
                    if (string.IsNullOrEmpty(json.Next))
                    {
                        break;
                    }

                    pagina++;
                }
            }

            return null; // Personaje no encontrado
        }
    }
}