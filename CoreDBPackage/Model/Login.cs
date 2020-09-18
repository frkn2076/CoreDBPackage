using System.ComponentModel.DataAnnotations;

namespace CoreDBPackage.Model {
    public class Login {
        [Key]
        public int id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string emailKey { get; set; }
        [Required]
        public string password { get; set; }
        public bool IsActive { get; set; }
    }
}
