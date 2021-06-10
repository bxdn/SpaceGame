using Assets.Scripts.Model;
using System;
using static Assets.Scripts.Registry.GoodsServicesRegistry;

namespace Assets.Scripts.Trade.Model
{
    [Serializable]
    public class TradeRoute
    {
        public GoodOrService SentGood { get; }
        public float SentAmount { get; }
        public GoodOrService ReceivedGood { get; }
        public float ReceivedAmount { get; }
        public Colony Originator { get; }
        public Colony Receiver { get; }
        public float Cost { get; }
        public TradeRoute(GoodOrService sentGood, float sentAmount, GoodOrService receivedGood, float receivedAmount, Colony originator, Colony receiver, float cost)
        {
            SentGood = sentGood;
            SentAmount = sentAmount;
            ReceivedGood = receivedGood;
            ReceivedAmount = receivedAmount;
            Originator = originator;
            Receiver = receiver;
            Cost = cost;
        }
    }
}
