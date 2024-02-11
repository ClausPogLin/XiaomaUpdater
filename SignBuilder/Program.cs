
namespace SignBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            start:
            SignBuilder sb = new SignBuilder();
            var or = Console.ReadLine();
            Console.WriteLine(sb.GetSign(or));
            goto start;
        }
    }
}