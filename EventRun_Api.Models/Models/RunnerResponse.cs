using System.ComponentModel.DataAnnotations.Schema;

namespace EventRun_Api.Models;

public partial class RunnerResponse
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string CodeDocumentType { get; set; } = null!;

    public string? DocumentType { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public string CodeCity { get; set; } = null!;

    public string? City { get; set; }

    public string? Address { get; set; }

    public string BloodType { get; set; } = null!;

    public string? CodeCountryNationality { get; set; }

    public string? CountryNationality { get; set; }

    public int IdGender { get; set; }

    public string? Gender { get; set; }

    public string EmergencyContactName { get; set; } = null!;

    public string EmergencyContactPhone { get; set; } = null!;
}
