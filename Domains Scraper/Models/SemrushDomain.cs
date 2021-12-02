namespace Domains_Scraper.Models
{
    public class SemrushDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorityScore { get; set; }
        public int Backlinks { get; set; }
        public OrganicData OrganicData { get; set; }
        public FollowLinksVsNoFollowLink FollowLinksVsNotFollowLink { get; set; }
        public BacklinkType BacklinkType { get; set; }
    }
}
