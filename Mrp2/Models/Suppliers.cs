﻿namespace Mrp2.Models
{
    public class Suppliers
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        public string Notes { get; set; } = "";


        public Suppliers()
        {

        }
    }
}
