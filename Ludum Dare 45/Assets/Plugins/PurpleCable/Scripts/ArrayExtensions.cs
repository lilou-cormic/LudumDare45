using UnityEngine;

public static class ArrayExtensions
{
    public static T GetRandom<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
            return default;

        if (array.Length == 1)
            return array[0];

        return array[Random.Range(0, array.Length)];
    }
}
