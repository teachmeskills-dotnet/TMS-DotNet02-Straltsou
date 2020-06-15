namespace LearnApp.WebAPI.Models
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
        public Links links { get; set; }
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
    public class Links
    {
        public string self { get; set; }
        public string html { get; set; }
        public string download { get; set; }
        public string download_location { get; set; }
    }

    /// <summary>
    /// User information.
    /// </summary>
    public class User
    {
        public string id { get; set; }
        public string updated_at { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public object twitter_username { get; set; }
        public string portfolio_url { get; set; }
        public string bio { get; set; }
        public string location { get; set; }
        public Links1 links { get; set; }
        public Profile_Image profile_image { get; set; }
        public string instagram_username { get; set; }
        public int total_collections { get; set; }
        public int total_likes { get; set; }
        public int total_photos { get; set; }
        public bool accepted_tos { get; set; }
    }

    /// <summary>
    /// Links to user info.
    /// </summary>
    public class Links1
    {
        public string self { get; set; }
        public string html { get; set; }
        public string photos { get; set; }
        public string likes { get; set; }
        public string portfolio { get; set; }
        public string following { get; set; }
        public string followers { get; set; }
    }

    /// <summary>
    /// Profile image.
    /// </summary>
    public class Profile_Image
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }

    /// <summary>
    /// Image details.
    /// </summary>
    public class Exif
    {
        public string make { get; set; }
        public string model { get; set; }
        public string exposure_time { get; set; }
        public string aperture { get; set; }
        public string focal_length { get; set; }
        public string iso { get; set; }
    }

    /// <summary>
    /// Location of making picture.
    /// </summary>
    public class Location
    {
        public string title { get; set; }
        public string name { get; set; }
        public object city { get; set; }
        public string country { get; set; }
        public Position position { get; set; }
    }

    /// <summary>
    /// Position.
    /// </summary>
    public class Position
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
