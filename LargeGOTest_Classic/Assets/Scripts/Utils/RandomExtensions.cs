namespace Utils
{
    public static class RandomExtensions
    {
        public static T GetRandom<T>(this T[] collection) where T : class
        {
            var rndIndex = UnityEngine.Random.Range(0, collection.Length);
            return collection[rndIndex];
        }

        public static T GetRandomExcept<T>(this T[] collection, T except) where T : class
        {
            var rndIndex = UnityEngine.Random.Range(0, collection.Length - 1);
            if (collection[rndIndex] == except)
            {
                return collection[rndIndex + 1];
            }

            return collection[rndIndex];
        }
    }
}