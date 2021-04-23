using System.ComponentModel.DataAnnotations;

namespace keeprapp.Models
{
    public class Keepr
    {

        public int Id { get; set; }

        public string CreatorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Img { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public int Keeps { get; set; }

        public Profile Creator { get; set; }

    }
    public class VaultKeepViewModel : Keepr
    {
        public int VaultKeepId { get; set; }
    }
}