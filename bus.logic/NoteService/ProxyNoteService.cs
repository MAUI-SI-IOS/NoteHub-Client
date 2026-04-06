using bus.logic.ApiService;
using bus.logic.models;
using bus.logic.Result;
using bus.logic.service;
using System.Diagnostics;


namespace bus.logic.NoteService
{

    public class ProxyNoteService : INoteService, IDisposable 
    {
        private readonly INoteService _fallback;
        private readonly INoteService _upgrade;
        private bool IsUpgraded;
        public event Action<bool> OnStatusChanged;
        private INoteService Resolve => IsUpgraded ? _upgrade : _fallback;
        private CancellationTokenSource _cts;


        public ProxyNoteService(INoteService fallback, INoteService upgraded)
        {
            _upgrade = upgraded;
            _fallback = fallback;
            Monitor();
        }


        //--------------------//
        //    INoteService    //
        //--------------------//
        public async Task<Result<List<Note>, HttpException>> SearchNote(string token)
        {
            return await Resolve.SearchNote(token)
                .BindErrAsync(async (ex) => {
                    if (ex.code < 10)
                    {
                        Monitor();
                        return await _fallback.SearchNote(token);
                    }
                    return Result<List<Note>, HttpException>.Failure(ex);
                });
        }

        public async Task<Result<Note, HttpException>> GetNoteByTitle(string title)
        {
            return await Resolve.GetNoteByTitle(title)
                .BindErrAsync(async (ex) => {
                    if (ex.code < 10)
                    {
                        Monitor();
                        return await _fallback.GetNoteByTitle(title);
                    }
                    return Result<Note, HttpException>.Failure(ex);
                });
        }

        public async Task<Result<Note, HttpException>> CreateUpdateNote(long? id, string title, string note)
        {
            return await Resolve.CreateUpdateNote(id, title, note)
                .BindErrAsync(async (ex) => {
                    Debug.WriteLine($"[WAITING] {ex.code} {ex.msg}");
                    if (ex.code < 10)
                    {
                        Monitor();
                        return await _fallback.CreateUpdateNote(id, title, note);
                    }
                    return Result<Note, HttpException>.Failure(ex);
                });     
        }

        public Task<bool> Ping()
        {
            return Resolve.Ping();
        }
        

        private void Monitor()
        {
            UpdateStatus(false);
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            Task.Run(async () =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        Debug.WriteLine("[WAITING] for response");
                        var result = await _upgrade.Ping();
                        if (result)
                        {
                            UpdateStatus(true);
                            break;
                        }
                        await Task.Delay(2000, token);
                    }
                }
                catch (OperationCanceledException) { }
            }, token);
        }
        private void UpdateStatus(bool status)
        {
            IsUpgraded = status;
            OnStatusChanged?.Invoke(IsUpgraded);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
        ~ProxyNoteService()
        {
            Dispose();
        }
    }
}
