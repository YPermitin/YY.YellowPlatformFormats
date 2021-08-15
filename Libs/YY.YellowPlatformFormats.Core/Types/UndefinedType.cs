namespace YY.YellowPlatformFormats.Core.Types
{
    public class UndefinedType : BaseType
    {
        public static readonly UndefinedType Default = new UndefinedType();

        private UndefinedType()
        {
        }

        public override string ToString()
        {
            return "Undefined";
        }
    }
}
