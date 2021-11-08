using System.Collections.Generic;

namespace Application.Models
{
  public class PaginationResponse<TModel>
  {
    public PaginationResponse()
    {
    }

    public PaginationResponse(IEnumerable<TModel> list, int totalCount)
    {
      List = list;
      TotalCount = totalCount;
    }

    public IEnumerable<TModel> List { get; set; }

    public int TotalCount { get; set; }
  }
}
