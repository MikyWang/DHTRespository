using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DHTRespository.Models;

public class InfoHash
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key, Required]
    public string Hash { get; set; } = string.Empty;
    public string IP { get; set; } = string.Empty;
    public int Port { get; set; }
    [Required]
    public string Url { get; set; } = string.Empty;
    public string MetaData { get; set; } = string.Empty;
}
