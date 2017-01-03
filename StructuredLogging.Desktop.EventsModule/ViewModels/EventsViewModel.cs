using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Desktop.Utilities.Extensions;
using StructuredLogging.Desktop.Utilities.Models.Filter;
using StructuredLogging.Desktop.Utilities.Services;
using StructuredLogging.Desktop.Utilities.Services.Clients;
using StructuredLogging.Desktop.Utilities.ViewModels;

namespace StructuredLogging.Desktop.EventsModule.ViewModels
{
    public sealed class EventsViewModel
        : ViewModelBase
    {
        #region Fields

        private readonly IDialogService _dialogService;
        private readonly IServiceClient _client;
        private readonly IHubClient _hubClient;
        private DateTime? _selectedStartDate;
        private DateTime? _selectedEndDate;
        private TimeSpan? _selectedStartTime;
        private TimeSpan? _selectedEndTime;

        private DateTime _minStartDate;
        private DateTime _maxStartDate;
        private string _queryText;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand FilterCommand { get; }

        #endregion

        #region Properties

        public CultureInfo CurrentCulture { get; set; }

        public string QueryText
        {
            get { return _queryText; }
            set { SetPropertyChanged(ref _queryText, value); }
        }

        public ObservableCollection<FilterGroup> QueryFilters { get; }

        public ObservableCollection<SearchResultItem> SearchResults { get; }
        public SearchResultItem SelectedSearchResult { get; set; }

        public DateTime MinStartDate
        {
            get { return _minStartDate; }
            set { SetPropertyChanged(ref _minStartDate, value); }
        }
        public DateTime MaxStartDate
        {
            get { return _maxStartDate; }
            set { SetPropertyChanged(ref _maxStartDate, value); }
        }
        public DateTime? SelectedStartDate
        {
            get { return _selectedStartDate; }
            set { SetPropertyChanged(ref _selectedStartDate, value); }
        }
        public TimeSpan? SelectedStartTime
        {
            get { return _selectedStartTime; }
            set { SetPropertyChanged(ref _selectedStartTime, value); }
        }
        public DateTime? SelectedEndDate
        {
            get { return _selectedEndDate; }
            set { SetPropertyChanged(ref _selectedEndDate, value); }
        }
        public TimeSpan? SelectedEndTime
        {
            get { return _selectedEndTime; }
            set { SetPropertyChanged(ref _selectedEndTime, value); }
        }

        #endregion

        public EventsViewModel(IShellService shellService, IDialogService dialogService, IServiceClient client, IHubClient hubClient)
        {
            _dialogService = dialogService;
            _client = client;
            _hubClient = hubClient;

            SearchCommand = new DelegateCommand<string>(OnSearch);
            ClearCommand = new DelegateCommand<string>(OnSearch);
            RefreshCommand = new DelegateCommand(OnRefresh);
            FilterCommand = new DelegateCommand<FilterItem>(OnFilter);

            SearchResults = new ObservableCollection<SearchResultItem>();
            SearchResults.CollectionChanged += (sender, args) => RaisePropertyChanged("SearchResults");

            QueryFilters = new ObservableCollection<FilterGroup>();
            QueryFilters.CollectionChanged += (sender, args) => RaisePropertyChanged("QueryFilters");

            OnInitialize();

            shellService.ChangeWindowSize(600, 1024);
        }

        ~EventsViewModel()
        {
            _dialogService.DialogClosed -= OnGetRawEvents;

            _hubClient.Disconnect();
        }

        private void OnInitialize()
        {
            _dialogService.DialogClosed += OnGetRawEvents;

            var culture = new CultureInfo("en-US");
            CurrentCulture = culture;

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            OnGetRawEvents();

            _hubClient.EventReceived += async item =>
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    OnAddSearchResultItem(item);
                });
            };

            _hubClient.EventsReceived += async items =>
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    foreach (var item in items)
                    {
                        OnAddSearchResultItem(item);
                    }
                });
            };

            _hubClient.Connect();
        }

        private void OnAddSearchResultItem(SearchResultItem item)
        {
            SearchResults.Add(item);

            SearchResults.Sort();
        }

        private async void OnGetRawEvents()
        {
            IsBusy = true;

            var result = await _client.Search(new SearchRequest(string.Empty));

            SearchResults.Clear();
            foreach (var item in result.Items)
            {
                SearchResults.Add(item);
            }

            QueryFilters.Clear();
            foreach (var group in result.Groups)
            {
                QueryFilters.Add(group);
            }

            MinStartDate = result.Items.Any() ? result.Items.Min(p => p.Timestamp) : DateTime.MinValue;
            MaxStartDate = result.Items.Any() ? result.Items.Max(p => p.Timestamp) : DateTime.MaxValue;

            SelectedStartDate = SearchResults.Any() ? SearchResults.Min(p => p.Timestamp) : MinStartDate;
            SelectedEndDate = SearchResults.Any() ? SearchResults.Max(p => p.Timestamp) : MaxStartDate;

            IsBusy = false;
        }
        
        private async void OnSearch(string phrase)
        {
            IsBusy = true;

            await Search(phrase);

            IsBusy = false;
        }

        private async void OnFilter(FilterItem parameter)
        {
            IsBusy = true;

            await Search(QueryText, parameter);

            IsBusy = false;
        }

        private async Task Search(string phrase, FilterItem parameter = null)
        {
            var filters = QueryFilters
                .SelectMany(p => p.Filters)
                    .Where(p => p.IsChecked)
                        .Select(p => p.Item)
                            .ToList();

            if (parameter != null && parameter.IsChecked)
            {
                var filter = parameter.Item;

                if(!filters.Contains(filter))
                { 
                    filters.Add(new QueryFilterItem(filter.Name, filter.Value, filter.HitCount));
                }
            }

            SearchResult result;
            if (SelectedStartDate.HasValue && SelectedEndDate.HasValue)
            {
                result = await _client.Search(new SearchRequest(phrase, filters, SelectedStartDate.Value, SelectedEndDate.Value));
            }
            else
            {
                result = await _client.Search(new SearchRequest(string.Empty));
            }

            SearchResults.Clear();
            foreach (var item in result.Items)
            {
                SearchResults.Add(item);
            }

            RefreshFilters(QueryFilters.SelectMany(p => p.Filters).Where(p => p.IsChecked).ToArray(), result.Groups);
        }

        private void RefreshFilters(FilterItem[] activeFilters, QueryFilterGroup[] groups)
        {
            QueryFilters.Clear();
            foreach (var group in groups)
            {
                QueryFilters.Add(group);
            }

            foreach (var group in QueryFilters)
            {
                foreach (var filter in group.Filters)
                {
                    FilterItem item = filter;

                    if (activeFilters.FirstOrDefault(a => a.Item.Equals(item.Item)) != null)
                    {
                        item.IsChecked = true;
                    }
                }
            }
        }

        private void OnRefresh()
        {
            QueryText = string.Empty;

            OnGetRawEvents();
        }
    }
}