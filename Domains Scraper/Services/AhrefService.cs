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
        public static async Task<List<AhrefDomain>> GetData(List<string> domains, int delay)
        {
            await LogIn();
            var ahrefDomains = new List<AhrefDomain>();
            for (int i = 0; i < domains.Count; i++)
            {
                try
                {
                    var ahrefDomain = await StartScraping(domains[i]);
                    if (ahrefDomain != null)
                    {
                        ahrefDomains.Add(ahrefDomain);
                    }
                    Reporter.Progress(i + 1, domains.Count, $@"Domain Scraped from app.ahrefs.com {i + 1}/{domains.Count}");
                    await Task.Delay(delay);
                }
                catch (Exception ex)
                {

                    //
                }
            }
            return ahrefDomains;
        }

        public static async Task<AhrefDomain> StartScraping(string domainName)
        {

            var ahrefDomain = new AhrefDomain();
            ahrefDomain.Name=domainName;
            var csHashAndUr = await GetCsHashAndUr(domainName);
            _csHash = csHashAndUr.csHash;
            ahrefDomain.Ur = csHashAndUr.Ur;
            ahrefDomain.Dr = await GetDr();
            var backLinks = await GetBacklinksData(domainName);
            ahrefDomain.BacklinksType = backLinks.backlinksType;
            ahrefDomain.TotalBacklinks = backLinks.totalBacklinks;
            var referringDomains = await GetReferringDomainsData(domainName);
            ahrefDomain.ReferringPages = referringDomains.referringPages;
            ahrefDomain.ReferringIPs = referringDomains.referringIPs;
            ahrefDomain.ReferringSubnets = referringDomains.referringSubnets;
            ahrefDomain.ReferringDomainsTypes = referringDomains.refDomains;
            ahrefDomain.TotalReferringDomains = referringDomains.totalReferringDomains;
            //ahrefDomain.OrganicCharts = await GetOrganicDataData(domainName);
            //ahrefDomain.OrganicTraffic = ahrefDomain.OrganicCharts.Last().OrganicTrafficTotal;
            //ahrefDomain.OrganicKeyWords = ahrefDomain.OrganicCharts.Last().OrganicKeyWordsTotal;
            var charts = await GetCharts();
            ahrefDomain.AhrefSimpleCharts = charts.ahrefSimpleCharts;
            ahrefDomain.AhrefNewAndLostCharts = charts.ahrefNewAndLostCharts;
            ahrefDomain.TopLevelDomain = await GetTld();

            return ahrefDomain;
        }

        private static async Task<TopLevelDomain> GetTld()
        {
            var topLevelDomain = new TopLevelDomain();
            var json = await _httpCaller.GetHtmlAhref("https://app.ahrefs.com/site-explorer/ajax/overview/tlds-chart/" + _csHash);
            var obj = JObject.Parse(json);
            var sum = (double)obj.SelectToken("AllSum");
            var dic = new Dictionary<string, string>();
            var tlds = obj.SelectToken("..Series..data2").ToList();
            var Ctlds = obj.SelectToken("..Series..data").ToList();
            var tldsDistribution = new List<TldsDistribution>();
            var tldsDistributionByCountry = new List<TldsDistribution>();
            foreach (var tld in tlds)
            {
                var cName = (string)tld.SelectToken("CName");
                var symbol = ((string)tld.SelectToken("ISO2")).ToLower();
                dic.Add(symbol, cName);
            }
            foreach (var Ctld in Ctlds)
            {
                var name = (string)Ctld.SelectToken("name");
                if (dic.ContainsKey(name))
                {
                    var countryName = dic[name];
                    var value = (int)Ctld.SelectToken("y");
                    double percent = (value * 100) / sum;
                    percent = Math.Round(percent, 2);
                    topLevelDomain.TldsDistributionByCountry.Add(new TldsDistribution { Name = countryName, Value = value, Percent = percent });
                    topLevelDomain.TldsDistribution.Add(new TldsDistribution { Name = name, Value = value, Percent = percent });
                }
                else
                {
                    var value = (int)Ctld.SelectToken("y");
                    double percent = (value * 100) / sum;
                    percent = Math.Round(percent, 2);
                    topLevelDomain.TldsDistribution.Add(new TldsDistribution { Name = name, Value = value, Percent = percent });
                }
            }
            return topLevelDomain;
        }

        public static async Task<(AhrefSimpleCharts ahrefSimpleCharts, AhrefNewAndLostCharts ahrefNewAndLostCharts)> GetCharts()
        {

            var json = await _httpCaller.GetHtmlAhref("https://app.ahrefs.com/site-explorer/ajax/overview/main-chart/" + _csHash);
            var obj = JObject.Parse(json);
            //var obj = JObject.Parse(File.ReadAllText("Charts.txt"));

            var ahrefSimpleChart = GetAhrefSimpleChart(obj);
            var ahrefNewAndLostCharts = GetAhrefNewAndLostCharts(obj);

            return (ahrefSimpleChart, ahrefNewAndLostCharts);
        }

        public static AhrefNewAndLostCharts GetAhrefNewAndLostCharts(JObject obj)
        {
            var ahrefNewAndLostCharts = new AhrefNewAndLostCharts();

            var date = (DateTime)obj.SelectToken("pointStartText");
            var refDomainsNewLastAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'Ref.Domains New')].data").ToList();
            var refDomainsLostAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'Ref.Domains Lost')].data").ToList();
            ahrefNewAndLostCharts.NewAndLostReferringDomainAllTimeChart = GetnewAndLostReferringDomainAllTimeChart(refDomainsNewLastAllTime, refDomainsLostAllTime, date);


            date = (DateTime)obj.SelectToken("pointStartText");
            var refDomainsNewLast30Days = obj.SelectToken("$.all.Series.[?(@.name == 'Ref.Domains New')].data").ToList();
            var refDomainsLost30Days = obj.SelectToken("$.all.Series.[?(@.name == 'Ref.Domains Lost')].data").ToList();
            ahrefNewAndLostCharts.NewAndLostReferringDomain30DaysChart = GetNewAndLostReferringDomain30DaysChart(refDomainsNewLast30Days, refDomainsLost30Days);


            date = (DateTime)obj.SelectToken("pointStartText");
            var newDoFollowAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'NewDoFollow')].data").ToList();
            var lostDoFollowAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'LostDoFollow')].data").ToList();
            var newNoFollowAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'NewNoFollow')].data").ToList();
            var lostNoFollowAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'LostNoFollow')].data").ToList();
            var newRedirectAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'NewRedirect')].data").ToList();
            var lostRedirectAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'LostRedirect')].data").ToList();
            var newOtherAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'NewOther')].data").ToList();
            var lostOtherAllTime = obj.SelectToken("$.monthly.Series.[?(@.name == 'LostOther')].data").ToList();
            ahrefNewAndLostCharts.NewAndBacklinksDomainChartAllTime = GetNewAndBacklinksDomainChartAllTime(newDoFollowAllTime, lostDoFollowAllTime,
            newNoFollowAllTime, lostNoFollowAllTime, newRedirectAllTime, lostRedirectAllTime, newOtherAllTime, lostOtherAllTime, date);



            date = (DateTime)obj.SelectToken("pointStartText");
            var newDoFollow30Days = obj.SelectToken("$.all.Series.[?(@.name == 'NewDoFollow')].data").ToList();
            var lostDoFollow30Days = obj.SelectToken("$.all.Series.[?(@.name == 'LostDoFollow')].data").ToList();
            var newNoFollow30Days = obj.SelectToken("$.all.Series.[?(@.name == 'NewNoFollow')].data").ToList();
            var lostNoFollow30Days = obj.SelectToken("$.all.Series.[?(@.name == 'LostNoFollow')].data").ToList();
            var newRedirect30Days = obj.SelectToken("$.all.Series.[?(@.name == 'NewRedirect')].data").ToList();
            var lostRedirect30Days = obj.SelectToken("$.all.Series.[?(@.name == 'LostRedirect')].data").ToList();
            var newOther30Days = obj.SelectToken("$.all.Series.[?(@.name == 'NewOther')].data").ToList();
            var lostOther30Days = obj.SelectToken("$.all.Series.[?(@.name == 'LostOther')].data").ToList();
            ahrefNewAndLostCharts.NewAndBacklinksDomain30DaysChart = GetNewAndBacklinksDomainChart30Days(newDoFollow30Days, lostDoFollow30Days,
            newNoFollow30Days, lostNoFollow30Days, newRedirect30Days, lostRedirect30Days, newOther30Days, lostOther30Days);

            #region One year charts work
            //var refDomainsNewLastOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'Ref.Domains New')].data").ToList();
            //var refDomainsLostOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'Ref.Domains Lost')].data").ToList();
            //ahrefNewAndLostCharts.NewAndLostReferringDomainChartOneYearChart = GetNewAndLostReferringDomainOneYearChart(refDomainsNewLastOneYear, refDomainsLostOneYear);

            //var newDoFollowOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'NewDoFollow')].data").ToList();
            //var lostDoFollowOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'LostDoFollow')].data").ToList();
            //var newNoFollowOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'NewNoFollow')].data").ToList();
            //var lostNoFollowOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'LostNoFollow')].data").ToList();
            //var newRedirectOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'NewRedirect')].data").ToList();
            //var lostRedirectOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'LostRedirect')].data").ToList();
            //var newOtherOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'NewOther')].data").ToList();
            //var lostOtherOneYear = obj.SelectToken("$.weekly.Series.[?(@.name == 'LostOther')].data").ToList();
            //ahrefNewAndLostCharts.NewAndBacklinksDomainChartOneYear = GetNewAndBacklinksDomainChartOneYear(newDoFollowOneYear, lostDoFollowOneYear,
            //newNoFollowOneYear, lostNoFollowOneYear, newRedirectOneYear, lostRedirectOneYear, newOtherOneYear, lostOtherOneYear); 
            #endregion

            return ahrefNewAndLostCharts;
        }

        private static List<AhrefChartPointNewAndLostBacklinksDomain> GetNewAndBacklinksDomainChart30Days(List<JToken> newDoFollow30Days, List<JToken> lostDoFollow30Days, List<JToken> newNoFollow30Days, List<JToken> lostNoFollow30Days, List<JToken> newRedirect30Days, List<JToken> lostRedirect30Days, List<JToken> newOther30Days, List<JToken> lostOther30Days)
        {
            var newAndBacklinksDomain30DaysChart = new List<AhrefChartPointNewAndLostBacklinksDomain>();

            var date = DateTime.Now;
            for (int i = newDoFollow30Days.Count(); i-- > 0;)
            {
                var nDfl = (long)newDoFollow30Days[i];
                var lDfl = (long)lostDoFollow30Days[i];
                var nNfl = (long)newNoFollow30Days[i];
                var lNfl = (long)lostNoFollow30Days[i];
                var nRdct = (long)newRedirect30Days[i];
                var lRdct = (long)lostRedirect30Days[i];
                var nOther = (long)newOther30Days[i];
                var lOther = (long)lostOther30Days[i];
                var totalNew = nDfl + nNfl + nRdct + nOther;
                var totalLost = lDfl + lNfl + lRdct + lOther;

                newAndBacklinksDomain30DaysChart.Add(new AhrefChartPointNewAndLostBacklinksDomain
                {
                    Date1 = date,
                    Date2 = null,
                    NewDoFollow = nDfl,
                    LostDoFollow = lDfl,
                    NewNoFollow = nNfl,
                    LostNoFollow = lNfl,
                    NewRedirect = nRdct,
                    LostRedirect = lRdct,
                    NewOther = nOther,
                    LostOther = lOther,
                    TotalNew = totalNew,
                    TotalLost = totalLost
                });

                date = date.AddDays(-1);
            }
            newAndBacklinksDomain30DaysChart = newAndBacklinksDomain30DaysChart.OrderBy(x => x.Date1).ToList();
            return newAndBacklinksDomain30DaysChart;
        }

        private static List<AhrefChartPointNewAndLostBacklinksDomain> GetNewAndBacklinksDomainChartOneYear(List<JToken> newDoFollowOneYear, List<JToken> lostDoFollowOneYear, List<JToken> newNoFollowOneYear, List<JToken> lostNoFollowOneYear, List<JToken> newRedirectOneYear, List<JToken> lostRedirectOneYear, List<JToken> newOtherOneYear, List<JToken> lostOtherOneYear)
        {

            var newAndBacklinksDomainChartOneYear = new List<AhrefChartPointNewAndLostBacklinksDomain>();
            var dd = DateTime.Now;
            var date1 = dd;
            var date2 = date1.AddDays(6);
            for (int i = newDoFollowOneYear.Count(); i-- > 0;)
            {
                var nDfl = (long)newDoFollowOneYear[i];
                var lDfl = (long)lostDoFollowOneYear[i];
                var nNfl = (long)newNoFollowOneYear[i];
                var lNfl = (long)lostNoFollowOneYear[i];
                var nRdct = (long)newRedirectOneYear[i];
                var lRdct = (long)lostRedirectOneYear[i];
                var nOther = (long)newOtherOneYear[i];
                var lOther = (long)lostOtherOneYear[i];
                var totalNew = nDfl + nNfl + nRdct + nOther;
                var totalLost = lDfl + lNfl + lRdct + lOther;

                newAndBacklinksDomainChartOneYear.Add(new AhrefChartPointNewAndLostBacklinksDomain
                {
                    Date1 = date1,
                    Date2 = date2,
                    NewDoFollow = nDfl,
                    LostDoFollow = lDfl,
                    NewNoFollow = nNfl,
                    LostNoFollow = lNfl,
                    NewRedirect = nRdct,
                    LostRedirect = lRdct,
                    NewOther = nOther,
                    LostOther = lOther,
                    TotalNew = totalNew,
                    TotalLost = totalLost
                });

                date1 = date1.AddDays(-7);
                date2 = date2.AddDays(-7);
            }
            newAndBacklinksDomainChartOneYear = newAndBacklinksDomainChartOneYear.OrderBy(x => x.Date1).ToList();
            return newAndBacklinksDomainChartOneYear;
        }

        private static List<AhrefChartPointNewAndLostBacklinksDomain> GetNewAndBacklinksDomainChartAllTime(List<JToken> newDoFollowAllTime, List<JToken> lostDoFollowAllTime, List<JToken> newNoFollowAllTime, List<JToken> lostNoFollowAllTime, List<JToken> newRedirectAllTime, List<JToken> lostRedirectAllTime, List<JToken> newOtherAllTime, List<JToken> lostOtherAllTime, DateTime date)
        {
            var newAndBacklinksDomainChartAllTime = new List<AhrefChartPointNewAndLostBacklinksDomain>();

            for (int i = 0; i < newDoFollowAllTime.Count(); i++)
            {
                var nDfl = (long)newDoFollowAllTime[i];
                var lDfl = (long)lostDoFollowAllTime[i];
                var nNfl = (long)newNoFollowAllTime[i];
                var lNfl = (long)lostNoFollowAllTime[i];
                var nRdct = (long)newRedirectAllTime[i];
                var lRdct = (long)lostRedirectAllTime[i];
                var nOther = (long)newOtherAllTime[i];
                var lOther = (long)lostOtherAllTime[i];
                var totalNew = nDfl + nNfl + nRdct + nOther;
                var totalLost = lDfl + lNfl + lRdct + lOther;

                newAndBacklinksDomainChartAllTime.Add(new AhrefChartPointNewAndLostBacklinksDomain
                {
                    Date1 = date,
                    Date2 = null,
                    NewDoFollow = nDfl,
                    LostDoFollow = lDfl,
                    NewNoFollow = nNfl,
                    LostNoFollow = lNfl,
                    NewRedirect = nRdct,
                    LostRedirect = lRdct,
                    NewOther = nOther,
                    LostOther = lOther,
                    TotalNew = totalNew,
                    TotalLost = totalLost
                });

                date = date.AddMonths(1);
            }

            return newAndBacklinksDomainChartAllTime;
        }

        private static List<AhrefChartPointNewAndLostReferringDomain> GetNewAndLostReferringDomainOneYearChart(List<JToken> refDomainsNewLastOneYear, List<JToken> refDomainsLostOneYear)
        {
            var newAndLostReferringDomainChartOneYearChart = new List<AhrefChartPointNewAndLostReferringDomain>();
            var dd = DateTime.Now;
            var date1 = dd;
            var date2 = date1.AddDays(6);

            for (int i = refDomainsNewLastOneYear.Count(); i-- > 0;)
            {
                newAndLostReferringDomainChartOneYearChart.Add(new AhrefChartPointNewAndLostReferringDomain { Date1 = date1, Date2 = date2, New = (long)refDomainsNewLastOneYear[i], Lost = (long)refDomainsLostOneYear[i] });
                date1 = date1.AddDays(-7);
                date2 = date2.AddDays(-7);
            }
            return newAndLostReferringDomainChartOneYearChart;
        }

        private static List<AhrefChartPointNewAndLostReferringDomain> GetNewAndLostReferringDomain30DaysChart(List<JToken> refDomainsNewLast30Days, List<JToken> refDomainsLost30Days)
        {
            var lastMonth = new List<AhrefChartPointNewAndLostReferringDomain>();
            var date = DateTime.Now;
            for (int i = refDomainsNewLast30Days.Count(); i-- > 0;)
            {
                lastMonth.Add(new AhrefChartPointNewAndLostReferringDomain { New = (long)refDomainsNewLast30Days[i], Lost = (long)refDomainsLost30Days[i], Date1 = date, Date2 = null });
                date = date.AddDays(-1);
            }
            lastMonth = lastMonth.OrderBy(x => x.Date1).ToList();
            return lastMonth;
        }

        private static List<AhrefChartPointNewAndLostReferringDomain> GetnewAndLostReferringDomainAllTimeChart(List<JToken> refDomainsNewLastAllTime, List<JToken> refDomainsLostAllTime, DateTime date)
        {
            var alltime = new List<AhrefChartPointNewAndLostReferringDomain>();

            for (int i = 0; i < refDomainsNewLastAllTime.Count(); i++)
            {

                alltime.Add(new AhrefChartPointNewAndLostReferringDomain { New = (long)refDomainsNewLastAllTime[i], Lost = (long)refDomainsLostAllTime[i], Date1 = date, Date2 = null });
                date = date.AddMonths(1);
            }
            return alltime;
        }

        private static AhrefSimpleCharts GetAhrefSimpleChart(JObject obj)
        {
            var simpleCharts = new AhrefSimpleCharts();

            var date = (DateTime)obj.SelectToken("pointStartText");
            var refDomains = obj.SelectToken("$.all.Series.[?(@.name == 'Referring Domains')].data").ToList();
            simpleCharts.ReferringDomaiChart = GetChart(refDomains, date);

            date = (DateTime)obj.SelectToken("pointStartText");
            var refPages = obj.SelectToken("$.all.Series.[?(@.name == 'Total')].data").ToList();
            simpleCharts.ReferringPagesChart = GetChart(refPages, date);

            date = (DateTime)obj.SelectToken("pointStartText");
            var domainRatings = obj.SelectToken("$.all.Series.[?(@.name == 'DomainRating')].data").ToList();
            simpleCharts.DomainRatingChart = GetChart(domainRatings, date);

            return simpleCharts;
        }

        private static List<AhrefChartPointDomain> GetChart(List<JToken> tokens, DateTime date)
        {
            var chart = new List<AhrefChartPointDomain>();

            for (int i = 0; i < tokens.Count(); i++)
            {
                long value = 0;
                var token = (string)tokens[i];
                if (token != null)
                {
                    value = (long)tokens[i];
                }
                chart.Add(new AhrefChartPointDomain { Date = date, Value = value });
                date = date.AddDays(1);
            }
            return chart;
        }

        private static async Task<(List<BacklinksType> backlinksType, long totalBacklinks)> GetBacklinksData(string domainName)
        {
            var backlinksType = new List<BacklinksType>();
            var json = await _httpCaller.GetHtmlAhref("https://app.ahrefs.com/site-explorer/ajax/overview/backlinks-stats/" + _csHash);
            var obj = JObject.Parse(json);
            var totalBacklinks = (long)obj.SelectToken("total_backlinks");
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
        public static async Task<List<OrganicChart>> GetOrganicDataData(string domainName)
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
        private static async Task<(List<BacklinksType> refDomains, long totalReferringDomains, long referringPages, long referringIPs, long referringSubnets)> GetReferringDomainsData(string domainName)
        {
            var referringDomains = new List<BacklinksType>();
            var json = await _httpCaller.GetHtmlAhref("https://app.ahrefs.com/site-explorer/ajax/overview/referring-domains-stats/" + _csHash);
            var obj = JObject.Parse(json);
            var referringPages = (long)obj.SelectToken("referring_pages");
            var referringIPs = (long)obj.SelectToken("refips");
            var referringSubnets = (long)obj.SelectToken("refclass_c");
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
            return (referringDomains, totalReferringDomains, referringPages, referringIPs, referringSubnets);
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
