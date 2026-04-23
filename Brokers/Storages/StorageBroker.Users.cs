//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
                AsNoTracking().
                FirstOrDefaultAsync(user => user.Id == userId);

        public IQueryable<User> SelectAllUsers() =>
            SelectAll<User>();

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            EntityEntry<User> userEntityEntry =
                this.Users.Update(user);

            await this.SaveChangesAsync();

            return userEntityEntry.Entity;
        }

        public async ValueTask<User> DeleteUserAsync(User user)
        {
            EntityEntry<User> userEntityEntry =
                this.Users.Remove(user);

            await this.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> SelectUserByTokenAsync(string token) =>
            await this.Users.FirstOrDefaultAsync(
                user => user.EmailConfirmationToken == token);
    }
}