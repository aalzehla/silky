namespace Silky.Core
{
    public class Singleton<T> : BaseSingleton
    {
        private static T instance;

        /// <summary>
        /// Specify typeTsingleton
        /// </summary>
        public static T Instance
        {
            get => instance;
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}