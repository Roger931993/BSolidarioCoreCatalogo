using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Core.Catalog.Shared.Helpers
{
    public static class Util
    {
        public static async Task<T> ConvertResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            string data = string.Empty;
            int codeerror = 10000;
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                string strResult = await httpResponseMessage.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(strResult))
                    throw new HttpRequestException($"{(int)httpResponseMessage.StatusCode}|{httpResponseMessage}-{strResult}");
            }
            JsonObject jsonObject = JsonNode.Parse(await httpResponseMessage.Content.ReadAsStringAsync())!.AsObject();
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                data = jsonObject.FirstOrDefault(x => x.Key == "Data").Value?.ToJsonString()!;
            else
                codeerror = (int)jsonObject["error"]!;

            return await ConvertObject<T>(JsonSerializer.Serialize((object)new
            {
                codeError = codeerror,
                Data = data
            }, (JsonSerializerOptions?)null));
        }

        public static async Task<T> ConvertObject<T>(string dataJson)
        {
            string dataJson2 = dataJson;
            return await Task.Run(() => JsonSerializer.Deserialize<T>(dataJson2)!);
        }
    }
}
