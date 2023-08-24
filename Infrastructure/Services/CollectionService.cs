

using Core.DTO.RequestDto;
using Core.Entities;
using Core.Interfaces;
using Core.Model;
using Infrastructure.HelperService;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class CollectionService : ICollectionService
{
    private readonly IUnitOfWork _uow;
    public CollectionService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PagedResult<CollectionResponseDto>> ListCollections(PagingWithTimeRequestDTO dto)
    {
        // var results = await _uow.Repository<Collection>().GetEntityWithSpec(x => x.IsActive != false, null, "");

        // var lst = new List<CollectionResponseDto>();

        // var query = "SELECT * FROM collections;";
        // var test = await _uow.Repository<Collection>().GetWithRawSql(query);

        // foreach (var item in test)
        // {
        //     var collection = new CollectionResponseDto()
        //     {
        //         Id = item.Id,
        //         CollectionName = item.CollectionName,
        //         DisplayName = item.DisplayName,
        //         SortOrder = item.SortOrder,
        //         MetaKeyword = item.MetaKeyword,
        //         MetaDescription = item.MetaDescription,
        //         UrlCode = item.UrlCode
        //     };
        //     lst.Add(collection);
        // }

        string searchKey = !string.IsNullOrEmpty(dto.SearchKey) ? dto.SearchKey.ToUpper() : string.Empty;
        var diseasesPaging = new PagedResult<Collection>();
        diseasesPaging = await _uow.Repository<Collection>().GetWithPaging(dto.PageIndex, dto.PageSize);

        var diseasesByLanguage = MapListHelpers.MapListObjectToString(diseasesPaging.Results.ToList());
        var diseasesAfterMapping = JsonConvert.DeserializeObject<List<CollectionResponseDto>>(diseasesByLanguage).OrderBy(x => x.DisplayName).ToList();
        var diseasesResult = new PagedResult<CollectionResponseDto>
        {
            PageIndex = diseasesPaging.PageIndex,
            PageSize = diseasesPaging.PageSize,
            NumberOfPage = diseasesPaging.NumberOfPage,
            TotalCount = diseasesPaging.TotalCount,
            Results = diseasesAfterMapping
        };
        return diseasesResult;
    }


    // public async Task<List<CollectionResponseDto>> ListCollections(PagingWithTimeRequestDTO dto)
    // {
    //     var results = await repository.GetEntityWithSpec(x => x.IsActive != false, null, "");

    //     var lst = new List<CollectionResponseDto>();

    //     foreach (var item in results)
    //     {
    //         var collection = new CollectionResponseDto()
    //         {
    //             Id = item.Id,
    //             CollectionName = item.CollectionName,
    //             DisplayName = item.DisplayName,
    //             SortOrder = item.SortOrder,
    //             MetaKeyword = item.MetaKeyword,
    //             MetaDescription = item.MetaDescription,
    //             UrlCode = item.UrlCode
    //         };
    //         lst.Add(collection);
    //     }

    //     return lst;
    // }
}
