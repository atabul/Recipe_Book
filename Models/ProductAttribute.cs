using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryBeginners.Models
{
    public enum AttributeType {ProductType=1, Season=2,Make=3}

    //1 - Product type , 2 for Season, 3 for Make (Country of Origin)
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }                
        public AttributeType AttributeId { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        
    }
}