using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRun_Api.Models;

[PrimaryKey(nameof(IdRace), nameof(IdRunner))]
public partial class InscriptionDataResponse
{
    [ForeignKey("IdRace")]
    public int IdRace { get; set; }

    public string? Race { get; set; }

    [ForeignKey("IdRunner")]
    public int IdRunner { get; set; }

    public int IdCategory { get; set; }

    public string? Category { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? AirlineCityOrigin { get; set; }

    public DateTime? DepartureDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? IdPaymentMethod { get; set; }

    public string? PaymentMethod { get; set; }

    public string? ProofPayment { get; set; }

    public string? DetailsPayment { get; set; }

    public string? TshirtSize { get; set; }

    public bool? AuthorizationListEnrolled { get; set; }

    public string? Club { get; set; }

    public string? Observations { get; set; }

    public bool? AcceptanceTyC { get; set; }
}
