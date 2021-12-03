using Domains_Scraper.Entity_Framework_folder;
using Domains_Scraper.Models;
using Domains_Scraper.Services;
using Newtonsoft.Json;
using static Domains_Scraper.Entity_Framework_folder.LibraryModel;

namespace Domains_Scraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private async void Start_Click(object sender, EventArgs e)
        {
            InsertData();
            return;
            await MainWork();
        }
        private static void InsertData()
        {
            using (var context = new LibraryContext())
            {
                var domain = JsonConvert.DeserializeObject<SemrushDomain>(File.ReadAllText("json.txt"));
                //var domains = new List<SemrushDomain> { domain, domain, domain, domain, domain, domain };
                var domains = new List<SemrushDomain>();
                for (int i = 0; i < 1; i++)
                {
                    domains.Add(domain);
                }
                //var listOfListOfDoamins = new List<List<SemrushDomain>> { domains, domains, domains, domains, domains, domains, domains, domains, domains, domains, domains, domains };
                context.SemrushDomain.AddRange(domains);
                context.SaveChanges(); 
            }
        }
        private async Task MainWork()
        {

            //InsertData();
            //return;
            await AddParaMetersToSemRush();
            var semrushTask = SemrushServices.GetData();
            await Task.WhenAll(semrushTask);
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
            var list = await SemrushServices.StartScraping("upwork.com");

        }
    }
}