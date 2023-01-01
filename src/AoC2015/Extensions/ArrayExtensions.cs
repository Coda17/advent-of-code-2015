namespace AoC2015.Extensions;

public static class ArrayExtensions
{
    // Heap's algorithm
    // https://en.wikipedia.org/wiki/Heap%27s_algorithm
    public static IEnumerable<T[]> Permutations<T>(this T[] arr)
    {
        var a = (T[])arr.Clone();
        var n = a.Length;
        var permutations = new List<T[]>
        {
            (T[])a.Clone()
        };

        var c = Enumerable.Range(0, n).Select(_ => 0).ToArray();
        var i = 0;

        while (i < n)
        {
            if (c[i] < i)
            {
                // Is even
                if (i % 2 == 0)
                {
                    (a[0], a[i]) = (a[i], a[0]);
                }
                else
                {
                    (a[c[i]], a[i]) = (a[i], a[c[i]]);
                }

                permutations.Add((T[])a.Clone());

                ++c[i];
                i = 0;
            }
            else
            {
                c[i] = 0;
                ++i;
            }
        }

        return permutations;
    }
}