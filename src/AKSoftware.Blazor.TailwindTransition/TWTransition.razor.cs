using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.Blazor.TailwindTransition
{
    public partial class TWTransition : TWTransitionalComponentBase
    {

        [CascadingParameter]
        public TWTransitionalElement Parent { get; set; }

        public async Task Toggle()
        {
            await InvokeAsync(async () =>
            {
                if (Parent.IsOpened)
                    await ShowAsync();
                else
                    await HideAsync();
            });
        }

        protected override void OnInitialized()
        {
            Console.WriteLine("Initialized" + GetHashCode());
            Parent.AddTransition(this);
        }

    }
}
