namespace BarberShop.Infrastructure;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var record in collection)
        {
            action(record);
        }
    }
}