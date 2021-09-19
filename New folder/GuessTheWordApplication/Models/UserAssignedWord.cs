using System;
using System.Collections.Generic;

#nullable disable

namespace GuessTheWordApplication.Models
{
    public partial class UserAssignedWord
    {
        public string Word { get; set; }
        public string ToUser { get; set; }
        public string FromUser { get; set; }

        public virtual UserDetail FromUserNavigation { get; set; }
        public virtual UserDetail ToUserNavigation { get; set; }
    }
}
