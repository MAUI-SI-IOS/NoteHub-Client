using bus.logic.ApiService.Directors;
using bus.logic.ApiService.Url;
using bus.logic.Result;
using bus.logic.service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace bus.logic.ApiService
{
    public class Api
    {
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




        /// <summary>
        /// Sends a request to server
        /// </summary>
        /// <typeparam name="T">will cast T into the response object</typeparam>
        /// <returns>Task<T></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<T,Exception>> Send<T>(IQueryDirector<T> director)
        {
            System.Diagnostics.Debug.WriteLine($"[Search] about to make a search");
            var query = director.MakeQuery();
            return query.Type switch
            {
                "GET"  => await Get<T>(query),
                "POST" => await Post<T>(query),
                //"WS"   => await ConnectWebSocket(query),
                _ => throw new Exception("yo")
            }; 
        }

        private async Task<Result<T,Exception>> Get<T>(Request query)
        {
            try
            {
                var response = await _client.GetAsync(query.Uri)
                    ?? throw new Exception();
                //try to read the content else it returns null instead of crashing
                var data = await response.Content.ReadFromJsonAsync<T>()
                    ?? throw new Exception("Internal Error, couldn't parse response");
                return Result<T,Exception>.Success(data);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"[API Error]: {e.Message}");
                return Result<T, Exception>.Failure(e);
            }
        }
        private async Task<Result<T,Exception>> Post<T>(Request query)
        {
            try
            {
                var response = await _client.PostAsJsonAsync(query.Uri,query.Body,_serializerOptions)                  
                    ?? throw new Exception();

                var data = await response.Content.ReadFromJsonAsync<T>()
                   ?? throw new Exception("Internal Error, couldn't parse response");
                return Result<T, Exception>.Success(data);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"[API Error]: {e.Message}");
                return Result<T, Exception>.Failure(e);
            }
        }

        //private async Task<ClientWebSocket> ConnectWebSocketd(Request query)
        //{
        //    var client = new ClientWebSocket();
        //    await client.ConnectAsync(new Uri(query.Uri), CancellationToken.None);
        //    return client;
        //}
    }
}
