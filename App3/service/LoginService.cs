using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.service
{
    class LoginService
    {
        ClientApi<Driver> cL = new ClientApi<Driver>();

        //no more used
        public async Task<bool> CheckLoginIfExists(string username, string password)
        {
            var check = await cL.checkLogin(username, password);

            return check;
        }
        public async Task<Driver> CheckLoginIfExist(string username, string password)
        {
            var check = await cL.checkLogin1(username, password);

            return check;
        }
    }
}
