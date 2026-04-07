using bus.logic.Result;
using System.Diagnostics;
using System.Net.Http.Json;
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
            this.body = body;
        }



        public async Task<Result<TOutput, HttpException>> DoQuery()
        {
            Debug.Write($"[QUERY] {this._client.BaseAddress.ToString()}, {this.route}");
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
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"[HTTP EXCEPTION]: {e.Message}");

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
