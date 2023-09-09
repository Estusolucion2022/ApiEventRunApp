using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRun_Api.Models;

[PrimaryKey(nameof(IdRace), nameof(IdRunner))]
public partial class InscriptionData
{
    [ForeignKey("IdRace")]
    public int IdRace { get; set; }

    [NotMapped]
    public string? Race { get; set; }

    [ForeignKey("IdRunner")]
    public int IdRunner { get; set; }

    public int IdCategory { get; set; }

    [NotMapped]
    public string? Category { get; set; }

    [NotMapped]
    public DateTime RegistrationDate { get; set; }

    public string? AirlineCityOrigin { get; set; }

    public DateTime? DepartureDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? IdPaymentMethod { get; set; }

    [NotMapped]
    public string? PaymentMethod { get; set; }

    public string? ProofPayment { get; set; }

    public string DetailsPayment { get; set; } = null!;

    public string? TshirtSize { get; set; }

    public bool? AuthorizationListEnrolled { get; set; }

    public string? Club { get; set; }

    public string? Observations { get; set; }

    public bool? AcceptanceTyC { get; set; }

    // logger
    [NotMapped]
    public int? IdUser { get; set; }
    [NotMapped]
    public string? Description { get; set; }

    // end logger

}
