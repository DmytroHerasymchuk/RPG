using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NPC : LivingEntity
    {
        public int Id { get; set; }

        public string ImageName { get; set; }
        //public Dialog Dialog { get; set; }

        public NPC(int id, string name, string imageName) :
             base(name, 9999, 9999, new List<PlayerAttribute>(), 9999)
        {
            Id = id;
            ImageName = imageName;
        }



    }
}
