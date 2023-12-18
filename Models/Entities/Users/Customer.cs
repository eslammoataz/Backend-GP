﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Entities.Users
{
    public class Customer : User
    {

        public string Address { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        //list<worker> favourite




    }
}
