/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
using System.ComponentModel.DataAnnotations.Schema;

namespace _43LimeMobileApp.Models.Entities
{

    public interface ILatLng
    {
        string Lattitude { get; set; }
        string Longitude { get; set; }
    }

    [ComplexType]
    public class LatLng : ILatLng
    {
        public string Lattitude { get; set; }
        public string Longitude { get; set; }

        private LatLng()
        {

        }
        public LatLng(string lattitude, string longitude) : base()
        {
            this.Lattitude = lattitude;
            this.Longitude = longitude;
        }
    }





}
