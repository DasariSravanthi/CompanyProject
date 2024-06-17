namespace CompanyApp.Models.DTO;

public class ReceiptDto
{
    public String ReceiptDate { get; set; } = null!;

    public byte SupplierId { get; set; }   // Foreign Key

    public String BillNo { get; set; } = null!;

    public String BillDate { get; set; } = null!;

    public double BillValue { get; set; }
}