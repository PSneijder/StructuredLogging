using System;
using Microsoft.AspNet.SignalR.Client;
using StructuredLogging.DataContracts.Event;
using System.Configuration;

namespace StructuredLogging.Desktop.Utilities.Services.Clients
{
    sealed class HubClient
        : ConfigurationReaderBase
            , IHubClient
    {
        private readonly string _baseUrl;
        private IHubProxy _proxy;
        private HubConnection _connection;

        #region Message Event

        public event EventReceivedEventHandler EventReceived;

        private void OnEventReceived(RawEvent rawEvent)
        {
            var handler = EventReceived;
            handler?.Invoke(rawEvent);
        }

        public event EventsReceivedEventHandler EventsReceived;

        private void OnEventsReceived(RawEvents rawEvents)
        {
            var handler = EventsReceived;
            handler?.Invoke(rawEvents);
        }

        #endregion

        #region StateChange event

        public delegate void StateChangedEventHandler(StateChange state);
        public event StateChangedEventHandler StateChanged;

        private void OnStateChanged(StateChange change)
        {
            var handler = StateChanged;
            handler?.Invoke(change);
        }

        #endregion

        public HubClient(Configuration configuration)
            : base(configuration)
        {
            _baseUrl = ReadSetting("baseUrl");
        }

        public void Connect()
        {
            _connection = new HubConnection(_baseUrl);
            _proxy = _connection.CreateHubProxy("EventHub");

            _proxy.On("BroadcastEvent", new Action<RawEvent>(OnEventReceived));
            _proxy.On("BroadcastEvents", new Action<RawEvents>(OnEventsReceived));

            _connection.Start();
            _connection.StateChanged += OnStateChanged;
        }

        public void Disconnect()
        {
            _connection.Stop();
            _connection.Dispose();
        }
    }
}