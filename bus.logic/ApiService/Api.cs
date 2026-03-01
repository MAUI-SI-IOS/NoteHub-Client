using bus.logic.ApiService.Directors;
using bus.logic.ApiService.Url;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace bus.logic.ApiService
{
    internal class Api
    {
        IQueryDirector director;
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public Api(HttpClient client)
        {
            _client = client;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async Task<T> Send<T>()
        {
            var query = director.MakeQuery();
            return query.Type switch
            {
                "GET"  => await Get<T>(query),
                "POST" => await Post<T>(query),
                _      => throw new Exception()
            }; 
        }

        private async Task<T> Get<T>(Request query)
        {
            try
            {
                return await _client.GetFromJsonAsync<T>(query.Uri, _serializerOptions)
                    ?? throw new Exception();
            }
            catch
            {
                throw new Exception();
            }
        }
        private async Task<T> Post<T>(Request query)
        {
            try
            {
                var response = await _client.PostAsJsonAsync(query.Uri,query.Body,_serializerOptions)
                    ?? throw new Exception();

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>(_serializerOptions)
                    ?? throw new Exception("Empty Response");
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
