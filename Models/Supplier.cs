namespace InventoryBeginners.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(6)]
        public string Code { get; set; } = "";

        [Required]
        [MaxLength(75)]
        public string Name { get; set; } = "";        
        

        [Remote("IsEmailExists","Supplier",AdditionalFields="Id", ErrorMessage ="Email Id Already Exists")]
        [Required]
        [MaxLength(75)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-Mail is not Valid")]
        public string EmailId { get; set; } = "";

        [MaxLength(75)]
        public string Address { get; set; } = "";

        [MaxLength(75)]
        public string PhoneNo { get; set; } = "";
    }
}
