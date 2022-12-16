﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanHang.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class News
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề tin tức")]
        [MinLength(2, ErrorMessage = "Tên Menu ít nhất phải có 2 ký tự")]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên tác giả")]
        [MinLength(2, ErrorMessage = "Tên Menu ít nhất phải có 2 ký tự")]
        public string Author { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}