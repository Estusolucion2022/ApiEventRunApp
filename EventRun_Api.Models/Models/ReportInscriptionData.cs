using Microsoft.EntityFrameworkCore;

namespace EventRun_Api.Models.Models
{
    [PrimaryKey(nameof(IdRace), nameof(IdRunner))]
    public class ReportInscriptionData
    {
        public int? IdRunner { get; set; }
        public int? IdRace { get; set; }
        public string? Race { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string? BloodType { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? AirlineCityOrigin { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? ProofPayment { get; set; }
        public string? DetailsPayment { get; set; }
        public string? TshirtSize { get; set; }
        public bool? AuthorizationListEnrolled { get; set; }
        public string? Club { get; set; }
        public string? Observations { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? Category { get; set; }

        public string? CategoryRace { get; set; }
    }
}
