using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using StructuredLogging.Desktop.EventsModule.Dialogs.Views;
using StructuredLogging.Desktop.Utilities.Models;
using StructuredLogging.Desktop.Utilities.Services;
using StructuredLogging.Desktop.Utilities.Services.Clients;
using StructuredLogging.Desktop.Utilities.ViewModels;

namespace StructuredLogging.Desktop.EventsModule.Dialogs.ViewModels
{
    public sealed class MasterDataViewModel
        : DialogViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IServiceClient _client;

        //private ControlRequestDto _controlRequest;
        //private DeviceDto _device;
        private bool _isNew;

        public ICommand CloseDialogCommand { get; private set; }
        public ICommand StatusCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand MovesCommand { get; private set; }
        public ICommand MoveCommand { get; private set; }

        //public ControlRequestDto ControlRequest { get { return _controlRequest; } set { SetPropertyChanged(ref _controlRequest, value); } }
        //public DeviceDto Device { get { return _device; } set { SetPropertyChanged(ref _device, value); } }
        public bool IsNew { get { return _isNew; } set { SetPropertyChanged(ref _isNew, value); } }

        public MasterDataViewModel(IDialogService dialogService, IServiceClient client)
        {
            _dialogService = dialogService;
            _client = client;

            CloseDialogCommand = new DelegateCommand(OnCloseDialog);
            StatusCommand = new DelegateCommand(OnStatus);
            ClearCommand = new DelegateCommand(OnClear);
            MovesCommand = new DelegateCommand(OnMoves);
            MoveCommand = new DelegateCommand(OnMove);

            Actions = new ObservableCollection<DialogActionCommand>
            {
                new DialogActionCommand { Command = StatusCommand, Content = "Status", IsDefault = false, IsEnabled = true, IsVisible = true },
                new DialogActionCommand { Command = MoveCommand, Content = "Move", IsDefault = false, IsEnabled = true, IsVisible = true },
                new DialogActionCommand { Command = MovesCommand, Content = "Moves", IsDefault = false, IsEnabled = true, IsVisible = true },
                new DialogActionCommand { Command = ClearCommand, Content = "Clear", IsDefault = false, IsEnabled = true, IsVisible = true },
                new DialogActionCommand { Command = CloseDialogCommand, Content = "Cancel", IsDefault = true, IsEnabled = true, IsVisible = true }
            };

            OnInitialize(true);
        }

        private void OnInitialize(bool isNew)
        {
            IsNew = isNew;

            Title = "Manage Device";
            SubTitle = "Here you can manage your device.";
        }

        private async void OnStatus()
        {
            IsBusy = true;

            //ControlResponseDto response = await _client.Status();
            //if (response != null)
            //{
            //    HasErrors = true;
            //    ValidationSummary = response.ResponseMessage;
            //}
            //else
            //{
            //    ClearSummary();
            //}

            IsBusy = false;
        }

        private async void OnMove()
        {
            IsBusy = true;

            ClearSummary();

            //var response = await _client.Move(ControlRequest.Id, ControlRequest);
            //if (response != null)
            //{
            //    HasErrors = true;
            //    ValidationSummary = response.ResponseMessage;
            //}
            //else
            //{
            //    HasErrors = false;
            //    ValidationSummary = string.Empty;
            //}

            IsBusy = false;
        }

        private async void OnMoves()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "Device", /*Device*/ null }
            };

            await _dialogService.ShowDialogAsync<MasterDataMultipleView>(parameters);
        }

        private async void OnClear()
        {
            IsBusy = true;

            //await _client.Clear();

            //ClearSummary();

            IsBusy = false;
        }

        private void ClearSummary()
        {
            HasErrors = false;
            ValidationSummary = string.Empty;
        }

        private async void OnCloseDialog()
        {
            await _dialogService.HideDialogAsync<MasterDataView>();
        }

        public override string SubTitle { get; protected set; }

        public override IEnumerable<DialogActionCommand> Actions { get; protected set; }

        public override void OnNavigatedTo(IDictionary<string, object> parameters)
        {
            //var device = parameters.GetParameter<DeviceDto>("Device");

            //if (device != null)
            //{
            //    Device = device;
            //    ControlRequest = new ControlRequestDto();

            //    OnInitialize(false);
            //}
            //else
            //{
            //    OnInitialize(true);
            //}
        }
    }
}