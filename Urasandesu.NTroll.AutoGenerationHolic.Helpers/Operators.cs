
namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers
{
    public static class Operators
    {
        public static int Eor(int x, int y)
        {
            return x ^ y;
        }

        public static int NullableGetHashCode(object obj)
        {
            return obj == null ? 0 : obj.GetHashCode();
        }
    }
}
