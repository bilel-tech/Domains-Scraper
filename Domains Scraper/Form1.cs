using Domains_Scraper.Entity_Framework_folder;
using Domains_Scraper.Models;
using Domains_Scraper.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Domains_Scraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private LibraryContext _context = new LibraryContext();
        private List<SemrushDomain> _semrushLodedDoamins = new List<SemrushDomain>();
        private List<OrganicTrafficChartData> _organicTrafficChartDatas = new List<OrganicTrafficChartData>();
        private List<OrganicChartData> _OrganicChartDatas = new List<OrganicChartData>();
        private List<OrganicTrafficAndKeywordsByCountry> _OrganicTrafficAndKeywordsByCountry = new List<OrganicTrafficAndKeywordsByCountry>();
        private int UpDown;
        private async void Start_Click(object sender, EventArgs e)
        {
            UpDown = (int)DelayUpDown.Value * 1000;
            await AddParaMetersToSemRush();
            await Task.Run(MainWork);
            InsertData();
        }
        private async void InsertData()
        {
            using (_context)
            {
                var domains = JsonConvert.DeserializeObject<List<SemrushDomain>>(File.ReadAllText("SemrushDomains.txt"));
                _semrushLodedDoamins = _context.SemrushDomain.Include(x => x.OrganicData).Include(x => x.OrganicData.AllTimeOrganicData).Include(x => x.OrganicData.OneYearOrganicData).Include(x => x.BacklinkType).Include(x => x.FollowLinksVsNotFollowLink).ToList();

                foreach (var domain in domains)
                {
                    await SaveData(domain);
                }
                await _context.BulkInsert(_organicTrafficChartDatas);
                await _context.BulkInsert(_OrganicChartDatas);
                await _context.BulkInsert(_OrganicTrafficAndKeywordsByCountry);
            }
        }

        private async Task SaveData(SemrushDomain domain)
        {
            var dd = _semrushLodedDoamins.FirstOrDefault(x => x.Name == domain.Name);

            if ((_semrushLodedDoamins.Count > 0 && dd == null) || _semrushLodedDoamins.Count == 0)
            {
                GatheringListsForBulkInsert(domain);
            }
            else
            {
                dd = _context.SemrushDomain.Include(x => x.OrganicData).Include(x => x.OrganicData.AllTimeOrganicData).Include(x => x.OrganicData.OneYearOrganicData)
                 .Where(x => x.Name == domain.Name).FirstOrDefault();
                dd.AuthorityScore = domain.AuthorityScore;
                dd.OrganicData.OrganicTraffic = domain.OrganicData.OrganicTraffic;
                dd.OrganicData.OrganicKeywords = domain.OrganicData.OrganicKeywords;
                dd.Backlinks = domain.Backlinks;
                dd.BacklinkType.TextLinks = domain.BacklinkType.TextLinks;
                dd.BacklinkType.FrameLinks = domain.BacklinkType.FrameLinks;
                dd.BacklinkType.FormLinks = domain.BacklinkType.FormLinks;
                dd.BacklinkType.ImageLinks = domain.BacklinkType.ImageLinks;
                dd.FollowLinksVsNotFollowLink.FollowLinks = domain.FollowLinksVsNotFollowLink.FollowLinks;
                dd.FollowLinksVsNotFollowLink.NotFollowLinks = domain.FollowLinksVsNotFollowLink.NotFollowLinks;
                _context.SaveChanges();
                GatheringNewPointsInCharToInsert(domain, dd);
            }

        }

        private void GatheringNewPointsInCharToInsert(SemrushDomain domain, SemrushDomain dd)
        {
            var organicPositionsDistrubutionLastDate = _context.SemrushDomain.Include(x => x.OrganicData.OrganicPositionsDistrubution)
                     .Where(x => x.Name == domain.Name).FirstOrDefault().OrganicData.OrganicPositionsDistrubution.Last();
            var newOrganicPositionsDistrubutionLastDates = domain.OrganicData.OrganicPositionsDistrubution.Where(date => date.Date > organicPositionsDistrubutionLastDate.Date).ToList();
            dd.OrganicData.OrganicPositionsDistrubution.AddRange(newOrganicPositionsDistrubutionLastDates);
            _context.SaveChanges();

            var organicTrafficAndKeywordsByCountry = _context.SemrushDomain.Include(x => x.OrganicData.OrganicTrafficAndKeywordsByCountry)
                .Where(x => x.Name == domain.Name).FirstOrDefault().OrganicData.OrganicTrafficAndKeywordsByCountry;

            var newCountries = domain.OrganicData.OrganicTrafficAndKeywordsByCountry.Where(p => !organicTrafficAndKeywordsByCountry.Any(p2 => p2.Country == p.Country)).ToList();

            foreach (var kwbc in dd.OrganicData.OrganicTrafficAndKeywordsByCountry)
            {
                var objt = domain.OrganicData.OrganicTrafficAndKeywordsByCountry.Where(x => x.Country == kwbc.Country).FirstOrDefault();
                if (objt != null)
                {
                    kwbc.OranicTraficValue = objt.OranicTraficValue;
                    kwbc.KeyWordsValue = objt.KeyWordsValue;
                }
            }
            dd.OrganicData.OrganicTrafficAndKeywordsByCountry.AddRange(newCountries);

            var allTimeOrganicTrafficChartDataLastDate = _context.SemrushDomain.Include(x => x.OrganicData.AllTimeOrganicData.AllTimeOrganicTrafficChartData)
                .Where(x => x.Name == domain.Name).FirstOrDefault().OrganicData.AllTimeOrganicData.AllTimeOrganicTrafficChartData.Last();
            var newAllTimeOrganicTrafficChartDataLastDates = domain.OrganicData.AllTimeOrganicData.AllTimeOrganicTrafficChartData.Where(date => date.Date > allTimeOrganicTrafficChartDataLastDate.Date).ToList();
            dd.OrganicData.AllTimeOrganicData.AllTimeOrganicTrafficChartData.AddRange(newAllTimeOrganicTrafficChartDataLastDates);

            var oneYearOrganicKeyWordsChartDataLastDate = _context.SemrushDomain.Include(x => x.OrganicData.OneYearOrganicData.OneYearOrganicKeyWordsChartData)
                .Where(x => x.Name == domain.Name).FirstOrDefault().OrganicData.OneYearOrganicData.OneYearOrganicKeyWordsChartData.Last();
            var newOneYearOrganicKeyWordsChartDataLastDates = domain.OrganicData.OneYearOrganicData.OneYearOrganicKeyWordsChartData.Where(date => date.Date > oneYearOrganicKeyWordsChartDataLastDate.Date).ToList();
            dd.OrganicData.OneYearOrganicData.OneYearOrganicKeyWordsChartData.AddRange(newOneYearOrganicKeyWordsChartDataLastDates);

            var allTimeOrganicKeyWordsChartDataLastDate = _context.SemrushDomain.Include(x => x.OrganicData.AllTimeOrganicData.AllTimeOrganicKeyWordsChartData)
                .Where(x => x.Name == domain.Name).FirstOrDefault().OrganicData.AllTimeOrganicData.AllTimeOrganicKeyWordsChartData.Last();
            var newAllTimeOrganicKeyWordsChartDataLastDates = domain.OrganicData.AllTimeOrganicData.AllTimeOrganicKeyWordsChartData.Where(date => date.Date > allTimeOrganicKeyWordsChartDataLastDate.Date).ToList();
            dd.OrganicData.AllTimeOrganicData.AllTimeOrganicKeyWordsChartData.AddRange(newAllTimeOrganicKeyWordsChartDataLastDates);

            var oneYearOrganicTrafficChartDataLastDate = _context.SemrushDomain.Include(x => x.OrganicData.OneYearOrganicData.OneYearOrganicTrafficChartData)
                .Where(x => x.Name == domain.Name).FirstOrDefault().OrganicData.OneYearOrganicData.OneYearOrganicTrafficChartData.Last();
            var newOneYearOrganicTrafficChartDataLastDates = domain.OrganicData.OneYearOrganicData.OneYearOrganicTrafficChartData.Where(date => date.Date > oneYearOrganicTrafficChartDataLastDate.Date).ToList();
            dd.OrganicData.OneYearOrganicData.OneYearOrganicTrafficChartData.AddRange(newOneYearOrganicTrafficChartDataLastDates);

            _context.SaveChanges();

        }

        private void GatheringListsForBulkInsert(SemrushDomain domain)
        {

            var oneYearOrganicTrafficChartData = domain.OrganicData.OneYearOrganicData.OneYearOrganicTrafficChartData;

            var oneYearOrganicKeyWordsChartData = domain.OrganicData.OneYearOrganicData.OneYearOrganicKeyWordsChartData;

            var allTimeOrganicTrafficChartData = domain.OrganicData.AllTimeOrganicData.AllTimeOrganicTrafficChartData;

            var allTimeOrganicKeyWordsChartData = domain.OrganicData.AllTimeOrganicData.AllTimeOrganicKeyWordsChartData;

            var organicPositionsDistrubution = domain.OrganicData.OrganicPositionsDistrubution;

            var organicTrafficAndKeywordsByCountry = domain.OrganicData.OrganicTrafficAndKeywordsByCountry;

            domain.OrganicData.OneYearOrganicData.OneYearOrganicTrafficChartData = null;
            domain.OrganicData.OneYearOrganicData.OneYearOrganicKeyWordsChartData = null;
            domain.OrganicData.AllTimeOrganicData.AllTimeOrganicTrafficChartData = null;
            domain.OrganicData.AllTimeOrganicData.AllTimeOrganicKeyWordsChartData = null;
            domain.OrganicData.OrganicPositionsDistrubution = null;
            domain.OrganicData.OrganicTrafficAndKeywordsByCountry = null;
            _context.SemrushDomain.Add(domain);
            _context.SaveChanges();
            var dd = _context.SemrushDomain.Include(x => x.OrganicData).Include(x => x.OrganicData.AllTimeOrganicData).Include(x => x.OrganicData.OneYearOrganicData)
             .Where(x => x.Name == domain.Name).FirstOrDefault();
            foreach (var oneYearOrganicTCHData in oneYearOrganicTrafficChartData)
            {
                oneYearOrganicTCHData.OneYearOrganicDataId = dd.OrganicData.OneYearOrganicData.Id;
            }
            foreach (var oneYearOrganicKCHData in oneYearOrganicKeyWordsChartData)
            {
                oneYearOrganicKCHData.OneYearOrganicDataId = dd.OrganicData.OneYearOrganicData.Id;
            }
            foreach (var allTimeOrganicTCHData in allTimeOrganicTrafficChartData)
            {
                allTimeOrganicTCHData.AllTimeOrganicDataId = dd.OrganicData.AllTimeOrganicData.Id;
            }
            foreach (var allTimeOrganicKCHData in allTimeOrganicKeyWordsChartData)
            {
                allTimeOrganicKCHData.AllTimeOrganicDataId = dd.OrganicData.AllTimeOrganicData.Id;
            }
            foreach (var oPD in organicPositionsDistrubution)
            {
                oPD.OrganicDataId = dd.OrganicDataId;
            }
            foreach (var oTrafficKBC in organicTrafficAndKeywordsByCountry)
            {
                oTrafficKBC.OrganicDataId = dd.OrganicDataId;
            }
            _organicTrafficChartDatas.AddRange(oneYearOrganicTrafficChartData);
            _organicTrafficChartDatas.AddRange(allTimeOrganicTrafficChartData);
            _OrganicChartDatas.AddRange(oneYearOrganicKeyWordsChartData);
            _OrganicChartDatas.AddRange(allTimeOrganicKeyWordsChartData);
            _OrganicChartDatas.AddRange(organicPositionsDistrubution);
            _OrganicTrafficAndKeywordsByCountry.AddRange(organicTrafficAndKeywordsByCountry);
        }

        private async Task MainWork()
        {

            //InsertData();
            //return;
            var domains = await GetDomainsToScrapeFromDatbase();
            var semrushTask = await SemrushServices.GetData(domains);
            var json = JsonConvert.SerializeObject(semrushTask, Formatting.Indented);
            File.WriteAllText("SemrushDomains.txt", json);
            //await Task.WhenAll(semrushTask);
        }



        private async Task<List<string>> GetDomainsToScrapeFromDatbase()
        {
            var domains = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                domains.Add("google.com");
            }
            return domains;
        }

        private async Task AddParaMetersToSemRush()
        {
            //var formLogIn = "{\"locale\":\"en\",\"source\":\"semrush\",\"g-recaptcha-response\":\"\",\"user-agent-hash\":\"7000108f0743214a82aeaaa4b5e796d8\",\"email\":\"shahid@houseofcomms.com\",\"password\":\"temp@HOC123\"}";
            //var resJson = await HttpCaller.PostJson("https://www.semrush.com/sso/authorize", formLogIn);
            //var doc = await HttpCaller.GetDoc("https://www.semrush.com/projects/");
            //var script = doc.DocumentNode.SelectSingleNode("//script[contains(text(),'window.sm2.user =')]").InnerText.Trim();
            //var x = script.IndexOf("user = ") + 7;
            //var xx = script.LastIndexOf("};") + 1;
            //var jsonn = script.Substring(x, xx-x);

            SemrushServices._userId = 5552212;
            SemrushServices._apiKey = "fdf437119e0eee3fcbb8a65c6fa20eef";
            //HttpCaller = new HttpCaller();
            ////fdf437119e0eee3fcbb8a65c6fa20eef
            //var domainName = "upwork.com";
            //var formDataAuthorityScore = "{\"id\":21,\"jsonrpc\":\"2.0\",\"method\":\"backlinks.Summary\",\"params\":{\"request_id\":\"6303b0fb-bcc7-4b65-a035-56f0b862eba2\",\"report\":\"domain.overview\",\"args\":{\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\"},\"userId\":" + SemrushServices._userId + ",\"apiKey\":\"" + SemrushServices._apiKey + "\"" + "}}";
            //var urlAuthority = "https://www.semrush.com/dpa/rpc";
            //var json = await HttpCaller.PostJson(urlAuthority, formDataAuthorityScore);
            //formDataAuthorityScore = "{\"id\":21,\"jsonrpc\":\"2.0\",\"method\":\"backlinks.Summary\",\"params\":{\"request_id\":\"6303b0fb-bcc7-4b65-a035-56f0b86ieba2\",\"report\":\"domain.overview\",\"args\":{\"searchItem\":\"" + domainName + "\"," + "\"searchType\":\"domain\"},\"userId\":" + SemrushServices._userId + ",\"apiKey\":\"" + SemrushServices._apiKey + "\"" + "}}";
            //json = await HttpCaller.PostJson(urlAuthority, formDataAuthorityScore);
            //var obj = JObject.Parse(json);
            //var authorityScore = (string)obj.SelectToken("..authorityScore");

        }
    }
}