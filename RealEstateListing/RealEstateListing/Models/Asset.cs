using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateListing.Models
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
        public string Location { get; set; }
        public DateTime? SoldDate { get; set; }
        public string SoldTo { get; set; }

        public bool IsAssetSold
        {
            get
            {
                return SoldDate != null;
            }
        }
    }
}
