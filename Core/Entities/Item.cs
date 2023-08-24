using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Item : BaseEntity
{

    public string Sku { get; set; }

    public string ProductName { get; set; }

    public float? Price { get; set; }

    public string VariationDescription { get; set; }

    public string ExtenedDesctiption { get; set; }

    public string AdditionalFeatures { get; set; }

    public string ParentCode { get; set; }

    public string DefaultCode { get; set; }

    public int TypeId { get; set; }

    public int CollectionId { get; set; }

    public int RoomId { get; set; }

    public int StyleId { get; set; }

    public int LifeStyleId { get; set; }

    public int PrimaryMaterialId { get; set; }

    public int SecondaryMaterialId { get; set; }

    public float? Cbm { get; set; }

    public float? Depth { get; set; }

    public float? Width { get; set; }

    public float? Height { get; set; }

    public float? ChairSeatHeight { get; set; }

    public float? ChairArmHeight { get; set; }

    public float? ChairInsideSeatDepth { get; set; }

    public float? ChairInsideSeatWidth { get; set; }

    public float? TableClearance { get; set; }

    public float? TableCloseDepth { get; set; }

    public float? TableCloseWidth { get; set; }

    public float? TableCloseHeight { get; set; }

    public short? TableLeavesCount { get; set; }

    public float? TableLeavesWidth { get; set; }

    public short? TableSeatsCountClosed { get; set; }

    public short? TableSeatsCountOpen { get; set; }

    public float? CmSideAndFrontRailApronClearance { get; set; }

    public float? CmSlatToTopOfSideRailClearance { get; set; }

    public float? CmSlatToTopOfFootRailClearance { get; set; }

    public float? InSideAndFrontRailApronClearance { get; set; }

    public float? InSlatToTopOfSideRailClearance { get; set; }

    public float? InSlatToTopOfFootRailClearance { get; set; }

    public string FinishName { get; set; }

    public float? NetWeightKg { get; set; }

    public float? GrossWeightKg { get; set; }

    public float? NetWeightInch { get; set; }

    public float? GrossWeightInch { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsBestSeller { get; set; }

    public string UrlCode { get; set; }

    public string Description { get; set; }

    public string MetaKeyword { get; set; }

    public string MetaDescription { get; set; }

    public bool? IsActive { get; set; }
    public string ImageMainUrl { get; set; }

    public virtual Collection Collection { get; set; }

    public virtual LifeStyle LifeStyle { get; set; }

    public virtual Room Room { get; set; }

    public virtual Style Style { get; set; }

    public virtual ProductType Type { get; set; }
}
