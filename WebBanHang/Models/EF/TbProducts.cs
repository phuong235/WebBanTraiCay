using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebBanHang.Models.EF
{
    public partial class TbProducts
    {
        public TbProducts()
        {
            TbOrderDetail = new HashSet<TbOrderDetail>();
            TbProductImage = new HashSet<TbProductImage>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public int Quantity { get; set; }
        public int ViewCount { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        public int ProductCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual TbProductCategory ProductCategory { get; set; }
        public virtual ICollection<TbOrderDetail> TbOrderDetail { get; set; }
        public virtual ICollection<TbProductImage> TbProductImage { get; set; }
    }
}
