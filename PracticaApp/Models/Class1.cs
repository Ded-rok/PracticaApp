using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaWpfApp.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public string Student { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
