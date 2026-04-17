using System.ComponentModel.DataAnnotations;

namespace StocksOperation.DTO.DTO_ACCOUNTS
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }  
    }
}
