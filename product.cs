using System;

public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int RetailerID { get; set; }
        public int SupplierID { get; set; }

        public Product(int id, string name, decimal price, int quantity, int retailerID, int supplierID)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            RetailerID = retailerID;
            SupplierID = supplierID;
        }
    }
