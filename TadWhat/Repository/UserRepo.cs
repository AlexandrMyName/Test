using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TadWhat.Domain;


namespace TadWhat.Repository
{
    public class UserRepo
    {
        DatabaseContext db;

        public UserRepo(DatabaseContext db){
            this.db = db;
        }

        public  IQueryable<User> GetAllUsers(){
            return db.user.OrderBy(x => x.Id);
        }


        public async Task<User>  GetUserByEmail(string email){
            return await db.user.SingleAsync(o => o.Email == email);
        }
        public async Task<User> GetUserById(Guid id){
            return await db.user.SingleAsync(o => o.Id == id);
        }


        public void RemoveUserByEmail(User user){
             db.user.Remove(user);
             db.SaveChanges();
        }
        public Guid AddedOrModifiedUser(User user){
            if (user.Id == default)
            db.user.Entry(user).State = EntityState.Added;
            else
            db.user.Entry(user).State = EntityState.Modified;

            db.SaveChanges();
            return user.Id;
        }
    }
}
