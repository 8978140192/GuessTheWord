using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWordApplication.Models
{
    public class TableUpdation:GuessTheWord
    {
        public List<string> FetechAvailableUsers()
        {
            List<string> availableUser=new();
            foreach (var item in context.UserDetails)
            {
                availableUser.Add(item.UserName);
            }
            return availableUser;
        }
    }
}
