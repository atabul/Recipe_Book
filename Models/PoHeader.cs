namespace InventoryBeginners.Models
{
    public class PoHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string PoNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PoDate { get; set; } = DateTime.Now.Date;


        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; private set; }

        [ForeignKey("BaseCurrency")]
        [Required]
        public int BaseCurrencyId { get; set; }
        public virtual Currency BaseCurrency { get; private set; }


        [ForeignKey("POCurrency")]
        [Required]
        public int PoCurrencyId { get; set; }
        public virtual Currency POCurrency { get; private set; }

        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "smallmoney")]
        [Required]
        public decimal ExchangeRate { get; set; }

        [Column(TypeName = "smallmoney")]
        [Required]
        public decimal DiscountPercent { get; set; }


        [Required]
        [MaxLength(15)]
        public string QuotationNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime QuotationDate { get; set; }=  DateTime.Now.Date;



        [Required]
        [MaxLength(500)]
        public string PaymentTerms { get; set; } = " ";

        [Required]
        [MaxLength(500)]
        public string Remarks { get; set; } = " ";

        public virtual List<PoDetail> PoDetails { get; set; } =new List<PoDetail>();        


    }
}
