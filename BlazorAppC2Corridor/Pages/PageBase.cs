using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorAppC2Corridor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorAppC2Corridor.Pages
{
    public class PageBase : ComponentBase, IDisposable
    {
        [Inject]
        public IDbContextFactory<ApplicationDbContext>? DbContextFactory { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime JS { get; set; }
        protected ApplicationDbContext? Context { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Context = await DbContextFactory.CreateDbContextAsync();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected ClaimsPrincipal? CurrentUser
        {
            get
            {
                var authState = AuthenticationStateTask.Result;
                var user = authState.User;
                return user;
            }
        }
    }
}
