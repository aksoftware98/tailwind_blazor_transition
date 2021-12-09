using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AKSoftware.Blazor.TailwindTransition
{
    public class TWTransitionalComponentBase : ComponentBase
    {

        [Parameter]
        public string Entering { get; set; } = "";

        [Parameter]
        public string EnteringFrom { get; set; } = "";

        [Parameter]
        public string EnteringTo { get; set; } = "";

        [Parameter]
        public int Duration { get; set; }

        [Parameter]
        public string Leaving { get; set; } = "";

        [Parameter]
        public string LeavingFrom { get; set; } = "";

        [Parameter]
        public string LeavingTo { get; set; } = "";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string AdditionalClasses { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }

        protected StringBuilder TransitionClasses = new StringBuilder();

        protected async Task ShowAsync()
        {
            if (string.IsNullOrWhiteSpace(Entering) && string.IsNullOrWhiteSpace(EnteringFrom) && string.IsNullOrWhiteSpace(EnteringTo))
                return;

            var durationMatch = Regex.Match(Entering, @"duration-(\d+)");
            if (durationMatch.Success)
                Duration = Convert.ToInt32(durationMatch.Groups[1].Value);

            TransitionClasses.Append(Entering);
            TransitionClasses.Append($" {EnteringFrom} ");
            await Task.Delay(Duration / 10);
            TransitionClasses.Replace(EnteringFrom, EnteringTo);
            await Task.Delay(Duration / 10);
        }

        protected async Task HideAsync()
        {
            var durationMatch = Regex.Match(Leaving, @"duration-(\d+)");
            if (durationMatch.Success)
                Duration = Convert.ToInt32(durationMatch.Groups[1].Value);

            if (Entering != Leaving && !string.IsNullOrWhiteSpace(Entering))
                TransitionClasses.Replace(Entering, Leaving);
            else
                TransitionClasses.Append(Leaving);
            if (EnteringTo != LeavingFrom && !string.IsNullOrWhiteSpace(EnteringTo))
                TransitionClasses.Replace(EnteringTo, LeavingFrom);
            else
                TransitionClasses.Append($" {LeavingFrom}");
            TransitionClasses.Replace(LeavingFrom, LeavingTo);
            await Task.Delay(Duration);

            TransitionClasses.Clear();
        }

    }
}
