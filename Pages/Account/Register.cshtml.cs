using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace BookStore1.Pages
{
    public class RegModel : PageModel{
        [BindProperty]
        [Required]
        public string Username {get; set;} ="";
        [BindProperty]
        [Required]
        public string Email {get; set;} ="";
        [BindProperty]
        [Required]
        public string Password {get; set;} ="";
        [BindProperty]
        [Required]
        public string ConfirmPassword {get; set;} ="";
        public string succesMessage="";
        public string errorMessage ="";

        public void OnGet(){

        }
        public void OnPost(){
            if(!ModelState.IsValid){
                errorMessage="Data validation failed";
                return;
            }
            succesMessage="Succesfully registered";
            Username="";
            Email="";
            Password="";
            ConfirmPassword="";
            ModelState.Clear();
        }
    }
}