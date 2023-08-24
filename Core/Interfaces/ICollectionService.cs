using Core.DTO.RequestDto;
using Core.Entities;
using Core.Model;

namespace Core.Interfaces
{
    public interface ICollectionService
    {
        Task<PagedResult<CollectionResponseDto>> ListCollections(PagingWithTimeRequestDTO dto);
    }
}