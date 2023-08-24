using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class BannerDetail : BaseEntity
{
    public int BannerId { get; set; }

    public short? SortOrder { get; set; }

    public string BannerDetailName { get; set; }

    public string UrlCode { get; set; }

    public string DisplayName { get; set; }

    public string QuickTitle { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public bool? IsActive { get; set; }

    public virtual Banner Banner { get; set; }
}
