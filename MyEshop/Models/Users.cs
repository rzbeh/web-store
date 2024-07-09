﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }


        [Required]
        public DateTime RegisterDate { get; set; }

        public bool IsAdmin { get; set; }

        public List<Order> orders { get; set; }

    }
}
