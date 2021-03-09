using Assets.Scripts.Model;
using System;

namespace Assets.Scripts.Trade.Model
{
    [Serializable]
    public class TradeRoute
    {
        public EGood SentGood { get; }
        public float SentAmount { get; }
        public EGood ReceivedGood { get; }
        public float ReceivedAmount { get; }
        public Colony Originator { get; }
        public Colony Receiver { get; }
        public TradeRoute(EGood sentGood, float sentAmount, EGood receivedGood, float receivedAmount, Colony originator, Colony receiver)
        {
            SentGood = sentGood;
            SentAmount = sentAmount;
            ReceivedGood = receivedGood;
            ReceivedAmount = receivedAmount;
            Originator = originator;
            Receiver = receiver;
        }
    }
}
