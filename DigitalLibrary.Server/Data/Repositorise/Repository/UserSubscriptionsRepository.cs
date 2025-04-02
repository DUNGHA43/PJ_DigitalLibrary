using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class UserSubscriptionsRepository : Repository<UserSubcriptions>, IUserSubscriptionsRepository
    {
        private readonly DbDigitalLibraryContext _context;

        public UserSubscriptionsRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserSubscriptionsDTO> FindUserSubscriptionsByUserId(int userId)
        {
            var query = _dbSet.Where(us => us.userid == userId).AsQueryable();

            var userSubscriptions = await query
                .Select(us => new UserSubscriptionsDTO
                {
                    id = us.id,
                    userid = us.userid,
                    planid = us.planid,
                    redate = us.redate,
                    exdate = us.exdate,
                    status = us.status,
                    SubscriptionPlans = _context.subscriptionPlans
                    .Where(sub => sub.id == us.planid).Select(sub => new SubscriptionPlansDTO
                    {
                        id = sub.id,
                        nameplan = sub.nameplan,
                        price = sub.price,
                        description = sub.description,
                        durationsindays = sub.durationindays
                    }).FirstOrDefault(),

                    Users = _context.users
                    .Where(u => u.id == us.userid).Select(u => new UsersDTO
                    {
                        id = u.id,
                        username = u.username,
                        email = u.email,
                        fullname = u.fullname,
                        address = u.address,
                        birthday = u.birthday,
                        identification = u.identification,
                        imageurl = u.imageurl,
                        phonenumber = u.phonenumber,
                        gender = u.gender ?? true,
                        password = u.password,
                        status = u.status,
                        createdate = u.createdate,
                    }).FirstOrDefault()

                }).FirstOrDefaultAsync();

            return userSubscriptions!;
        }
    }
}
