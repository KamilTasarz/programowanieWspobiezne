namespace Concurrent
{
    public class Program
    {
        static void Main(string[] args)
        {
            DivideClass divideClass = new DivideClass();
              
            Console.WriteLine(divideClass.divide(2.5, 0.5));
            try
            {
                Console.WriteLine(divideClass.divide(2.5, 0));
            } catch (DivideByZeroException e)
            {
                Console.WriteLine("DivideByZero exception");
            } 
            
        }
    }
}
