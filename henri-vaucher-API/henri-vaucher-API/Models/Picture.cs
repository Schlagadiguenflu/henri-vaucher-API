﻿using System.ComponentModel.DataAnnotations;

namespace henri_vaucher_API.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        public int Date { get; set; }
        public int Number { get; set; }
        [StringLength(200)]
        public string Signature { get; set; }
        [StringLength(200)]
        public string PositionSignature { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Surface { get; set; }
        [StringLength(200)]
        public string Support { get; set; }
        [StringLength(200)]
        public string Drawing { get; set; }
        [StringLength(200)]
        public string DominantTones { get; set; }
        [StringLength(200)]
        public string Owner { get; set; }
        [StringLength(200)]
        public string From { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }
        // Base 64
        public string File { get; set; }
    }
}
