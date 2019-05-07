using System.ComponentModel.DataAnnotations;

namespace Dzaba.AspNet.Contracts
{
    public class RegisterUser
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(64)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
