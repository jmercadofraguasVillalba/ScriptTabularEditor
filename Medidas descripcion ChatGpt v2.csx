using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

// You need to signin to https://platform.openai.com/ and create an API key for your profile then paste that key 
// into the apiKey constant below
const string apiKey = "<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";
const string uri = "https://api.openai.com/v1/completions";
const string question = "Explique el siguiente cálculo en unas pocas oraciones en términos comerciales simples sin usar nombres de funciones DAX:\n\n";

using (var client = new HttpClient()) {
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

    foreach (var t in Model.Tables)
    {
        foreach ( var m in t.Measures)
        {
            // Only uncomment the following when running from the command line or the script will 
            // show a popup after each measure
            //Info("Processing " + m.DaxObjectFullName) 
            //var body = new requestBody() { prompt = question + m.Expression   };
            var body = 
                "{ \"prompt\": " + JsonConvert.SerializeObject( question + m.Expression ) + 
                ",\"model\": \"text-davinci-003\" " +
                ",\"temperature\": 1 " +
                ",\"max_tokens\": 2048 " +
                ",\"stop\": \".\" }";

            var res = client.PostAsync(uri, new StringContent(body, Encoding.UTF8,"application/json"));
            res.Result.EnsureSuccessStatusCode();
            var result = res.Result.Content.ReadAsStringAsync().Result;
            var obj = JObject.Parse(result);
            var desc = obj["choices"][0]["text"].ToString().Trim();
            m.Description = desc + "\n=====\n" + m.Expression;
        }

    }
}