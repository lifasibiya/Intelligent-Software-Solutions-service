using Dapper;
using iss.Data;

namespace iss.Models
{
    public class UserManagement : IUserService
    {
        User? IUserService.AuthUser(string idnumber)
        {
            var param = new DynamicParameters();
            param.Add("idnumber", idnumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            return ISSDBContext.ExcuteProcs<User>("[dbo].[sp_AuthUser]", param).FirstOrDefault();
        }

        bool IUserService.CreateUser(User user)
        {
            var param = new DynamicParameters();
            param.Add("name", user.Name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("surname", user.Surname, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("dateOfBirth", user.DateOfBirth, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("idnumber", user.IDNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            return ISSDBContext.ExcuteProcs<bool>("[dbo].[sp_CreateUser]", param).FirstOrDefault();
        }

        User? IUserService.GetUser(int id)
        {
            var param = new DynamicParameters();
            param.Add("id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            return ISSDBContext.ExcuteProcs<User>("[dbo].[sp_AuthUser]", param).FirstOrDefault();
        }
    }
}
