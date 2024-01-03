using System;

namespace DTO.Models.Customization
{
    public class DashboardSkin_DTO
    {
        public long skin_id { get; set; }
        public string section_name { get; set; }
        public string company_name { get; set; }
        public string background_color { get; set; }
        public string font_color { get; set; }
        public long font_size { get; set; }
        public long image_height { get; set; }
        public long image_width { get; set; }
        public string text_alignment { get; set; }

        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }


        //Author Pranai Giri 
        public bool font_scheme { get; set; }

    }
}
