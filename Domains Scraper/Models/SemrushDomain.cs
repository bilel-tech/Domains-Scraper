namespace Domains_Scraper.Models
{
    public class SemrushDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long AuthorityScore { get; set; }
        public long Backlinks { get; set; }
        public int OrganicDataId { get; set; }
        public int FollowLinksVsNotFollowLinkId { get; set; }
        public int BacklinkTypeId { get; set; }
        public OrganicData OrganicData { get; set; }
        public FollowLinksVsNoFollowLink FollowLinksVsNotFollowLink { get; set; }
        public BacklinkType BacklinkType { get; set; }
    }
}
