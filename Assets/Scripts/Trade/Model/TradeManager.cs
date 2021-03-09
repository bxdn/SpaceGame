using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Assets.Scripts.Trade.Model
{
    [Serializable]
    public class TradeManager
    {
        [field: NonSerialized] public IList<TradeRoute> OutGoingRoutes { get; private set; } = 
            ImmutableList.Create<TradeRoute>();
        private readonly IList<TradeRoute> outGoingRoutes = new List<TradeRoute>();
        [field: NonSerialized] public IList<TradeRoute> IncomingRoutes { get; private set; } = 
            ImmutableList.Create<TradeRoute>();
        private readonly IList<TradeRoute> incomingRoutes = new List<TradeRoute>();
        public TradeManager() { }
        public void AddOutGoingRoute(TradeRoute route)
        {
            outGoingRoutes.Add(route);
            OutGoingRoutes = outGoingRoutes.ToImmutableList();
        }
        public void AddIncomingRoute(TradeRoute route)
        {
            incomingRoutes.Add(route);
            IncomingRoutes = incomingRoutes.ToImmutableList();
        }
        public void FinishDeserialization()
        {
            OutGoingRoutes = outGoingRoutes.ToImmutableList();
            IncomingRoutes = incomingRoutes.ToImmutableList();
        }
        public void ProcessTrades()
        {
            foreach (var route in OutGoingRoutes)
                ProcessRoute(route);
        }
        private void ProcessRoute(TradeRoute route)
        {
            if (!route.Originator.CanIncrementGood(route.SentGood, -route.SentAmount)
                || !route.Receiver.CanIncrementGood(route.ReceivedGood, -route.ReceivedAmount))
                return;
            route.Originator.IncrementGood(route.SentGood, -route.SentAmount);
            route.Originator.IncrementGood(route.ReceivedGood, route.ReceivedAmount);
            route.Receiver.IncrementGood(route.SentGood, route.SentAmount);
            route.Receiver.IncrementGood(route.ReceivedGood, -route.ReceivedAmount);
        }
    }
}
