namespace EventRun_Api.Models;

public partial class Race
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Year { get; set; }

    public bool Active { get; set; }
}
