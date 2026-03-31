using bus.logic.ApiService.Url;
using bus.logic.Result;
using bus.logic.service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace bus.logic.ApiService.Directors
{
    internal class PostStrategy<TOutput, TInput> : IQueryStrategy<TOutput>
    {
        readonly string route;
        TInput body;
        public HttpClient _client { get; set; }
        public JsonSerializerOptions _serializerOptions { get; set; }
        public PostStrategy(HttpClient client, string route, TInput body, JsonSerializerOptions options)
        {
            this._client = client;
            this._serializerOptions = options;
            this.route = route;
        }



        public async Task<Result<TOutput, HttpException>> DoQuery()
        {
            try
            {
                var response = await _client.PostAsJsonAsync<TInput>(route, body, _serializerOptions);
                response.EnsureSuccessStatusCode();
                if (typeof(TOutput) == typeof(Unit))
                {
                    return Result<TOutput, HttpException>.Success((TOutput)(object)Unit.Default);
                }

                var data = await response.Content.ReadFromJsonAsync<TOutput>(_serializerOptions);
                return Result<TOutput, HttpException>.Success(data);
            }
            catch (HttpRequestException e) when (e.StatusCode.HasValue)
            {
                Debug.WriteLine($"Http exception: {e.Message}");
                int code = (int)e.StatusCode.Value;
                return Result<TOutput, HttpException>.Failure(new HttpException(code));
            }
            catch (Exception e)
            {
                return Result<TOutput, HttpException>.Failure(new HttpException(500));
            }
        }

    }
}
