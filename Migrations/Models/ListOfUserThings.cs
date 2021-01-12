using System;
using System.Collections.Generic;
using System.Text;

namespace Migrations.Models
{
    public class ListOfUserThings
    {
        public Guid id { get; set; }

        public Guid userId { get; set; }

        public string[] arrayOfThings { get; set; }
    }
}
