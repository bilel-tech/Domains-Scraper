using Domains_Scraper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Services
{
    static public class AhrefService
    {
        private static HttpCaller _httpCaller = new HttpCaller();
        private static string _csHash;
        public static async Task LogIn()
        {
            //var json = "{\"remember_me\":true,\"auth\":{\"password\":\"5e\\\\.Xq5<(-QK.}r7\",\"login\":\"mei@alw.org.uk\"}}"; 5raaaaaa
            var jsonObj = new
            {
                remember_me = true,
                auth = new
                {
                    password = "temp@HOC123",
                    login = "shahid@houseofcomms.com"
                }
            };
            var json = JsonConvert.SerializeObject(jsonObj);
            var login = await _httpCaller.PostJson("https://auth.ahrefs.com/auth/login", json);
            await GeCshToken();

        }
        public static async Task GetData(List<string> domains, int delay)
        {
            await LogIn();
            var ahrefDomains = new List<AhrefDomain>();
            for (int i = 0; i < domains.Count; i++)
            {
                var ahrefDomain = await StartScraping(domains[i]);
                if (ahrefDomain != null)
                {
                    ahrefDomains.Add(ahrefDomain);
                }
                Reporter.Progress(i + 1, domains.Count, $@"Domain Scraped from app.ahrefs.com {i + 1}/{domains.Count}");
                await Task.Delay(delay);
            }
        }

        public static async Task<AhrefDomain> StartScraping(string domainName)
        {

            var ahrefDomain = new AhrefDomain();
            var csHashAndUr = await GetCsHashAndUr(domainName);
            _csHash = csHashAndUr.csHash;
            //ahrefDomain.Ur = csHashAndUr.Ur;
            //ahrefDomain.Dr = await GetDr();
            //var backLinks = await GetBacklinksData(domainName);
            //ahrefDomain.BacklinksType = backLinks.backlinksType;
            //ahrefDomain.TotalBacklinks = backLinks.totalBacklinks;
            //var referringDomains = await GetReferringDomainsData(domainName);
            //ahrefDomain.ReferringDomainsTypes = referringDomains.refDomains;
            //ahrefDomain.TotalReferringDomains = referringDomains.totalReferringDomains;
            //ahrefDomain.OrganicCharts = await GetOrganicDataData(domainName);
            //ahrefDomain.OrganicTraffic = ahrefDomain.OrganicCharts.Last().OrganicTrafficTotal;
            //ahrefDomain.OrganicKeyWords = ahrefDomain.OrganicCharts.Last().OrganicKeyWordsTotal;
            return ahrefDomain;
        }

        private static async Task<(List<BacklinksType> backlinksType, long totalBacklinks)> GetBacklinksData(string domainName)
        {
            var backlinksType = new List<BacklinksType>();
            var json = await _httpCaller.GetHtmlAhref("https://app.ahrefs.com/site-explorer/ajax/overview/backlinks-stats/" + _csHash);
            var obj = JObject.Parse(json);
            var totalBacklinks = (int)obj.SelectToken("total_backlinks");
            var BacklinksTypes = obj.SelectToken("BacklinksTypes");

            foreach (var BacklinksType in BacklinksTypes)
            {
                var bltp = new BacklinksType();
                bltp.Title = (string)BacklinksType.SelectToken("..title");
                bltp.Value = (long)BacklinksType.SelectToken("..backlinks");
                bltp.Percentage = (double)BacklinksType.SelectToken("..percentage");
                backlinksType.Add(bltp);
            }
            return (backlinksType, totalBacklinks);
        }
        private static async Task<List<OrganicChart>> GetOrganicDataData(string domainName)
        {
            var organicCharts = new List<OrganicChart>();
            var formData = new
            {
                mode = "subdomains",
                url = domainName
            };
            var jsonFormat = JsonConvert.SerializeObject(formData);
            var json = await _httpCaller.PostJson("https://app.ahrefs.com/v4/seGetOrganicChartDataPhpCompat?pretty=1", jsonFormat);
            var objt = JArray.Parse(json);

            foreach (var token in objt[1])
            {
                var organicChart = new OrganicChart();

                organicChart.Date = DateTime.Parse((string)token.SelectToken("date"));
                organicChart.OrganicTrafficTotal = (long)token.SelectToken("traffic");
                organicChart.OrganicKeyWordsTotal = (long)token.SelectToken("position");
                organicChart.Position_1_3 = (long)token.SelectToken("position_1_3");
                organicChart.Position_4_10 = (long)token.SelectToken("position_4_10");
                organicChart.Position_11_100 = organicChart.OrganicKeyWordsTotal - (organicChart.Position_1_3 + organicChart.Position_4_10);
                organicCharts.Add(organicChart);
            }
            return organicCharts;
        }
        private static async Task<(List<BacklinksType> refDomains, long totalReferringDomains)> GetReferringDomainsData(string domainName)
        {
            var referringDomains = new List<BacklinksType>();
            var json = await _httpCaller.GetHtmlAhref("https://app.ahrefs.com/site-explorer/ajax/overview/referring-domains-stats/" + _csHash);
            var obj = JObject.Parse(json);
            var totalReferringDomains = (long)obj.SelectToken("total_referring_domains");
            var refD = new BacklinksType();
            refD.Title = "Dofollow";
            refD.Value = (long)obj.SelectToken("..referring_domains_dofollow");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_percentage_dofollow");
            referringDomains.Add(refD);
            refD = new BacklinksType();
            refD.Title = "Governmental";
            refD.Value = (long)obj.SelectToken("..referring_domains_governmental");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_percentage_governmental");
            referringDomains.Add(refD);
            refD = new BacklinksType();
            refD.Title = "Educational";
            refD.Value = (long)obj.SelectToken("..referring_domains_educational");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_percentage_educational");
            referringDomains.Add(refD);
            refD = new BacklinksType();
            refD.Title = "gov";
            refD.Value = (long)obj.SelectToken("..referring_domains_gov");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_percentage_gov");
            referringDomains.Add(refD);
            refD = new BacklinksType();
            refD.Title = "edu";
            refD.Value = (long)obj.SelectToken("..referring_domains_edu");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_edu");
            referringDomains.Add(refD);
            refD = new BacklinksType();
            refD.Title = "com";
            refD.Value = (long)obj.SelectToken("..referring_domains_com");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_com");
            referringDomains.Add(refD);
            refD = new BacklinksType();
            refD.Title = "net";
            refD.Value = (long)obj.SelectToken("..referring_domains_net");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_net");
            referringDomains.Add(refD);
            refD = new BacklinksType();
            refD.Title = "org";
            refD.Value = (long)obj.SelectToken("..referring_domains_org");
            refD.Percentage = (double)obj.SelectToken("..referring_domains_org");
            referringDomains.Add(refD);
            return (referringDomains, totalReferringDomains);
        }
        private static async Task GeCshToken()
        {
            var html = await _httpCaller.GetHtml("https://app.ahrefs.com/site-explorer/overview/v2/subdomains/live?target=upwork.com");
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var token = doc.DocumentNode.SelectSingleNode("//meta[@name='_token']").GetAttributeValue("content", "");
            _httpCaller.CsrfToken = token;
        }

        private static async Task<int> GetDr()
        {
            var json = await _httpCaller.GetHtmlAhref("https://app.ahrefs.com/site-explorer/ajax/overview/domain-rating/" + _csHash);
            var obj = JObject.Parse(json);
            var dr = (int)obj.SelectToken("domain_rating");
            return dr;
        }

        private static async Task<(int Ur, string csHash)> GetCsHashAndUr(string domain)
        {
            var html = await _httpCaller.GetHtml($"https://app.ahrefs.com/site-explorer/overview/v2/subdomains/live?target={domain}", 3);
            var csHash = html.Substring(html.IndexOf("CSHash = ", StringComparison.Ordinal) + 10);
            var x = csHash.IndexOf(";", StringComparison.Ordinal);
            csHash = csHash.Substring(0, x - 1);
            var UrString = html.Substring(html.IndexOf("ahrefs_rank = ", StringComparison.Ordinal) + 15);
            x = UrString.IndexOf(";", StringComparison.Ordinal);
            var Ur = int.Parse(UrString.Substring(0, x - 1));
            return (Ur, csHash);
        }
    }
}
