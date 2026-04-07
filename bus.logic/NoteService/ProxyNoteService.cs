using bus.logic.ApiService;
using bus.logic.models;
using bus.logic.Result;
using bus.logic.service;
using System.Data;
using System.Diagnostics;


namespace bus.logic.NoteService
{

    public class ProxyNoteService : INoteService
    {
        private readonly SoftZip _zip;
        private bool _isOnline = true;
        public event Action<bool> OnStatusChanged;

        public ProxyNoteService(INoteService fallback, INoteService upgraded)
        {
            _zip = new SoftZip(fallback, upgraded);
            _zip.OnStatusChanged += UpdateStatus;
        }
        private void UpdateStatus(bool status)
        {
            if (_isOnline != status)
            {
                _isOnline = status;
                OnStatusChanged?.Invoke(_isOnline);
            }
        }

        //--------------------//
        //    INoteService    //
        //--------------------//
        public async Task<Result<List<INote>, NoteServiceException>> SearchNote(string token)
        => await _zip.SearchNote(token);
        public async Task<Result<INote, NoteServiceException>> GetNoteByTitle(string title)
        => await _zip.GetNoteByTitle(title);
        public async Task<Result<INote, NoteServiceException>> CreateUpdateNote(long? id, string title, string note)
        => await _zip.CreateUpdateNote(id, title, note);
        public Task<bool> Ping() => _zip.Ping();
    }
}
