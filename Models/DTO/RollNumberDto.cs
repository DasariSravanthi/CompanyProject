namespace CompanyApp.Models.DTO;

public class RollNumberDto
{
    public int ReceiptDetailId { get; set; }    // Foreign Key

    public String RollNumberValue { get; set; } = null!;
}