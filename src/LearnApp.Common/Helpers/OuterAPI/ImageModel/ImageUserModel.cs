namespace LearnApp.Common.Helpers.OuterAPI.ImageModel
{
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
        public UserLinks links { get; set; }
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
    public class UserLinks
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
