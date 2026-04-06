using bus.logic.models;
using bus.logic.NoteService;
using bus.logic.Result;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NoteHub_Client.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NoteHub_Client.ViewModels;

public partial class SearchNoteViewModel : ObservableObject
{
    [ObservableProperty]
    string _search;
    [ObservableProperty]
    ObservableCollection<Note> list;
    [ObservableProperty]
    string statusMessage;

    readonly INoteService service;
    CancellationTokenSource? _taskToken;

    public SearchNoteViewModel(INoteService service)
    {
        this.service = service;
        List = new ObservableCollection<Note>();
        if (service is ProxyNoteService proxy)
        {
            proxy.OnStatusChanged += HandleStatusChanged;
        }
    }
    partial void OnSearchChanged(string value) => SearchCommand.Execute(value);


    [RelayCommand]
    public async Task SearchAsync(string token)
    {
        _taskToken?.Cancel();
        if (String.IsNullOrEmpty(token)) { return; };
        
        //creating delay in between input
        _taskToken = new CancellationTokenSource();

        try
        {
            await Task.Delay(300, _taskToken.Token);

            var result = await service.SearchNote(token);
            result?.Match(
                ok: (notes) =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        List = new ObservableCollection<Note>(notes);
                        StatusMessage = "";
                    });
                },
                err: (ex) => StatusMessage = ex.msg
            );
        }
        catch (OperationCanceledException) { }
        catch (Exception e)
        {
            Debug.WriteLine($"[CATCHED ERROR], {e.Message}");
            StatusMessage = "[Internal Error], please try againg later";
        }
    }

    [RelayCommand]
    public async Task OnSelectedNote(Note note)
    {
        try
        {
            if (note == null) return;

            var navigationParameter = new Dictionary<string, object>
        {
            { NoteDetailsPage.NOTE_TITLE_QUERY_KEY, note }
        };
            await Shell.Current.GoToAsync(nameof(NoteDetailsPage), navigationParameter);
        }
        catch(Exception e)
        {
            Debug.WriteLine($"[ON SELECT] {e.Message}");
        }
    }

    private void HandleStatusChanged(bool isUpgraded)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            string msg = isUpgraded
                ? "Connecté au serveur Cloud"
                : "Mode Hors-ligne (Local)";

            if (isUpgraded)
            {
                await DisplayService.ShowSuccess(msg, null);
            }
            else
            {
                await DisplayService.ShowFailure(msg, null);
            }
        });
    }
}

