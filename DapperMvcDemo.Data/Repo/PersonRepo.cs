
using DapperMvcDemo.Data.Data;
using DapperMvcDemo.Data.Models.Domain;

namespace DapperMvcDemo.Data.Repo
{
    public class PersonRepo: IPersonRepo
    {
        private readonly IApplicationDbContext _db;

        public PersonRepo(IApplicationDbContext db)
        {
            _db=db;
        }

        public async Task<bool> AddAsync(Person person)
        {
            try
            {
                await _db.SaveData("sp_create_person", new { person.Name, person.Email, person.Address });
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {
                await _db.SaveData("sp_update_person", person);
                return true;
            }

            catch (Exception ex)
            {
                return false;

            }
        }


    }
}
