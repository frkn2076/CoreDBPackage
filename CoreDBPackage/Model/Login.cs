using System.ComponentModel.DataAnnotations;

namespace CoreDBPackage.Model {
    public class Login {
        [Key]
        public int id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
