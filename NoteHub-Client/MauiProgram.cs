using bus.logic.NoteService;
using bus.logic.service;
using CommunityToolkit.Maui;
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
                .UseMauiCommunityToolkit() 
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<INoteHubConfigService, NoteHubConfigService>();
            //check if config changed and use the appropriate service
            builder.Services.AddSingleton<INoteService>(container =>
            {
                var config = container.GetRequiredService<INoteHubConfigService>();
                var local  = new LocalNoteService(config.LocalDb);
                var remote = new ServerNoteService(config.Client);              
                return new ProxyNoteService(local,remote);
            });
            builder.Services.AddTransient<SearchNoteViewModel>();
            builder.Services.AddTransient<SearchPage>();

            builder.Services.AddTransient<ServerSelectionViewModel>();
            builder.Services.AddTransient<ServerSelectionPage>();

            builder.Services.AddTransient<NoteDetailsViewModel>();
            builder.Services.AddTransient<NoteDetailsPage>();

            Routing.RegisterRoute(nameof(NoteDetailsPage), typeof(NoteDetailsPage));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
