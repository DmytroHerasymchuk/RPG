using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Trader : LivingEntity
    {
        public int Id { get; }
        public Trader(int id, string name) : base(name,9999,9999, 20, 9999)
        {
            Id = id;
        }   
    }
}
