using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTorpedo.Models
{
    public class GamePicture
    {
        [Key]
        public int PictureId { get; set; }
        public byte[] Picture { get; set; }
    }
}