using bus.logic.NoteService;
using bus.logic.Result;
using bus.logic.service;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Security.Cryptography.X509Certificates;

namespace NoteHub_Client.ViewModels;

public partial class SearchNoteViewModel : ObservableObject
{
    [ObservableProperty]
    string _search;   
    [ObservableProperty]
    List<Note> list;
    [ObservableProperty]
    string statusMessage;

    readonly INoteService service;
    CancellationTokenSource? _taskToken;

    public SearchNoteViewModel(INoteService service)
    {
        this.service = service;
        list = new List<Note>();
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
                ok: (list) =>
                {
                    List = list;
                    StatusMessage = "";
                },
                err: (msg) =>
                {
                    StatusMessage = msg;
                }
            );
        }
        catch (OperationCanceledException) { }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"[Search error] {e.Message}");
            StatusMessage = "[Internal Error] please try againg later";
        }
    }
    [RelayCommand]
    public async void OnSelectedNoteCommand()
    {

    }
    [RelayCommand]
    public async Task OnAppearing()
    {
        if (service is LocalNoteService)
        {
            var snackbar = Snackbar.Make(
                message: "No connection found, offline mode",
                duration: TimeSpan.FromSeconds(3),
                visualOptions: new SnackbarOptions { BackgroundColor = Colors.Red });

            await snackbar.Show();
        }
    }
}

