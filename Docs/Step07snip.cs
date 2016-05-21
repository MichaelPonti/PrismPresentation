Step 7


		private Services.LocalStorageService _storageService = null;

		protected override Task OnInitializeAsync(IActivatedEventArgs args)
		{
			Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new Windows.ApplicationModel.Resources.ResourceLoader()));

			_storageService = new Services.LocalStorageService();
			Container.RegisterInstance<Services.ITodoStorageService>(_storageService);
			Container.RegisterType<Services.IMessageDialogService, Services.MessageDialogService>();

			return base.OnInitializeAsync(args);
		}
