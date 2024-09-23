
using System;
using System.Collections.Generic;
using System.IO;

class Program
    {
        private const string DataFilePath = "data.txt";

        private static List<Product> products;
        private static List<Retailer> retailers;
        private static List<Supplier> suppliers;

        static void Main(string[] args)
        {
            products = new List<Product>();
            retailers = new List<Retailer>();
            suppliers = new List<Supplier>();

            LoadData();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Display Product Details");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Search and Filter Products");
                Console.WriteLine("5. Calculate Characteristics");
                Console.WriteLine("6. Sort Products");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        DisplayProductDetails();
                        break;
                    case "3":
                        DeleteProduct();
                        break;
                    case "4":
                        SearchAndFilterProducts();
                        break;
                    case "5":
                        CalculateCharacteristics();
                        break;
                    case "6":
                        SortProducts();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }

            SaveData();
        }

        private static void LoadData()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(DataFilePath);
        
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(';');
        
                        if (parts.Length >= 5)
                        {
                            int ID = int.Parse(parts[0]);
                            string Name = parts[1];
                            decimal Price = decimal.Parse(parts[2]);
                            int Quantity = int.Parse(parts[3]);
                            int RetailerId = int.Parse(parts[4]);
        
                            int SupplierId = -1;
                            if (parts.Length >= 7)
                            {
                                SupplierId = int.Parse(parts[6]);
        
                                Supplier supplier = suppliers.Find(s => s.ID == SupplierId);
                                if (supplier == null)
                                {
                                    string SupplierName = parts[7];
                                    supplier = new Supplier(SupplierId, SupplierName);
                                    suppliers.Add(supplier);
                                }
                            }
        
                            products.Add(new Product(ID, Name, Price, Quantity, RetailerId, SupplierId));
        
                            Retailer retailer = retailers.Find(r => r.ID == RetailerId);
                            if (retailer == null)
                            {
                                string RetailerName = parts[5];
                                retailer = new Retailer(RetailerId, RetailerName);
                                retailers.Add(retailer);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred while loading data: " + ex.Message);
                }
            }
        }


        private static void SaveData()
{
    try
    {
        using (StreamWriter writer = new StreamWriter(DataFilePath))
        {
            foreach (Product product in products)
            {
                Retailer retailer = retailers.Find(r => r.ID == product.RetailerID);
                string retailerName = retailer?.Name ?? "";

                Supplier supplier = suppliers.Find(s => s.ID == product.SupplierID);
                string supplierName = supplier?.Name ?? "";

                writer.WriteLine($"{product.ID};{product.Name};{product.Price};{product.Quantity};{product.RetailerID};{retailerName};{product.SupplierID};{supplierName}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error occurred while saving data: " + ex.Message);
    }
}


      private static void AddProduct()
{
    Console.Write("Enter product ID: ");
    int ID;
    if (!int.TryParse(Console.ReadLine(), out ID))
    {
        Console.WriteLine("Invalid product ID. Please enter a valid integer.");
        return;
    }

    // Check if a product with the same ID already exists
    if (products.Exists(p => p.ID == ID))
    {
        Console.WriteLine("A product with the same ID already exists.");
        return;
    }

    Console.Write("Enter product name: ");
    string Name = Console.ReadLine().ToLower();

    // Check if a product with the same name already exists
    if (products.Exists(p => p.Name.ToLower() == Name))
    {
        Console.WriteLine("A product with the same name already exists.");
        return;
    }

    Console.Write("Enter product price: ");
    decimal Price;
    if (!decimal.TryParse(Console.ReadLine(), out Price))
    {
        Console.WriteLine("Invalid product price. Please enter a valid decimal number.");
        return;
    }

    Console.Write("Enter product quantity: ");
    int Quantity;
    if (!int.TryParse(Console.ReadLine(), out Quantity))
    {
        Console.WriteLine("Invalid product quantity. Please enter a valid integer.");
        return;
    }

    Console.Write("Enter retailer ID: ");
    int RetailerId;
    if (!int.TryParse(Console.ReadLine(), out RetailerId))
    {
        Console.WriteLine("Invalid retailer ID. Please enter a valid integer.");
        return;
    }

    // Check if a product with the same retailer ID already exists
    if (products.Exists(p => p.RetailerID == RetailerId))
    {
        Console.WriteLine("A product with the same retailer ID already exists.");
        return;
    }

    Console.Write("Enter retailer name: ");
    string RetailerName = Console.ReadLine().ToLower();

    Console.Write("Enter supplier ID: ");
    int SupplierId;
    if (!int.TryParse(Console.ReadLine(), out SupplierId))
    {
        Console.WriteLine("Invalid supplier ID. Please enter a valid integer.");
        return;
    }

    // Check if a product with the same supplier ID already exists
    if (products.Exists(p => p.SupplierID == SupplierId))
    {
        Console.WriteLine("A product with the same supplier ID already exists.");
        return;
    }

    Console.Write("Enter supplier name: ");
    string SupplierName = Console.ReadLine().ToLower();

    if (suppliers.Exists(s => s.Name.ToLower() == SupplierName))
    {
        Console.WriteLine("A supplier with the same name already exists.");
        return;
    }

    Supplier supplier = new Supplier(SupplierId, SupplierName);
    suppliers.Add(supplier);

    Retailer retailer = new Retailer(RetailerId, RetailerName);
    retailers.Add(retailer);

    products.Add(new Product(ID, Name, Price, Quantity, RetailerId, SupplierId));

    Console.WriteLine("Product added successfully.");
}
      
      
        private static void DisplayProductDetails()
        {
            Console.Write("Enter product ID: ");
            int id = int.Parse(Console.ReadLine());

            Product product = products.Find(p => p.ID == id);
            if (product != null)
            {
                Console.WriteLine($"ID: {product.ID}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Quantity: {product.Quantity}");

                Retailer retailer = retailers.Find(r => r.ID == product.RetailerID);
                if (retailer != null)
                    Console.WriteLine($"Retailer: {retailer.Name}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        private static void DeleteProduct()
        {
            Console.Write("Enter product ID: ");
            int id = int.Parse(Console.ReadLine());

            Product product = products.Find(p => p.ID == id);
            if (product != null)
            {
                products.Remove(product);
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

       private static void SearchAndFilterProducts()
        {
            Console.WriteLine("1. Search by product name");
            Console.WriteLine("2. Filter by retailer");
            Console.WriteLine("3. Filter by supplier");
        
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();
        
            switch (choice)
            {
                case "1":
                    Console.Write("Enter product name: ");
                    string productName = Console.ReadLine().ToLower();
        
                    List<Product> searchResults = products.FindAll(p => p.Name.ToLower().Contains(productName));
                    DisplayProducts(searchResults);
                    break;
                case "2":
                    Console.Write("Enter retailer name: ");
                    string retailerName = Console.ReadLine().ToLower();
        
                    List<Product> retailerProducts = new List<Product>();
                    Retailer retailer = retailers.Find(r => r.Name.ToLower() == retailerName);
                    if (retailer != null)
                    {
                        retailerProducts = products.FindAll(p => p.RetailerID == retailer.ID);
                    }
        
                    if (retailerProducts.Count > 0)
                    {
                        Console.WriteLine("Products for the given retailer:");
                        DisplayProducts(retailerProducts);
                    }
                    else
                    {
                        Console.WriteLine("No products found for the given retailer.");
                    }
                    break;
                case "3":
                    Console.Write("Enter supplier name: ");
                    string supplierName = Console.ReadLine().ToLower();
        
                    List<Product> supplierProducts = new List<Product>();
                    Supplier supplier = suppliers.Find(s => s.Name.ToLower() == supplierName);
                    if (supplier != null)
                    {
                        supplierProducts = products.FindAll(p => p.SupplierID == supplier.ID);
                    }
        
                    if (supplierProducts.Count > 0)
                    {
                        Console.WriteLine("Products for the given supplier:");
                        DisplayProducts(supplierProducts);
                    }
                    else
                    {
                        Console.WriteLine("No products found for the given supplier.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static void DisplayProducts(List<Product> productList)
        {
            if (productList.Count > 0)
            {
                Console.WriteLine("ID\tName\t\tPrice\tQuantity\tRetailer\tSupplier");
                Console.WriteLine("--\t----\t\t-----\t--------\t--------\t--------");
        
                foreach (Product product in productList)
                {
                    Retailer retailer = retailers.Find(r => r.ID == product.RetailerID);
                    Supplier supplier = suppliers.Find(s => s.ID == product.SupplierID);
        
                    Console.WriteLine($"{product.ID}\t{product.Name}\t\t{product.Price:C}\t{product.Quantity}\t\t{retailer?.Name}\t\t{supplier?.Name}");
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
          }
      
      private static void CalculateCharacteristics()
      {
          Console.WriteLine("1. Number of records with a certain price");
          Console.WriteLine("2. Number of records with a certain quantity");
      
          Console.Write("Enter your choice: ");
          string choice = Console.ReadLine();
          Console.WriteLine();
      
          switch (choice)
          {
              case "1":
                  Console.Write("Enter product price: ");
                  decimal price = decimal.Parse(Console.ReadLine());
      
                  int countByPrice = 0;
                  foreach (Product product in products)
                  {
                      if (product.Price == price)
                          countByPrice++;
                  }
                  Console.WriteLine($"Number of records with price {price:C}: {countByPrice}");
                  break;
              case "2":
                  Console.Write("Enter product quantity: ");
                  int quantity = int.Parse(Console.ReadLine());
      
                  int countByQuantity = 0;
                  foreach (Product product in products)
                  {
                      if (product.Quantity == quantity)
                          countByQuantity++;
                  }
                  Console.WriteLine($"Number of records with quantity {quantity}: {countByQuantity}");
                  break;
              default:
                  Console.WriteLine("Invalid choice. Please try again.");
                  break;
          }
      }
      

        private static void SortProducts()
        {
            Console.WriteLine("1. Sort by product name");
            Console.WriteLine("2. Sort by product price");
            Console.WriteLine("3. Sort by product quantity");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            List<Product> sortedList = new List<Product>(products);

            switch (choice)
            {
                case "1":
                    sortedList.Sort((p1, p2) => string.Compare(p1.Name, p2.Name, StringComparison.Ordinal));
                    DisplayProducts(sortedList);
                    break;
                case "2":
                    sortedList.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
                    DisplayProducts(sortedList);
                    break;
                case "3":
                    sortedList.Sort((p1, p2) => p1.Quantity.CompareTo(p2.Quantity));
                    DisplayProducts(sortedList);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }