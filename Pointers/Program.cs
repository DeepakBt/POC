// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
unsafe
{
    int x = 100;
    int* ptr = &x;
    Console.WriteLine((int)ptr); // Displays the memory address  
    Console.WriteLine(*ptr);
}
Console.ReadLine();
