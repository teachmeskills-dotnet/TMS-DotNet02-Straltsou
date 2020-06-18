using LearnApp.Core.Models;

namespace LearnApp.Core.Models
{
    /// <summary>
    /// Root JSON object of image.
    /// </summary>
    public class ImageModel
    {
        public string id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string promoted_at { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string color { get; set; }
        public string description { get; set; }
        public string alt_description { get; set; }
        public Urls urls { get; set; }
        public ImageLinks links { get; set; }
        public object[] categories { get; set; }
        public int likes { get; set; }
        public bool liked_by_user { get; set; }
        public object[] current_user_collections { get; set; }
        public object sponsorship { get; set; }
        public User user { get; set; }
        public Exif exif { get; set; }
        public Location location { get; set; }
        public int views { get; set; }
        public int downloads { get; set; }
    }

    /// <summary>
    /// Link for searching image.
    /// </summary>
    public class Urls
    {
        public string raw { get; set; }
        public string full { get; set; }
        public string regular { get; set; }
        public string small { get; set; }
        public string thumb { get; set; }
    }

    /// <summary>
    /// Other links. 
    /// </summary>
    public class ImageLinks
    {
        public string self { get; set; }
        public string html { get; set; }
        public string download { get; set; }
        public string download_location { get; set; }
    }
}
