using AutoMapper;
using Nest;
using SalesManagementWebsite.Contracts.Dtos.ElasticSearch;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Utilities;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;

namespace SalesManagementWebsite.Core.Services.ElasticSearchServices
{
    public class ElasticSearchServices : IElasticSearchServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IElasticClient _elasticClient;

        public ElasticSearchServices(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ElasticSearchServices> logger,
            IHttpContextAccessor httpContextAccessor,
            IElasticClient elasticClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _elasticClient = elasticClient;
        }

        public async ValueTask<bool> SyncItemToES(ItemIndex itemIndex)
        {
            var itemIndexs = _mapper.Map<ItemIndex>(itemIndex);

            var result = await _elasticClient.IndexDocumentAsync(itemIndexs);

            if (!result.IsValid)
            {
                return false;
            }

            return true;
        }

        public async ValueTask<ResponseHandle<ItemOutputDto>> SearchItemOnES(string id)
        {
            var doc = await _elasticClient.SearchAsync<ItemIndex>(
                         s => s.Query(q => q
                                  .Ids(i => i
                                      .Values(id)
                                  )
                 ));

            if (!doc.Documents.Any())
            {
                return new ResponseHandle<ItemOutputDto>
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Item), id)
                };
            }

            var documents = _mapper.Map<ItemIndex, ItemOutputDto>(doc.Documents.FirstOrDefault());

            return new ResponseHandle<ItemOutputDto>
            {
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                Data = documents
            };
        }

        public async ValueTask<ResponseHandle<ItemOutputDto>> SearchItemsOnES(List<string> ids)
        {
            var boolQuery = new BoolQuery();

            var mustQuery = new List<QueryContainer>()
            {
                new TermsQuery
                {
                    Field = "id.keyword", //Chưa đánh keyword sẽ cần xài cái này
                    Terms = ids
                }
            };

            if (mustQuery.Count > 0)
            {
                boolQuery.Must = mustQuery;
            }

            var searchRequest = new SearchRequest<ItemIndex>()
            {
                Query = boolQuery,
                From = 0,
                Size = 1000
            };

            var searchResponse = await _elasticClient.SearchAsync<ItemIndex>(searchRequest);

            if (!searchResponse.Documents.Any())
            {
                return new ResponseHandle<ItemOutputDto>
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_LIST, nameof(Item))
                };
            }

            var resultSearchTemp = new List<ItemIndex>();
            resultSearchTemp.AddRange(searchResponse.Documents);

            var documents = _mapper.Map<List<ItemIndex>, List<ItemOutputDto>>(resultSearchTemp);

            return new ResponseHandle<ItemOutputDto>
            {
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                ListData = documents
            };
        }
    }
}
