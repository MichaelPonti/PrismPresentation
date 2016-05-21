		protected override UIElement CreateShell(Frame rootFrame)
		{
			var shell = Container.Resolve<AppShell>();
			shell.SetContentFrame(rootFrame);
			return shell;
			//return base.CreateShell(rootFrame);
		}
	
	
	
	
	
	
	public class AboutPageViewModel : AppViewModelBase
	{
		public AboutPageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader, INavigationService navigationService)
			: base(messageBus, resourceLoader, navigationService, null)
		{
			/// don't need the storage service in here so we just don't bother with
			/// it.
			/// 

			Title = "This is the About Page";
		}


		private string _title = null;
		public string Title
		{
			get { return _title; }
			set { SetProperty<string>(ref _title, value); }
		}
	}



	public class SettingsPageViewModel : AppViewModelBase
	{
		public SettingsPageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader, INavigationService navigationService)
			: base(messageBus, resourceLoader, navigationService, null)
		{
			/// don't need the storage service in here so we just don't bother with
			/// it.
			/// 

			Title = "This is the settings Page";
		}


		private string _title = null;
		public string Title
		{
			get { return _title; }
			set { SetProperty<string>(ref _title, value); }
		}
	}



<prismMvvm:SessionStateAwarePage
	x:Class="Demo1.Views.AboutPage"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:local="using:Demo1.Views"
	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	xmlns:prismMvvm="using:Prism.Windows.Mvvm"
	prismMvvm:ViewModelLocator.AutoWireViewModel="True"
	mc:Ignorable="d">

	<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
		<TextBlock Text="{Binding Path=Title,Mode=TwoWay}" HorizontalAlignment="Center" VerticalAlignment="Center" />
	</Grid>
</prismMvvm:SessionStateAwarePage>


	public sealed partial class AboutPage : SessionStateAwarePage, INotifyPropertyChanged
	{
		public AboutPage()
		{
			this.InitializeComponent();

			DataContextChanged += (s, e) =>
			 {
				 var propertyChanged = PropertyChanged;
				 if (propertyChanged != null)
					 propertyChanged(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
			 };
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public ViewModels.AboutPageViewModel ConcreteDataContext
		{
			get { return DataContext as ViewModels.AboutPageViewModel; }
		}
	}


<prismMvvm:SessionStateAwarePage
	x:Class="Demo1.Views.SettingsPage"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:local="using:Demo1.Views"
	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	xmlns:prismMvvm="using:Prism.Windows.Mvvm"
	prismMvvm:ViewModelLocator.AutoWireViewModel="True"
	mc:Ignorable="d">

	<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
		<TextBlock Text="{Binding Path=Title,Mode=TwoWay}" HorizontalAlignment="Center" VerticalAlignment="Center" />
	</Grid>
</prismMvvm:SessionStateAwarePage>


	public sealed partial class SettingsPage : SessionStateAwarePage, INotifyPropertyChanged
	{
		public SettingsPage()
		{
			this.InitializeComponent();

			DataContextChanged += (s, e) =>
			{
				var propertyChanged = PropertyChanged;
				if (propertyChanged != null)
					propertyChanged(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;


		public ViewModels.SettingsPageViewModel ConcreteDataContext
		{
			get { return DataContext as ViewModels.SettingsPageViewModel; }
		}
	}
