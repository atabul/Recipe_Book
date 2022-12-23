namespace InventoryBeginners.Models
{
    public class Product
    {
        [Remote("IsProductCodeValid","Product", AdditionalFields ="Name", ErrorMessage ="Product Code Exists Already")]
        [Key]
        [StringLength(6)]
        public string Code { get; set; }

        [Remote("IsProductNameValid", "Product", AdditionalFields = "Code", ErrorMessage = "Product Name Exists Already")]
        [Required]
        [StringLength(75)]
        public String Name { get; set; }

        [Required]
        [StringLength(255)]
        public String Description { get; set; }

        [Required]         
        [Column(TypeName ="smallmoney")]
        public decimal Cost { get; set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Units")]
        [Display(Name="Unit")]
        public int UnitId { get; set; }
        public virtual Unit Units { get; set; }


        
        [ForeignKey("Brands")]
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        public virtual Brand Brands { get; set; }


        [ForeignKey("Categories")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public virtual Category Categories { get; set; }

        [ForeignKey("ProductGroups")]
        [Display(Name = "ProductGroup")]
        public int? ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroups { get; set; }


        [ForeignKey("ProductProfiles")]
        [Display(Name = "ProductProfile")]
        public int? ProductProfileId { get; set; }
        public virtual ProductProfile ProductProfiles { get; set; }

        public string PhotoUrl { get; set; } = "noimage.png";

        [Display(Name = "Product Photo")]
        [NotMapped]
        public IFormFile ProductPhoto { get; set; }

        [NotMapped]
        public string BreifPhotoName { get; set; }

    }
}
