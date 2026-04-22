//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; } = null!;

        public async ValueTask<User> InsertUserAsync(User user)
        {
            var entity = await this.Users.AddAsync(user);
            await this.SaveChangesAsync();

            return entity.Entity;
        }

        public async ValueTask<User> SelectUserByIdAsync(Guid userId) =>
            await this.Users.
                FirstOrDefaultAsync(user => user.Id == userId);
    }
}