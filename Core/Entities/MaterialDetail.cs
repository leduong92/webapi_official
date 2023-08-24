using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class MaterialDetail : BaseEntity
{

    public int MaterialId { get; set; }

    public string MaterialDetailName { get; set; }

    public string DisplayName { get; set; }

    public string Desription { get; set; }

    public string MetaKeyword { get; set; }

    public string MetaDescription { get; set; }

    public bool? IsActive { get; set; }

    public string ImageUrl { get; set; }

    public virtual Material Material { get; set; }
}
