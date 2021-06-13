using System.Collections.Generic;
using System.Security.AccessControl;

namespace MobileApi.Controllers.Object.Dto
{
    public class CreateGeoObjectDto
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<int> Points { get; set; }
    }
}