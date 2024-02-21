using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestApp
{
    [Table("ip_address")]
    public class IpAddressGeoInfo
    {
        [JsonIgnore, Column("id")]
        public int Id { get; set; } 

        [Column("ip")]
        public string Ip { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("region")]
        public string Region { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("loc")]
        public string Loc { get; set; }

        [Column("org")]
        public string Org { get; set; }

        [Column("postal")]
        public string Postal { get; set; }

        [Column("timezone")]
        public string Timezone { get; set; }

        [Column("readme")]
        public string Readme { get; set; }
    }
}