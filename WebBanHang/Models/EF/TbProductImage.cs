using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebBanHang.Models.EF
{
    public partial class TbProductImage
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Image { get; set; }
        public bool? IsDefault { get; set; }

        public virtual TbProducts Product { get; set; }
    }
}
