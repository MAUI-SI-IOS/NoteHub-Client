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
                var data = await _client.GetFromJsonAsync<TOutput>(route, _serializerOptions);
                return Result<TOutput, HttpException>.Success(data);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Http exception: {e.Message}");

                // Si on a un code (400, 404, 500...), on l'utilise.
                // Sinon (Serveur éteint/No Wi-Fi), on met 0 pour activer le Proxy.
                int code = e.StatusCode.HasValue ? (int)e.StatusCode.Value : 0;

                return Result<TOutput, HttpException>.Failure(new HttpException(code, e.Message));
            }
            catch (OperationCanceledException) // Pour gérer le Timeout (5s)
            {
                Debug.WriteLine("Timeout détecté !");
                return Result<TOutput, HttpException>.Failure(new HttpException(1, "Timeout"));
            }
            catch (Exception e)
            {
                // On ne met 500 que pour les erreurs inconnues, pas pour le réseau
                return Result<TOutput, HttpException>.Failure(new HttpException(500, e.Message));
            }
        }
    }
}
