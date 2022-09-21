using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Models
{
    public class NPC : LivingEntity
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        public ObservableCollection<Dialog> Dialogs { get; set; } = new ObservableCollection<Dialog>();

        public NPC(int id, string name, string imageName) :
             base(name, 9999, 9999, new List<PlayerAttribute>(), 9999)
        {
            Id = id;
            ImageName = imageName;
        }



    }
}
