using System;
using System.Collections.Generic;
using System.Text;

namespace Migrations.Models
{
    public class User
    {
        public Guid id { get; set; }

        public string userName { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string passportId { get; set; }

    }
}
