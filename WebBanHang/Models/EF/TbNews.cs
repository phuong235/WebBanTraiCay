using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebBanHang.Models.EF
{
    public partial class TbNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
