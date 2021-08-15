namespace YY.YellowPlatformFormats.Core.Types
{
    public class NullValue : BaseType
    {
        public static readonly NullValue Default = new NullValue();

        private NullValue()
        {
        }

        public override string ToString()
        {
            return "Null";
        }
    }
}
