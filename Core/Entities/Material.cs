using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Material : BaseEntity
{

    public string MaterialName { get; set; }

    public string UrlCode { get; set; }

    public short? SortOrder { get; set; }

    public string Desription { get; set; }

    public string MetaKeyword { get; set; }

    public string MetaDescription { get; set; }

    public string DisplayName { get; set; }

    public string ImageUrl { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<MaterialDetail> MaterialDetails { get; set; } = new List<MaterialDetail>();
}
