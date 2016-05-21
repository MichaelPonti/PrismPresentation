Step 1

		protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{
			NavigationService.Navigate(PageTokens.MainPage, null);
			return Task.FromResult(true);
		}

		protected override Task OnInitializeAsync(IActivatedEventArgs args)
		{
			Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new Windows.ApplicationModel.Resources.ResourceLoader()));
			return base.OnInitializeAsync(args);
		}
