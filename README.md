# AKSoftware.Blazor.TailwindTransition

AKSoftware.Blazor.TailwindTransition is a Blazor package to add support for the TailwindCSS & pre-built TailwindUI components transitions

The package contains 2 main components ***TWTransitionalElement*** & ***TWTransition***
The first component is the parent container for the full component that should be used to hide or show the full component, and the *TWTransition* is used to each div that needs to be transitioned
**Note:** You can detected which div is a TWTransition from the TailwindUI components through the comments on top of each div that supports Show/Hide Transition 

## Get Started 
- Install the Nuget package
For *.NET CLI*
```
	dotnet add package AKSoftware.Blazor.TailwindTransition
```
Or through the *Nuget Package Manager Console*
``` PS
	Install-Package AKSoftware.Blazor.TailwindTransition
```

- Then make sure you have Tailwind configured in your project, I highly recommend the following articles to get started:
[By Matt Ferderer - Using Tailwind CSS with Blazor](https://mattferderer.com/tailwind-with-blazor)
[By Chris Sanity -  Integrating Tailwind CSS with Blazor using Gulp - Part 1](https://chrissainty.com/integrating-tailwind-css-with-blazor-using-gulp-part-1/)

- In the **_imports.razor** import the namespace
  ``` Razor
  using AKSoftware.Blazor.TailwindTransition
  ```
Now you are just good to go, the following sample existing the in src/demo folder and in the component Index.razor
And basically it implements transition to show/hide a Slide-Over panel from the free TailwindUI components you can find it [here](https://tailwindui.com/components/application-ui/overlays/slide-overs) 

Now in your component file paste the HTML content from TailwindUI website and then transform the parent div into a TWTransitionalElement and bind the IsOpened Attribute to a boolean variable then inside it follow the comments from Tailwind so you can know which div should be transformed into TWTranstion and using the properties "**Entering,  EnteringFrom,  EnteringTo,  Leaving,  LeavingFrom,  LeavingTo,  Duration**"

The following code shows the full Index.razor component from the demo project

``` Razor
@page "/"
@using AKSoftware.Blazor.TailwindTransition

<h1 class="text-3xl">Welcome to AKSoftware.Blazor.TailwindTransition</h1>
<p>This library allows you to leverge the full power and capabilities of the TailwindCSS & TailwindUI components by providing you with the Transition component that you need to implement and take advantage of the smooth animation and Show/Hide transitions availabe out there</p>
<p style="color:red"><b>Note:</b> Make sure to add TailwindCSS in your project you can check the index.html file to add Tailwind using the CDN (not recommended) or check out the following links</p>
<ul>
    <li><a href="https://mattferderer.com/tailwind-with-blazor">Using Tailwind CSS with Blazor</a></li>
    <li><a href="https://chrissainty.com/integrating-tailwind-css-with-blazor-using-gulp-part-1/">Integrating Tailwind CSS with Blazor using Gulp - Part 1</a></li>
</ul>

<h3 class="text-3xl">Click here to check the transition for show/hide a side panel</h3>
<button @onclick="() => _isOpened = !_isOpened" type="button" class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
    @(_isOpened ? "Hide" : "Show")
</button>

<SurveyPrompt Title="How is Blazor working for you?" />

<!-- This example requires Tailwind CSS v2.0+ -->
<TWTransitionalElement @bind-IsOpened="_isOpened"
                       AdditionalClasses="fixed inset-0 overflow-hidden">
    <div class="absolute inset-0 overflow-hidden">
        <!--
          Background overlay, show/hide based on slide-over state.

          Entering: "ease-in-out duration-500"
            From: "opacity-0"
            To: "opacity-100"
          Leaving: "ease-in-out duration-500"
            From: "opacity-100"
            To: "opacity-0"
        -->
        <TWTransitionalElement @bind-IsOpened="_isOpened"
                               Entering="ease-in-out duration-500"
                               EnteringFrom="opacity-0"
                               EnteringTo="opacity-100"
                               Leaving="ease-in-out duration-500"
                               LeavingFrom="opacity-100"
                               LeavingTo="opacity-0"
                               Duration="500"
                               AdditionalClasses="absolute inset-0 bg-gray-500 bg-opacity-75 transition-opacity"></TWTransitionalElement>
        <section class="absolute inset-y-0 right-0 pl-10 max-w-full flex" aria-labelledby="slide-over-heading">
            <!--
              Slide-over panel, show/hide based on slide-over state.

              Entering: "transform transition ease-in-out duration-500 sm:duration-700"
                From: "translate-x-full"
                To: "translate-x-0"
              Leaving: "transform transition ease-in-out duration-500 sm:duration-700"
                From: "translate-x-0"
                To: "translate-x-full"
            -->
            <TWTransition Entering="transform transition ease-in-out duration-500 sm:duration-700"
                               EnteringFrom="translate-x-full"
                               EnteringTo="translate-x-0"
                               Leaving="transform transition ease-in-out duration-500 sm:duration-700"
                               LeavingFrom="translate-x-0"
                               LeavingTo="translate-x-full"
                               Duration="500"
                               AdditionalClasses="relative w-screen max-w-md">
                <!--
                  Close button, show/hide based on slide-over state.

                  Entering: "ease-in-out duration-500"
                    From: "opacity-0"
                    To: "opacity-100"
                  Leaving: "ease-in-out duration-500"
                    From: "opacity-100"
                    To: "opacity-0"
                -->
                <TWTransition Entering="ease-in-out duration-500"
                              EnteringFrom="opacity-0"
                              EnteringTo="opacity-100"
                              Leaving="ease-in-out duration-500"
                              LeavingFrom="opacity-100"
                              LeavingTo="opacity-0"
                              Duration="500"
                              AdditionalClasses="absolute top-0 left-0 -ml-8 pt-4 pr-2 flex sm:-ml-10 sm:pr-4">
                    <button  @onclick="() => _isOpened = !_isOpened" class="rounded-md text-gray-300 hover:text-white focus:outline-none focus:ring-2 focus:ring-white">
                        <span class="sr-only">Close panel</span>
                        <!-- Heroicon name: outline/x -->
                        <svg class="h-6 w-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
                        </svg>
                    </button>
                </TWTransition>
                <div class="h-full flex flex-col py-6 bg-white shadow-xl overflow-y-scroll">
                    <div class="px-4 sm:px-6">
                        <h2 id="slide-over-heading" class="text-lg font-medium text-gray-900">
                            Panel title
                        </h2>
                    </div>
                    <div class="mt-6 relative flex-1 px-4 sm:px-6">
                        <!-- Replace with your content -->
                        <div class="absolute inset-0 px-4 sm:px-6">
                            <div class="h-full border-2 border-dashed border-gray-200" aria-hidden="true"></div>
                        </div>
                        <!-- /End replace -->
                    </div>
                </div>
            </TWTransition>
        </section>
    </div>
</TWTransitionalElement>

@code 
{

    private bool _isOpened = false; 


}
```

The full documentation will be enhanced soon thank
Built with love by [AK Ahmad Mozaffar](https://ahmadmozaffar)
