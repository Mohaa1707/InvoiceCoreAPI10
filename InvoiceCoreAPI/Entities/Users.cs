using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceCoreAPI.Entities;

[Table("Users")]


public class Users
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string UserName { get; set; }
    [Required]
    [StringLength(255)]
    public string Email { get; set; }
    [Required]
    [StringLength(500)]
    public string PasswordHash { get; set; }
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }
    [StringLength(100)]
    public string? MiddleName { get; set; }
    [Required]
    [StringLength(100)]
    public string LastName { get; set; }
    [Required]
    [StringLength(200)]
    public string DisplayName { get; set; }
    [Required]
    [StringLength(25)]
    public string PhoneNumber { get; set; }
    [StringLength(25)]
    public string? AlternatePhone { get; set; }
    [Required]
    [StringLength(255)]
    public string AddressLine1 { get; set; }
    [StringLength(255)]
    public string? AddressLine2 { get; set; }
    [Required]
    [StringLength(100)]
    public string City { get; set; }
    [Required]
    [StringLength(100)]
    public string State { get; set; }
    [Required]
    [StringLength(20)]
    public string ZipCode { get; set; }
    [Required]
    [StringLength(100)]
    public string Country { get; set; }
    public DateTime? DateOfBirth { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public bool IsDeleted { get; set; }
    public DateTime? LastLoginDate { get; set; }
    [Required]
    [StringLength(100)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}



