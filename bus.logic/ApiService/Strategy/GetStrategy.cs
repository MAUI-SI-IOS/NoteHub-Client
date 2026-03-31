using bus.logic.ApiService.Url;
using bus.logic.Result;
using System.Net.Http.Json;
using System.Text.Json;
using System.Diagnostics;

namespace bus.logic.ApiService.Directors
{
    internal class GetStrategy<TOutput>: IQueryStrategy<TOutput>
    {
        string route;
        public HttpClient _client { get; set; }
        public JsonSerializerOptions _serializerOptions { get; set; }
        public GetStrategy(HttpClient client, string route, JsonSerializerOptions options)
        {
            this._client = client;
            this._serializerOptions = options;
            this.route = route;
        }

        

        public async Task<Result<TOutput, HttpException>> DoQuery()
        {
            try
            {
                //Unit is Null as a type 
                if (typeof(TOutput) == typeof(Unit))
                {
                    var response = await _client.GetAsync(route);
                    response.EnsureSuccessStatusCode(); 
                    return Result<TOutput, HttpException>.Success((TOutput)(object)Unit.Default);
                }

                //if not unit then parse json
                var data = await _client.GetFromJsonAsync<TOutput>(route);
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
