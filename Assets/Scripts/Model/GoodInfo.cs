namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class GoodInfo
    {
        public int Increasing { get; set; }
        public float Value { get;}
        public GoodInfo(float value)
        {
            Increasing = 0;
            Value = value;
        }
    }
}
