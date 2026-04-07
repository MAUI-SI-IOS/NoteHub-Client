using bus.logic.ApiService;
using bus.logic.models;
using bus.logic.Result;
using bus.logic.service;
using System.Diagnostics;


namespace bus.logic.NoteService
{

    public class ProxyNoteService : INoteService, IDisposable 
    {
        private readonly SoftZip _zip;
        
        private bool IsUpgraded;
        public event Action<bool> OnStatusChanged;

        private CancellationTokenSource _cts;


        public ProxyNoteService(INoteService fallback, INoteService upgraded)
        {
            _zip = new SoftZip(fallback, upgraded);
            Monitor();
        }


        //--------------------//
        //    INoteService    //
        //--------------------//
        public async Task<Result<List<Note>, HttpException>> SearchNote(string token)
        {
            return await _zip.SearchNote(token);        
        }

        public async Task<Result<Note, HttpException>> GetNoteByTitle(string title)
        {
            return await _zip.GetNoteByTitle(title);
        }

        public async Task<Result<Note, HttpException>> CreateUpdateNote(long? id, string title, string note)
        {
            //try to write localy if it works try to create if it works try to write
            return await _zip.CreateUpdateNote(id, title, note);
         }

        public Task<bool> Ping()
        {
            return _zip.Ping();
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
                        var result = await _zip.Ping();
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
