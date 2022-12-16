using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebBanHang.Models.EF
{
    public partial class TbProductCategory
    {
        public TbProductCategory()
        {
            TbProducts = new HashSet<TbProducts>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Icon { get; set; }
        public int Position { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<TbProducts> TbProducts { get; set; }
    }
}
