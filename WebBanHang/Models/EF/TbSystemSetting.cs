using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebBanHang.Models.EF
{
    public partial class TbSystemSetting
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string SettingDescription { get; set; }
    }
}
