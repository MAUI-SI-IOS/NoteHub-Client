using bus.logic.NoteService;
using bus.logic.Result;
using bus.logic.service;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Extensions.Logging;
using NoteHub_Client.Services.Config;
using NoteHub_Client.ViewModels;


namespace NoteHub_Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() //adds snackbar/toast
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<INoteHubConfigService, NoteHubConfigService>();
            //check if config changed and use the appropriate service
            builder.Services.AddTransient<INoteService>(container =>
            {
                var config = container.GetRequiredService<INoteHubConfigService>();

                return config.Client.Match<HttpClient, string, INoteService>(
                    ok: (client) => new ServerNoteService(client),
                    err:(dbPath) => new LocalNoteService(dbPath)
                );
            });

            
            builder.Services.AddTransient<WriteNotePage>();
            builder.Services.AddTransient<WriteNoteViewModel>();

            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<SearchNoteViewModel>();

            builder.Services.AddTransient<ServerSelectionPage>();
            builder.Services.AddTransient<ServerSelectionViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
