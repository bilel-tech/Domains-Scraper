using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Domains_Scraper.Models
{
    public class HttpCaller
    {
        HttpClient _httpClient;
        public string CsrfToken { get; set; }
        readonly HttpClientHandler _httpClientHandler = new HttpClientHandler()
        {
            CookieContainer = new CookieContainer(),
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        public HttpCaller()
        {
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36");
        }
        public async Task<HtmlAgilityPack.HtmlDocument> GetDoc(string url, int maxAttempts = 1)
        {
            var html = await GetHtml(url, maxAttempts);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }
        public async Task<string> GetHtml(string url, int maxAttempts = 1)
        {
            int tries = 0;
            //_httpClient.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");
            //_httpClient.DefaultRequestHeaders.Add("sec-fetch-user", "?1");
            do
            {
                try
                {
                    var response = await _httpClient.GetAsync(url);
                    string html = await response.Content.ReadAsStringAsync();
                    //_httpClient.DefaultRequestHeaders.Remove("upgrade-insecure-requests");
                    return html;
                }
                catch (WebException ex)
                {
                    var errorMessage = "";
                    try
                    {
                        errorMessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception)
                    {
                    }
                    tries++;
                    if (tries == maxAttempts)
                    {
                        throw new Exception(ex.Status + " " + ex.Message + " " + errorMessage);
                    }
                    await Task.Delay(2000);
                }
            } while (true);
        }
        public async Task<string> PostJson(string url, string json, int maxAttempts = 1)
        {
            int tries = 0;
            var s = "";
            do
            {
                try
                {
                    if (json!=null)
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        // content.Headers.Add("x-appeagle-authentication", Token);
                        var r = await _httpClient.PostAsync(url, content);
                        s = await r.Content.ReadAsStringAsync(); 
                    }
                    else
                    {
                        var r = await _httpClient.PostAsync(url, null);
                        s = await r.Content.ReadAsStringAsync();
                    }
                    return s;
                }
                catch (WebException ex)
                {
                    var errorMessage = "";
                    try
                    {
                        errorMessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception)
                    {
                    }
                    tries++;
                    if (tries == maxAttempts)
                    {
                        throw new Exception(ex.Status + " " + ex.Message + " " + errorMessage);
                    }
                    await Task.Delay(2000);
                }
            } while (true);

        }
        public async Task<string> PostFormData(string url, List<KeyValuePair<string, string>> formData, int maxAttempts = 1)
        {
            var formContent = new FormUrlEncodedContent(formData);
            int tries = 0;
            do
            {
                try
                {
                    var response = await _httpClient.PostAsync(url, formContent);
                    string html = await response.Content.ReadAsStringAsync();
                    return html;
                }
                catch (WebException ex)
                {
                    var errorMessage = "";
                    try
                    {
                        errorMessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception)
                    {
                    }
                    tries++;
                    if (tries == maxAttempts)
                    {
                        throw new Exception(ex.Status + " " + ex.Message + " " + errorMessage);
                    }
                    await Task.Delay(2000);
                }
            } while (true);
        }
        public async Task<string> GetHtmlAhref(string url, int maxAttempts = 1)
        {
            int tries = 0;
            do
            {
                try
                {
                    var request = new HttpRequestMessage();
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri(url);
                    request.Headers.Add("x-csrf-token", CsrfToken);
                    request.Headers.Add("x-requested-with", "XMLHttpRequest");
                    var response = await _httpClient.SendAsync(request);
                    string html = await response.Content.ReadAsStringAsync();
                    return html;
                }
                catch (WebException ex)
                {
                    var errorMessage = "";
                    try
                    {
                        errorMessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    }
                    catch (Exception)
                    {
                    }
                    tries++;
                    if (tries == maxAttempts)
                    {
                        throw new Exception(ex.Status + " " + ex.Message + " " + errorMessage);
                    }
                    await Task.Delay(2000);
                }
            } while (true);
        }
    }
}
