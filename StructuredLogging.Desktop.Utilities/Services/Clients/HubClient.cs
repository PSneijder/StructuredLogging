using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using StructuredLogging.DataContracts.Event;
using System.Configuration;
using StructuredLogging.DataContracts;

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

        private void OnEventReceived(SearchResultItem item)
        {
            var handler = EventReceived;
            handler?.Invoke(item);
        }

        public event EventsReceivedEventHandler EventsReceived;

        private void OnEventsReceived(IEnumerable<SearchResultItem> items)
        {
            var handler = EventsReceived;
            handler?.Invoke(items);
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

            _proxy.On("BroadcastEvent", new Action<SearchResultItem>(OnEventReceived));
            _proxy.On("BroadcastEvents", new Action<IEnumerable<SearchResultItem>>(OnEventsReceived));

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