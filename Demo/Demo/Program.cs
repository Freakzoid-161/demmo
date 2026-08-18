using Demo;
Console.WriteLine($"Connection provided by {ProjectConstants.DatabaseProvider}");



Menu();

static void QueringStock()
{
    using (Varastohallinta varastohallinta = new())
    {
        Console.WriteLine("Stock");

        IQueryable<Tuotteet>? tuotteet = varastohallinta.tuotteet;

        if (tuotteet is null)
        {
            Console.WriteLine("Ei Tuotteita!");
            return;

        }


        foreach (Tuotteet tuote in tuotteet)
        {
            Console.WriteLine($"{ tuote.tuotenimi} : {tuote.varastosaldo}");
        }
    }
    Menu();
}

static void QueringProductByName(string name)
{
    using (Varastohallinta varastohallinta = new())
    {

        IQueryable<Tuotteet>? tuotteet = varastohallinta.tuotteet?.Where(tuotteet => tuotteet.tuotenimi == name);

        if (tuotteet is null)
        {
            Console.WriteLine("Ei Tuotteita!");
            return;

        }


        foreach (Tuotteet tuote in tuotteet)
        {
            Console.WriteLine($"{name} quantity is {tuote.varastosaldo}");
        }
    }
    Menu();
}

static bool AddProduct(string name, int stock, int price)
{

    using (Varastohallinta varastohallinta = new())
    {
        int last_id = varastohallinta.tuotteet?.Select(tuotteet => tuotteet.int_id).Max() ?? 0;

        Tuotteet tuotteet = new();
        {
            tuotteet.varastosaldo = stock;
            tuotteet.tuotenimi = name;
            tuotteet.tuotehinta = price;
            tuotteet.int_id = last_id + 1;
        }

        varastohallinta.tuotteet?.Add(tuotteet);

        int affected = varastohallinta.SaveChanges();
        return (affected == 1);
        Console.WriteLine($" Added {name} quantity: {stock} price:{price} to stock");

    }
    Menu();
}

static bool ChangeProductName(string newproductname, int int_id)
{
    using (Varastohallinta varastohallinta = new())
    {
        Tuotteet UpdateProduct = varastohallinta.tuotteet?.Find(int_id);

        if (UpdateProduct is null)
        {
            Console.WriteLine("Product not found");
            Menu();
            return false;
        }
        else
        {
            UpdateProduct.tuotenimi = newproductname;
            int affected = varastohallinta.SaveChanges();
            Menu();
            return (affected == 1);
        }
    }
}

static int DeleteProduct(int int_id)
{
    using (Varastohallinta varastohallinta = new())
    {
        Tuotteet DeleteProduct = varastohallinta.tuotteet?.Find(int_id);
        if (DeleteProduct is null)
        {
            Console.WriteLine("Product not found");
            Menu();
            return 0;

        }
        else
        {
            varastohallinta.tuotteet?.Remove(DeleteProduct);
            int affected = varastohallinta.SaveChanges();
            Console.WriteLine("Deleted product");
            Menu();
            return affected;
            Console.WriteLine("Deleted product");
        }
    }
}

static void Menu()
{
    Console.WriteLine("0. List all products");
    Console.WriteLine("1. Search product by name");
    Console.WriteLine("2. Add product to list");
    Console.WriteLine("3. Delete product from list");
    Console.WriteLine("4. Change product name");
    Console.WriteLine("5.Stop program");

    string input = Console.ReadLine();

    if( input == "0")
    {
        QueringStock();
    }
    else if( input == "1")
    {
        Console.WriteLine("Enter product name:");
        string name = Console.ReadLine();
        QueringProductByName(name);
    }
    else if( input == "2")
    {
        Console.WriteLine("Enter product name:");
        string name = Console.ReadLine();
        Console.WriteLine("Enter product stock:");
        int stock = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter product price:");
        int price = int.Parse(Console.ReadLine());
        AddProduct(name, stock, price);
    }
    else if( input == "3")
    {
        Console.WriteLine("Enter product ID to delete:");
        int id = int.Parse(Console.ReadLine());
        DeleteProduct(id);
    }
    else if( input == "4")
    {
        Console.WriteLine("Enter product ID to change name:");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter new product name:");
        string newName = Console.ReadLine();
        ChangeProductName(newName, id);
    }
    else if (input == "5")
    {
        System.Environment.Exit(1);
    }
    else
    {
        Console.WriteLine("Input not valid, try again.");
        return;
    }
}