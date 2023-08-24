using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Banner : BaseEntity
{
    public string BannerName { get; set; }

    public int SortOrder { get; set; }

    public bool? IsActive { get; set; }

    public string Description { get; set; }

    public virtual ICollection<BannerDetail> BannerDetails { get; set; } = new List<BannerDetail>();
}
