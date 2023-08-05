namespace EventRun_Api.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Observations { get; set; }

    public bool Active { get; set; }
}
