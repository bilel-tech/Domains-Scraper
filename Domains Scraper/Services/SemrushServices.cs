using Domains_Scraper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Domains_Scraper.Services
{
    static public class SemrushServices
    {
        public static int _userId;
        public static string _apiKey;
        public static List<string> _domains;
        public static HttpCaller HttpCaller = new HttpCaller();
        private static List<SemrushDomain> SemrushDomains = new List<SemrushDomain>();
        public async static Task<List<SemrushDomain>> GetData(List<string> domains)
        {
            Reporter.Log("Start scraping domains from semrush.com");
            //var tpl = new TransformBlock<string, SemrushDomain>(async x => await StartScraping(x).ConfigureAwait(false), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 });
            //foreach (var domain in _domains)
            //{
            //    tpl.Post(domain);
            //}

            for (int i = 0; i < domains.Count; i++)
            {
                var domainSemRush = await StartScraping(domains[i]);
                if (domainSemRush != null)
                {
                    SemrushDomains.Add(domainSemRush);
                }
                Reporter.Progress(i + 1, domains.Count, $@"Domain Scraped from semrush.com {i + 1}/{domains.Count}");
                //await 
            }
            return SemrushDomains;
        }

        public async static Task<SemrushDomain> StartScraping(string domainName)
        {
            var domain = new SemrushDomain();
            domain.Name = domainName;
            domain.OrganicData = await GetOrganicData(domainName);
            var authorityAndBacklinksScore = await GetAuthrityAndBacklinksScore(domainName);
            domain.AuthorityScore = authorityAndBacklinksScore.authorityScore;
            domain.Backlinks = authorityAndBacklinksScore.backlinks;
            var followVsNotFollowLinksAndBacklinkType = await GetfollowVsNotFollowLinksAndBacklinkType(domainName);
            domain.FollowLinksVsNotFollowLink = followVsNotFollowLinksAndBacklinkType.fVsNotF;
            domain.BacklinkType = followVsNotFollowLinksAndBacklinkType.backlinkType;
            //var json = JsonConvert.SerializeObject(domain, Formatting.Indented);
            //File.WriteAllText("SemrushDomains.txt", json);
            return domain;

        }

        public async static Task<(FollowLinksVsNoFollowLink fVsNotF, BacklinkType backlinkType)> GetfollowVsNotFollowLinksAndBacklinkType(string domainName)
        {
            try
            {
                var random = new Random();


                var r1 = random.Next(0, 9);
                var r2 = random.Next(0, 9);
                var r3 = random.Next(0, 9);
                var r4 = random.Next(0, 9);

                var fVsNotF = new FollowLinksVsNoFollowLink();
                var backlinkType = new BacklinkType();

                var request_id = "63" + r4 + "3b0fb-bcc7-4b6" + r3 + "-a035-5" + r2 + "f0b" + r1 + "62eba2";
                //{\"id\":7,\"jsonrpc\":\"2.0\",\"method\":\"backlinks.Overview\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\"},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}
                var jsonFormData = "{\"id\":7,\"jsonrpc\":\"2.0\",\"method\":\"backlinks.Overview\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\"},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}";
                var url = "https://www.semrush.com/dpa/rpc";
                var json = await HttpCaller.PostJson(url, jsonFormData);
                var obj = JObject.Parse(json);

                fVsNotF.FollowLinks = (long)obj.SelectToken(".result.follow");
                fVsNotF.NotFollowLinks = (long)obj.SelectToken(".result.nofollow");
                backlinkType.TextLinks = (long)obj.SelectToken(".result.texts");
                backlinkType.FrameLinks = (long)obj.SelectToken(".result.frames");
                backlinkType.FormLinks = (long)obj.SelectToken(".result.forms");
                backlinkType.ImageLinks = (long)obj.SelectToken(".result.images");
                return (fVsNotF, backlinkType);
            }
            catch (Exception e)
            {
                Reporter.Error($"Error on getting Authority score {domainName} : {e.Message}");
                return (null, null);
            }
        }

        public async static Task<List<OrganicTrafficAndKeywordsByCountry>> GetOrganicTrafficAndKeywordsByCountry(string domainName)
        {
            var random = new Random();

            var r1 = random.Next(0, 9);
            var r2 = random.Next(0, 9);
            var r3 = random.Next(0, 9);
            var r4 = random.Next(0, 9);

            var organicTrafficAndKeywordsByCountries = new List<OrganicTrafficAndKeywordsByCountry>();
            try
            {
                var request_id = "63" + r4 + "3b0fb-bcc7-4b6" + r3 + "-a035-5" + r2 + "f0b" + r1 + "62eba2";
                var jsonFormData = "{\"id\":6,\"jsonrpc\":\"2.0\",\"method\":\"organic.Summary\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\",\"dateType\":\"daily\",\"dateFormat\":\"date\"},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}";
                var url = "https://www.semrush.com/dpa/rpc";
                var json = await HttpCaller.PostJson(url, jsonFormData);
                var obj = JObject.Parse(json);
                var results = obj.SelectToken("result");
                foreach (var result in results)
                {
                    var country = (string)result.SelectToken("database");
                    if (country.Contains("mobile"))
                    {
                        continue;
                    }
                    var traffic = (int)result.SelectToken("organicTraffic");
                    var keyWords = (int)result.SelectToken("organicPositions");
                    organicTrafficAndKeywordsByCountries.Add(new OrganicTrafficAndKeywordsByCountry { Country = country, OranicTraficValue = traffic, KeyWordsValue = keyWords });
                }
                organicTrafficAndKeywordsByCountries = organicTrafficAndKeywordsByCountries.OrderBy(x => x.OranicTraficValue).ToList();
                return organicTrafficAndKeywordsByCountries;
            }
            catch (Exception ex)
            {

                Reporter.Error($@"Error while parsing the data to get Organic traffic and Key Words by country Datas from {domainName}" + "\r\n" + ex);
                return null;
            }

        }

        public async static Task<OrganicData> GetOrganicData(string domainName)
        {
            var organicData = new OrganicData();
            organicData.OneYearOrganicData = await GetOneYearOrganicData(domainName);
            organicData.AllTimeOrganicData = await GetAllTimeOrganicData(domainName);
            organicData.OrganicTrafficAndKeywordsByCountry = await GetOrganicTrafficAndKeywordsByCountry(domainName);
            organicData.OrganicKeywords = organicData.OneYearOrganicData.OneYearOrganicKeyWordsChartData.Last().Total;
            organicData.OrganicTraffic = organicData.OneYearOrganicData.OneYearOrganicTrafficChartData.Last().OrganicTrafficValue;
            organicData.OrganicPositionsDistrubution = await GetOrganicPositionsDistributionData(domainName);
            return organicData;
        }

        public async static Task<List<OrganicChartData>> GetOrganicPositionsDistributionData(string domainName)
        {
            var random = new Random();


            var r1 = random.Next(0, 9);
            var r2 = random.Next(0, 9);
            var r3 = random.Next(0, 9);
            var r4 = random.Next(0, 9);

            var organicPositionsDistribution = new List<OrganicChartData>();
            try
            {
                //{\"id\":16,\"jsonrpc\":\"2.0\",\"method\":\"organic.OverviewTrend\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"dateType\":\"daily\",\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\",\"dateRange\":null,\"database\":\"us\",\"global\":false},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}
                var request_id = "63" + r4 + "3b0fb-bcc7-4b6" + r3 + "-a035-5" + r2 + "f0b" + r1 + "62eba2";
                var jsonFormData = "{\"id\":16,\"jsonrpc\":\"2.0\",\"method\":\"organic.OverviewTrend\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"dateType\":\"daily\",\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\",\"dateRange\":null,\"database\":\"us\",\"global\":false},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}";
                var url = "https://www.semrush.com/dpa/rpc";
                var json = await HttpCaller.PostJson(url, jsonFormData);
                var obj = JObject.Parse(json);
                var results = obj.SelectToken("result");
                foreach (var result in results)
                {
                    var dt = (string)result.SelectToken("date");
                    dt = dt.Insert(4, "/");
                    dt = dt.Insert(7, "/");
                    var date = DateTime.Parse(dt);
                    var organicTrafficValue = (long)result.SelectToken("organicTraffic");
                    var positions = result.SelectToken("organicPositionsTrend");
                    var topThree = (long)positions[0];
                    var fourToTen = (long)positions[1];
                    var elevenToTwenty = (long)positions[2];
                    var twentyOneToFifty = (long)positions[3] + (long)positions[4] + (long)positions[5];
                    var fiftyOneToOneHundred = (long)positions[6] + (long)positions[7] + (long)positions[8] + (long)positions[9] + (long)positions[10];
                    var total = topThree + fourToTen + elevenToTwenty + twentyOneToFifty + fiftyOneToOneHundred;
                    organicPositionsDistribution.Add(new OrganicChartData { Date = date, TopThree = topThree, FourToTen = fourToTen, ElevenToTwenty = elevenToTwenty, FiftyOneToOneHundred = fiftyOneToOneHundred, Total = total });
                }
                organicPositionsDistribution = organicPositionsDistribution.OrderBy(x => x.Date).ToList();
                return organicPositionsDistribution;
            }
            catch (Exception ex)
            {

                Reporter.Error($@"Error while parsing the data to get Organic Position Distribution Datas from {domainName}" + "\r\n" + ex);
                return null;
            }
        }

        public async static Task<OneYearOrganicData> GetOneYearOrganicData(string domainName)
        {
            var random = new Random();


            var r1 = random.Next(0, 9);
            var r2 = random.Next(0, 9);
            var r3 = random.Next(0, 9);
            var r4 = random.Next(0, 9);

            try
            {
                var oneYearOrganicData = new OneYearOrganicData();
                var request_id = "63" + r4 + "3b0fb-bcc7-4b6" + r3 + "-a035-5" + r2 + "f0b" + r1 + "62eba2";

                var jsonData = "{\"id\":14,\"jsonrpc\":\"2.0\",\"method\":\"organic.OverviewTrend\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"dateType\":\"daily\",\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\",\"dateRange\":null,\"database\":\"ae\",\"global\":true},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}";
                var json = await HttpCaller.PostJson("https://www.semrush.com/dpa/rpc", jsonData);
                var obj = JObject.Parse(json);
                oneYearOrganicData.OneYearOrganicTrafficChartData = GetTrafficData(obj, domainName);
                oneYearOrganicData.OneYearOrganicKeyWordsChartData = GetKeyWordsData(obj, domainName);
                return oneYearOrganicData;
            }
            catch (Exception ex)
            {
                Reporter.Error($@"Error while parsing the data to get One year Organic Datas from {domainName}" + "\r\n" + ex);
                return null;
            }
        }
        public async static Task<AllTimeOrganicData> GetAllTimeOrganicData(string domainName)
        {
            var random = new Random();


            var r1 = random.Next(0, 9);
            var r2 = random.Next(0, 9);
            var r3 = random.Next(0, 9);
            var r4 = random.Next(0, 9);


            var allTimeOrganicData = new AllTimeOrganicData();
            try
            {
                var request_id = "63" + r4 + "3b0fb-bcc7-4b6" + r3 + "-a035-5" + r2 + "f0b" + r1 + "62eba2";
                var jsonData = "{\"id\":15,\"jsonrpc\":\"2.0\",\"method\":\"organic.OverviewTrend\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"dateType\":\"monthly\",\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\",\"dateRange\":null,\"database\":\"ae\",\"global\":true},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}";
                var json = await HttpCaller.PostJson("https://www.semrush.com/dpa/rpc", jsonData);
                var obj = JObject.Parse(json);
                allTimeOrganicData.AllTimeOrganicTrafficChartData = GetTrafficData(obj, domainName);
                allTimeOrganicData.AllTimeOrganicKeyWordsChartData = GetKeyWordsData(obj, domainName);
                return allTimeOrganicData;
            }
            catch (Exception ex)
            {
                Reporter.Error($@"Error while parsing the data to get one All tile Organic Datas from {domainName}" + "\r\n" + ex);
                return null;
            }
        }
        public static List<OrganicChartData>? GetKeyWordsData(JObject obj, string domainName)
        {
            var organicKeyWordsChartData = new List<OrganicChartData>();
            try
            {
                var results = obj.SelectToken("result");
                foreach (var result in results)
                {

                    var dt = (string)result.SelectToken("date");
                    dt = dt.Insert(4, "/");
                    dt = dt.Insert(7, "/");
                    var date = DateTime.Parse(dt);
                    var organicTrafficValue = (long)result.SelectToken("organicTraffic");
                    var positions = result.SelectToken("organicPositionsTrend");
                    var topThree = (long)positions[0];
                    var fourToTen = (long)positions[1];
                    var elevenToTwenty = (long)positions[2];
                    var twentyOneToFifty = (long)positions[3] + (long)positions[4] + (long)positions[5];
                    var fiftyOneToOneHundred = (int)positions[6] + (int)positions[7] + (int)positions[8] + (int)positions[9] + (int)positions[10];
                    var total = topThree + fourToTen + elevenToTwenty + twentyOneToFifty + fiftyOneToOneHundred;
                    organicKeyWordsChartData.Add(new OrganicChartData { Date = date, TopThree = topThree, FourToTen = fourToTen, ElevenToTwenty = elevenToTwenty, TwentyOneToFifty = twentyOneToFifty, FiftyOneToOneHundred = fiftyOneToOneHundred, Total = total });
                }
                organicKeyWordsChartData.OrderBy(x => x.Date).ToList();
                return organicKeyWordsChartData;
            }
            catch (Exception ex)
            {
                Reporter.Error($@"Error while parsing the data to get Organic Key Words Datas from {domainName}" + "\r\n" + ex);
                return null;
            }
        }

        public static List<OrganicTrafficChartData> GetTrafficData(JObject obj, string domainName)
        {
            var organicTrafficChartDatas = new List<OrganicTrafficChartData>();
            try
            {
                var results = obj.SelectToken("result");
                foreach (var result in results)
                {

                    try
                    {
                        var dt = (string)result.SelectToken("date");
                        dt = dt.Insert(4, "/");
                        dt = dt.Insert(7, "/");
                        var date = DateTime.Parse(dt);
                        var organicTrafficValue = (long)result.SelectToken("organicTraffic");
                        var paidTrafficValue = (long)result.SelectToken("adwordsTraffic");
                        organicTrafficChartDatas.Add(new OrganicTrafficChartData { Date = date, OrganicTrafficValue = organicTrafficValue, PaidTrafficValue = paidTrafficValue });
                    }
                    catch (Exception)
                    {

                        var g = (string)result.SelectToken("organicTraffic");
                    }
                }
                organicTrafficChartDatas.OrderBy(x => x.Date).ToList();
                return organicTrafficChartDatas;
            }
            catch (Exception ex)
            {
                Reporter.Error($@"Error while parsing the data to get Organic Traffic Datas from {domainName}" + "\r\n" + ex);
                return null;
            }
        }

        public async static Task<(long authorityScore, long backlinks)> GetAuthrityAndBacklinksScore(string domainName)
        {
            try
            {
                var random = new Random();


                var r1 = random.Next(0, 9);
                var r2 = random.Next(0, 9);
                var r3 = random.Next(0, 9);
                var r4 = random.Next(0, 9);

                var request_id = "63" + r4 + "3b0fb-bcc7-4b6" + r3 + "-a035-5" + r2 + "f0b" + r1 + "62eba2";

                var jsonFormData = "{\"id\":21,\"jsonrpc\":\"2.0\",\"method\":\"backlinks.Summary\",\"params\":{\"request_id\":\"" + request_id + "\"," + "\"report\":\"domain.overview\",\"args\":{\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\"},\"userId\":" + _userId + ",\"apiKey\":\"" + _apiKey + "\"" + "}}";
                var url = "https://www.semrush.com/dpa/rpc";
                var json = await HttpCaller.PostJson(url, jsonFormData);
                var obj = JObject.Parse(json);
                var authorityScore = (long)obj.SelectToken("..authorityScore");
                var backlinks = (long)obj.SelectToken("..backlinks");
                return (authorityScore, backlinks);
            }
            catch (Exception e)
            {
                Reporter.Error($"Error on getting Authority score {domainName} : {e.Message}");
                return (0, 0);
            }
        }

    }
}
