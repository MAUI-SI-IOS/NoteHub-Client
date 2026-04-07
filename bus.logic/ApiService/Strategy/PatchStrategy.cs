using bus.logic.Result;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text.Json;

namespace bus.logic.ApiService.Directors
{
    internal class PatchStrategy<TInput, TOutput> : IQueryStrategy<TOutput>
    {
        readonly string route;
        TInput body;
        public HttpClient _client { get; set; }
        public JsonSerializerOptions _serializerOptions { get; set; }
        public PatchStrategy(HttpClient client, string route, TInput body, JsonSerializerOptions options)
        {
            this._client = client;
            this._serializerOptions = options;
            this.route = route;
            this.body = body;
        }



        public async Task<Result<TOutput, NoteServiceException>> DoQuery()
        {
            try
            {
                var response = await _client.PatchAsJsonAsync<TInput>(route, body, _serializerOptions);
                response.EnsureSuccessStatusCode();
                if (typeof(TOutput) == typeof(Unit))
                {
                    return Result<TOutput, NoteServiceException>.Success((TOutput)(object)Unit.Default);
                }

                var data = await response.Content.ReadFromJsonAsync<TOutput>(_serializerOptions);
                return Result<TOutput, NoteServiceException>.Success(data);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Http exception: {e.Message}");

                // Si on a un code (400, 404, 500...), on l'utilise.
                // Sinon (Serveur éteint/No Wi-Fi), on met 0 pour activer le Proxy.
                int code = e.StatusCode.HasValue ? (int)e.StatusCode.Value : 0;

                return Result<TOutput, NoteServiceException>.Failure(new NoteServiceException(code, e.Message));
            }
            catch (OperationCanceledException) // Pour gérer le Timeout (5s)
            {
                Debug.WriteLine("Timeout détecté !");
                return Result<TOutput, NoteServiceException>.Failure(new NoteServiceException(1, "Timeout"));
            }
            catch (Exception e)
            {
                // On ne met 500 que pour les erreurs inconnues, pas pour le réseau
                return Result<TOutput, NoteServiceException>.Failure(new NoteServiceException(500, e.Message));
            }
        }

    }
}
