using System;

public class Retailer
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Retailer(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }