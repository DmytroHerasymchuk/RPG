using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ItemPercentage
    {
        public int Id { get; }
        public int Percentage { get; }

        public ItemPercentage(int id, int percentage)
        {
            Id = id;
            Percentage = percentage;
        }
    }
}
