using System.Collections.Generic;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetAllAuthorQueryResult
    {
        public List<GetAuthorQueryResult> GetWeatherForecastQueryResult { get; set; }
    }
}