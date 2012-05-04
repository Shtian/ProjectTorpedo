using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTorpedo.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,100)]
        public int MetaRating { get; set; }
        public DateTime Release { get; set; }
        public string Description { get; set; }

        [ForeignKey("Platform"),Required]
        public int PlatformId { get; set; }
        public virtual Platform Platform { get; set; }

        public GamePicture GamePicture { get; set; }


    }
}