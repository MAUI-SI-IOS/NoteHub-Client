
using bus.logic.Result;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace bus.logic.ApiService.Directors
{
    public interface IQueryStrategy<T>
    {
        HttpClient _client { get; set; }
        JsonSerializerOptions _serializerOptions { get; set; }
        Task<Result<T, HttpException>> DoQuery();
    }
}
