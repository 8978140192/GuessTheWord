using System;
using System.Collections.Generic;

#nullable disable

namespace GuessTheWordApplication.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            UserAssignedWordFromUserNavigations = new HashSet<UserAssignedWord>();
            UserAssignedWordToUserNavigations = new HashSet<UserAssignedWord>();
        }

        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserContact { get; set; }

        public virtual ScoreBoard ScoreBoard { get; set; }
        public virtual ICollection<UserAssignedWord> UserAssignedWordFromUserNavigations { get; set; }
        public virtual ICollection<UserAssignedWord> UserAssignedWordToUserNavigations { get; set; }
    }
}
