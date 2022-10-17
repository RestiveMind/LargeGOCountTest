namespace Utils
{
    public static class RandomExtensions
    {
        public static T GetRandom<T>(this T[] collection)
        {
            var rndIndex = UnityEngine.Random.Range(0, collection.Length);
            return collection[rndIndex];
        }

        public static T GetRandomExcept<T>(this T[] collection, T except)
        {
            var rndIndex = UnityEngine.Random.Range(0, collection.Length - 1);
            return collection[rndIndex].Equals(except) ? collection[rndIndex + 1] : collection[rndIndex];
        }
    }
}