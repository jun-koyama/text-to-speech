using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TextToSpeech
{
    class TextToSpeechClient
    {
        private static readonly string EndPointUrl = "https://api.apigw.smt.docomo.ne.jp/aiTalk/v1/textToSpeech?APIKEY=";

        private static readonly string ApiKey = "XXXXXXXX";

        private static readonly string ContentType = "application/ssml+xml";

        private static readonly string Accept = "audio/L16";

        public static byte[] SendRequest(Ssml ssml)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var responseMessage = client.SendAsync(GetRequest(ssml));
                    responseMessage.Wait();
                    if (responseMessage.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = responseMessage.Result.Content;
                        var stream = content.ReadAsStreamAsync().GetAwaiter().GetResult();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            byte[] buf = new byte[1024 * 1024]; // 1m
                            while (true)
                            {
                                int read = stream.Read(buf, 0, buf.Length);
                                if (read <= 0)
                                {
                                    break;
                                }
                                ms.Write(buf, 0, buf.Length);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {

                }
            }
            return null;
        }

        private static HttpRequestMessage GetRequest(Ssml ssml)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, EndPointUrl + ApiKey);
            request.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(ssml.ToString()));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Accept));
            return request;
        }
    }
}
