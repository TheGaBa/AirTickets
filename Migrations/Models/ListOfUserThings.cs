using System;
using System.Collections.Generic;
using System.Text;

namespace Migrations.Models
{
    public class ListOfUserThings
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ArrayOfThings { get; set; }
    }
}
