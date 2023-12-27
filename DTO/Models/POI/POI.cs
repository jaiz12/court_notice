using System;

namespace DTO.Models.POI
{
    public class POI_List
    {
        public Int32 location_id { get; set; }
        public string location_name { get; set; }
        public string location_general_name { get; set; }
        //public string distance { get; set; }
        public decimal delta { get; set; }
    }
    public class POI_Selected_List
    {
        public Int32 location_id { get; set; }
        public string location_name { get; set; }
        public string location_general_name { get; set; }
        //public string distance { get; set; }
        public decimal distance_delta { get; set; }
        public decimal time_delta { get; set; }
    }
}
