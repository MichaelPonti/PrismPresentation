Step 3 first view model

	public class MainPageViewModel : AppViewModelBase
	{
		public MainPageViewModel(IEventAggregator messageBus, IResourceLoader resourceLoader,
			INavigationService navigationService)
			: base(messageBus, resourceLoader, navigationService, null)
		{
		}

		private string _testText = "test text";
		public string TestText
		{
			get { return _testText; }
			set { SetProperty<string>(ref _testText, value); }
		}
	}




	xmlns:prismMvvm="using:Prism.Windows.Mvvm"
	prismMvvm:ViewModelLocator.AutoWireViewModel="True"
	
	prismMvvm:SessionStateAwarePage

		<Grid.RowDefinitions>
			<RowDefinition Height="Auto" />
		</Grid.RowDefinitions>
	<TextBox Text="{Binding TestText,Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}" />


	public sealed partial class MainPage : SessionStateAwarePage, INotifyPropertyChanged
	{
		public MainPage()
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

		public ViewModels.MainPageViewModel ConcreteDataContext
		{
			get { return DataContext as ViewModels.MainPageViewModel; }
		}
	}



