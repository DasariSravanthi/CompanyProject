using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class Receipt
{
    [Column("R_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReceiptId { get; set; }

    [Column("R_Date")]
    public DateOnly ReceiptDate { get; set; }

    [Column("S_Id")]
    public byte SupplierId { get; set; }   // Foreign Key

    [Column("Bill_No", TypeName = "varchar(100)")]
    public String BillNo { get; set; } = null!;

    [Column("Bill_Date")]
    public DateOnly BillDate { get; set; }

    [Column("Bill_Value")]
    public float BillValue { get; set; }

    // Navigation property
    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = null!;
}