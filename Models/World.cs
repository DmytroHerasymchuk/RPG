using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Models
{
    public class World
    {
        private readonly List<Location> _locations = new List<Location>();

        public ObservableCollection<MapPiece> MapPieces { get; } = new ObservableCollection<MapPiece>();

        public void AddLocation(Location location)
        {
            _locations.Add(location);
            MapPieces.Add(location.Map);
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach(Location loc in _locations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
                
            }
            return null;
        }
    }
}
