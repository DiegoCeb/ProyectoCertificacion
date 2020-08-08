namespace Cache
{
    public interface ICache
    {
        void MemoryCache();

        void RedisCache();
    }
}
