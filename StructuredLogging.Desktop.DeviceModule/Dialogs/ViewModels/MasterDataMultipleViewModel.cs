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
    public sealed class MasterDataMultipleViewModel
        : DialogViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IServiceClient _client;

        //private ControlRequestDto _controlRequest;
        //private DeviceDto _device;
        private bool _isNew;

        public ICommand CloseDialogCommand { get; private set; }
        public ICommand MoveCommand { get; private set; }
        public ICommand AddMoveCommand { get; private set; }

        //public ObservableCollection<ControlRequestDto> ControlRequests { get; set; }
        //public ControlRequestDto ControlRequest { get { return _controlRequest; } set { SetPropertyChanged(ref _controlRequest, value); } }
        //public DeviceDto Device { get { return _device; } set { SetPropertyChanged(ref _device, value); } }
        public bool IsNew { get { return _isNew; } set { SetPropertyChanged(ref _isNew, value); } }

        public MasterDataMultipleViewModel(IDialogService dialogService, IServiceClient client)
        {
            _dialogService = dialogService;
            _client = client;

            //ControlRequests = new ObservableCollection<ControlRequestDto>();
            //ControlRequests.CollectionChanged += (sender, args) => RaisePropertyChanged("ControlRequests");

            CloseDialogCommand = new DelegateCommand(OnCloseDialog);
            MoveCommand = new DelegateCommand(OnMove);
            AddMoveCommand = new DelegateCommand(OnAddMove);

            Actions = new ObservableCollection<DialogActionCommand>
            {
                new DialogActionCommand { Command = MoveCommand, Content = "Move", IsDefault = false, IsEnabled = true, IsVisible = true },
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

        private void OnAddMove()
        {
            ClearSummary();

            //ControlRequests.Add(
            //    new ControlRequestDto
            //    {
            //        Id = ControlRequest.Id,
            //        Text1 = ControlRequest.Text1,
            //        Text2 = ControlRequest.Text2,
            //        Text3 = ControlRequest.Text3,
            //        Text4 = ControlRequest.Text4,
            //        Confirm = ControlRequest.Confirm
            //    });

            //ControlRequest = new ControlRequestDto();
        }

        private async void OnMove()
        {
            IsBusy = true;

            //var responses = await _client.Move(ControlRequests);
            //if (responses != null)
            //{
            //    HasErrors = true;
            //    ValidationSummary = string.Join(Environment.NewLine, responses.Select(p => p.ResponseMessage));
            //}
            //else
            //{
            //    ClearSummary();
            //}

            IsBusy = false;
        }

        private void ClearSummary()
        {
            HasErrors = false;
            ValidationSummary = string.Empty;
        }

        private async void OnCloseDialog()
        {
            await _dialogService.HideDialogAsync<MasterDataMultipleView>();
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