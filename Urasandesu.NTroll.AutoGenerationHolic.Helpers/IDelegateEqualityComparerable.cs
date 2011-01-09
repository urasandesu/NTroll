
namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers
{
    public interface IDelegateEqualityComparerable<T>
    {
        DelegateEqualityComparer<T> Comparer { get; set; }
    }
}
