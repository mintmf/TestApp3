using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp
{
    [Table("ip_bogon")]
    public class IpBogon
    {
        public int Id { get; set; }
        public string Ip { get; set; }  
    }
}
