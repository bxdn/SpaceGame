namespace Assets.Scripts.Model
{
    public class GoodInfo
    {
        public bool Increasing { get; set; }
        public float Value { get;}
        public GoodInfo(float value)
        {
            Increasing = true;
            Value = value;
        }
    }
}
