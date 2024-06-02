using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

namespace MVC_11424.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public int DurationInSeconds { get; set; }
        public Album? Album { get; set; }
    }

    public class Album
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
    }
}
