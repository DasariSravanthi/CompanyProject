namespace CompanyApp.Models.DTO;

public class ProductionCalendaringDto
{
    public String ProductionCoatingDate { get; set; } = null!;

    public int ProductionCoatingId { get; set; }    // Foreign Key

    public string RollNumber { get; set; } = null!;

    public Single BeforeWeight { get; set; }

    public Single BeforeMoisture { get; set; }

    public String CalendaringStart { get; set; } = null!;

    public String CalendaringEnd { get; set; } = null!;

    public byte RollCount { get; set; }
}