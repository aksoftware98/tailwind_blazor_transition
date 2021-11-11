using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.Blazor.TailwindTransition
{
    public partial class TWTransitionalElement : TWTransitionalComponentBase
    {

        [Parameter]
        public bool IsOpened { get; set; }

        [Parameter]
        public EventCallback<bool> IsOpenedChanged { get; set; }

        private bool _oldStatus = false;
        private List<TWTransition> _transitions = null;

        private bool _isFirstRender = true;

        protected async override Task OnParametersSetAsync()
        {
            if (_oldStatus != IsOpened && !_isFirstRender)
            {
                _oldStatus = IsOpened;
                if (IsOpened)
                    SetHiddenClass();
                await ToggleAsync();
            }
            else
                SetHiddenClass();
        }

        protected override void OnInitialized()
        {
            _oldStatus = IsOpened;
            _transitions = new List<TWTransition>();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _isFirstRender = false;
                return;
            }
        }

        private string _hiddenClass = "";
        private bool _isTransitional => !string.IsNullOrWhiteSpace(Entering) || !string.IsNullOrWhiteSpace(Leaving);
        public async Task Toggle()
        {
            await InvokeAsync(async () =>
            {
                if (IsOpened)
                    await ShowAsync();
                else
                    await HideAsync();
            });
        }

        internal async Task ToggleAsync()
        {
            var tasks = new List<Task>();
            if (_isTransitional)
                tasks.Add(Toggle());
            foreach (var item in _transitions)
            {
                tasks.Add(item.Toggle());
            }
            //Console.WriteLine(tasks.Count());
            // Run all the tasks
            await Task.WhenAll(tasks);

            await IsOpenedChanged.InvokeAsync(IsOpened);

            SetHiddenClass();
        }

        internal void AddTransition(TWTransition transition)
        {
            _transitions.Add(transition);
        }

        private void SetHiddenClass()
        {
            _hiddenClass = IsOpened ? "" : "hidden";
            StateHasChanged();
        }
    }
}
