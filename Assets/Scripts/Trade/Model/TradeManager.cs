using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Assets.Scripts.Trade.Model
{
    [Serializable]
    public class TradeManager
    {
        public IEnumerable<TradeRoute> OutGoingRoutes { get; private set; } =
            new HashSet<TradeRoute>();
        public IEnumerable<TradeRoute> IncomingRoutes { get; private set; } =
            new HashSet<TradeRoute>();
        public TradeManager() { }
        public void AddOutGoingRoute(TradeRoute route)
        {
            (OutGoingRoutes as HashSet<TradeRoute>).Add(route);
        }
        public void AddIncomingRoute(TradeRoute route)
        {
            (IncomingRoutes as HashSet<TradeRoute>).Add(route);
        }
        public void ProcessTrades()
        {
            foreach (var route in OutGoingRoutes)
                ProcessRoute(route);
        }
        public void RemoveRoute(TradeRoute route)
        {
            (OutGoingRoutes as HashSet<TradeRoute>).Remove(route);
            (IncomingRoutes as HashSet<TradeRoute>).Remove(route);
        }
        private static void ProcessRoute(TradeRoute route)
        {
            if (!CanProcessRoute(route))
                return;
            route.Originator.IncrementGood(route.SentGood, -route.SentAmount);
            route.Originator.IncrementGood(route.ReceivedGood, route.ReceivedAmount);
            route.Receiver.IncrementGood(route.SentGood, route.SentAmount);
            route.Receiver.IncrementGood(route.ReceivedGood, -route.ReceivedAmount);
            route.Originator.IncrementGood(Scripts.Model.EGood.Hydrogen, -route.Cost);
        }
        private static bool CanProcessRoute(TradeRoute route)
        {
            return route.Originator.CanIncrementGood(route.SentGood, -route.SentAmount)
                && route.Receiver.CanIncrementGood(route.ReceivedGood, -route.ReceivedAmount)
                && route.Originator.CanIncrementGood(Scripts.Model.EGood.Hydrogen, -route.Cost);
        }
    }
}
