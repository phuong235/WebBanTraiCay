using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebBanHang.Models.EF
{
    public partial class TbOrder
    {
        public TbOrder()
        {
            TbOrderDetail = new HashSet<TbOrderDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int TotalAmount { get; set; }
        public int TypePayment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        public virtual ICollection<TbOrderDetail> TbOrderDetail { get; set; }
    }
}
