using System.ComponentModel.DataAnnotations;

namespace ServiceApresVenteApp.ViewModels
{
    public class CreateRoleViewModel
    {

        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
