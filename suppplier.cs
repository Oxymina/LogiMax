using System;

public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Supplier(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
