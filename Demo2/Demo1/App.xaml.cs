using Prism.Unity.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Prism.Windows.AppModel;
using Microsoft.Practices.Unity;
using Prism.Windows.Navigation;

namespace Demo1
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class App : PrismUnityApplication
	{
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
		}


		private Services.LocalStorageService _storage = null;

		protected async override Task OnInitializeAsync(IActivatedEventArgs args)
		{
			Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new Windows.ApplicationModel.Resources.ResourceLoader()));

			_storage = new Services.LocalStorageService();
			await _storage.Load();
			Container.RegisterInstance<Services.ITodoStorageService>(_storage);
			Container.RegisterType<Services.IMessageDialogService, Services.MessageDialogService>();

			await base.OnInitializeAsync(args);
		}

		protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{
			NavigationService.Navigate(PageTokens.MainPage, null);
			return Task.FromResult(true);
		}


		protected override UIElement CreateShell(Frame rootFrame)
		{
			var shell = Container.Resolve<AppShell>();
			shell.SetContentFrame(rootFrame);
			return shell;
			//return base.CreateShell(rootFrame);
		}

		protected async override Task OnSuspendingApplicationAsync()
		{
			await _storage.Save();
			await base.OnSuspendingApplicationAsync();
		}

		protected override INavigationService OnCreateNavigationService(IFrameFacade rootFrame)
		{
			return new Services.HamburgerFrameNavigationService(rootFrame, GetPageType, SessionStateService);
		}

		protected override void OnRegisterKnownTypesForSerialization()
		{
			base.OnRegisterKnownTypesForSerialization();
			SessionStateService.RegisterKnownType(typeof(ViewModels.AddPageViewModel));
		}
	}
}
