namespace SportsLeague.Domain.Entities;

public class Tournament : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}