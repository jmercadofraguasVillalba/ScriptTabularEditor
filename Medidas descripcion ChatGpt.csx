using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    
       
        // Ingresa aquí tu clave de API de OpenAI
        string apiKey = "tu_clave_de_api";
        
        // Ingresa la fórmula DAX que deseas describir
        string formulaDax = "TUS FORMULAS DAX AQUÍ";

        // Llamada a la API de OpenAI
        string response = await GetChatResponse(formulaDax, apiKey);

        // Procesamiento de la respuesta
        string description = ParseChatResponse(response);

        // Imprimir la descripción
        Console.WriteLine("Descripción de la fórmula DAX:");
        Console.WriteLine(description);
    }

    static async Task<string> GetChatResponse(string query, string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            string apiUrl = "https://api.openai.com/v1/engines/davinci-codex/completions";
            string prompt = "DAX Formula: " + query + "\nDescription:";

            var requestBody = new
            {
                prompt = prompt,
                max_tokens = 100,
                temperature = 0.7
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }

    static string ParseChatResponse(string response)
    {
        dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        string description = data.choices[0].text;

        return description;
    }
}