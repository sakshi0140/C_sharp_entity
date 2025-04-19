using EntityFramewrokCore.Data;
using EntityFramewrokCore.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new AppDbContext())
{
    context.Database.EnsureCreated();
    Console.WriteLine("Database initialized!");
}
static void AddProduct()
{
    using var context = new AppDbContext(); 
    Console.Write("Enter product name: ");
    var name = Console.ReadLine()!;
    Console.Write("Enter price: ");
    var price = decimal.Parse(Console.ReadLine()!);
    Console.Write("Enter stock quantity: ");
    var stock = int.Parse(Console.ReadLine()!);
   
    context.Products.Add(new Product
    {
        Name = name,
        Price = price,
        Stock = stock
    });
    context.SaveChanges(); 
    Console.WriteLine("Product added!");
}

static void ListProducts()
{
    using var context = new AppDbContext();
    var products = context.Products.ToList(); 
    Console.WriteLine("\n{0,-5} {1,-20} {2,-10} {3}", "ID", "Name", "Price",
    "Stock");
    foreach (var p in products)
    {
        Console.WriteLine($"{p.Id,-5} {p.Name,-20} {p.Price,-10:C} {p.Stock}");
    }
}
static void UpdateProduct()
{
    using var context = new AppDbContext();
    Console.Write("Enter product ID to update: ");
    var id = int.Parse(Console.ReadLine()!);
    
    var product = context.Products.Find(id);
    if (product == null)
    {
        Console.WriteLine("Product not found!");
        return;
    }
    
    Console.Write($"New name [{product.Name}]: ");
    var name = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(name)) product.Name = name;
    Console.Write($"New price [{product.Price}]: ");
    var priceInput = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(priceInput))
        product.Price = decimal.Parse(priceInput);
    context.SaveChanges(); 
    Console.WriteLine("Updated successfully!");
}
static void DeleteProduct()
{
    using var context = new AppDbContext();
    Console.Write("Enter product ID to delete: ");
    var id = int.Parse(Console.ReadLine()!);
    var product = context.Products.Find(id);
    if (product == null)
    {
        Console.WriteLine("Product not found!");
        return;
    }
    context.Products.Remove(product);
    context.SaveChanges();
    Console.WriteLine("Product deleted!");
}
while (true)
{
    Console.WriteLine("\n1. Add Product");
    Console.WriteLine("2. List Products");
    Console.WriteLine("3. Update Product");
    Console.WriteLine("4. Delete Product");
    Console.WriteLine("5. Exit");
    Console.Write("Choose an option: ");
    switch (Console.ReadLine())
    {
        case "1": AddProduct(); break;
        case "2": ListProducts(); break;
        case "3": UpdateProduct(); break;
        case "4": DeleteProduct(); break;
        case "5": return;
        default: Console.WriteLine("Invalid option!"); break;
    }
}
