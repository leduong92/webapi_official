using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.DTO.RequestDto;

public class CollectionResponseDto
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public string CollectionName { get; set; }

    public string UrlCode { get; set; }

    public short? SortOrder { get; set; }

    public string Desription { get; set; }

    public string MetaKeyword { get; set; }

    public string MetaDescription { get; set; }

    public string DisplayName { get; set; }

    public string ImageUrl { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsLogo { get; set; }

    public bool? IsStory { get; set; }

    public bool? IsDisplayName { get; set; }

    public bool? IsDescription { get; set; }

    public bool? IsImage { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
