using PraktikaDomain.Interfaces;
using StackExchange.Redis;
namespace PraktikaInfrastructure.Email
{
    public class MailRepository : IMailRepository
    {
        private readonly ConnectionMultiplexer redis;

        private readonly IDatabase db;

        private readonly string HashKey = "CheckMailClient";

        public MailRepository()
        {
            redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            db = redis.GetDatabase();
        }
        public async Task<int> AddMailCode(string Email)
        {
            if (await SearchMailCode(Email) != 0)
                await DeleteMailCode(Email);

            int Code = new Random().Next(100000, 999999);

            string key = $"{HashKey}:{Email}";

            var heshEntries = new HashEntry[]
            {
                new HashEntry(Email, Code)
            };
            await db.HashSetAsync(HashKey, heshEntries);

            await db.StringSetAsync(key, Code);
            await db.KeyExpireAsync(key, TimeSpan.FromHours(2));

            return Code;
        }

        public async Task<bool> DeleteMailCode(string Email)
        {
            if (!await db.HashDeleteAsync(HashKey, Email)) throw new Exception("Redis hash delete error");
            if (!await db.KeyDeleteAsync($"{HashKey}:{Email}")) throw new Exception("Redis key delete error");

            return true;
        }

        public async Task<int> SearchMailCode(string Email)
        {
            return (int)await db.HashGetAsync(HashKey, Email);
        }
    }
}
