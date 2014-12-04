using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace EASNSMVC4.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "User name")]
        [StringLength(25, ErrorMessage = "Screen name cannot exceed 25 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "First name cannot exceed 30 characters.")]
        public string UserFirst { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string UserLast { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please provide a valid email address.")]
        [StringLength(120, ErrorMessage = "Email Address cannot exceed 120 characters.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Telephone number is required.")]
        [Display(Name = "Telephone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please provide a valid telephone number.")]
        [StringLength(11, ErrorMessage = "Telephone number cannot exceed 11 characters.")]
        public string UserTelephone { get; set; }

        [Required(ErrorMessage = "Requested Role is required.")]
        [Display(Name = "Requested Role")]
        public string UserReqRole { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public string UserCity { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        [StringLength(25, ErrorMessage = "Screen name cannot exceed 25 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "First name cannot exceed 30 characters.")]
        public string UserFirst { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string UserLast { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please provide a valid email address.")]
        [StringLength(120, ErrorMessage = "Email Address cannot exceed 120 characters.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Telephone number is required.")]
        [Display(Name = "Telephone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please provide a valid telephone number.")]
        [StringLength(11, ErrorMessage = "Telephone number cannot exceed 11 characters.")]
        public string UserTelephone { get; set; }

        [Required(ErrorMessage = "Requested Role is required.")]
        [Display(Name = "Requested Role")]
        public string UserReqRole { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public string UserCity { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
