namespace ExceptionDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Program started ===");

            try
            {
                Console.WriteLine("Trying to read the file and calculate...");
                var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
                var result = ProcessFile(path);

                Console.WriteLine($"\nResult: {result}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format error: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Cannot divide by zero: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Cleanup: Finished logging the call.");
            }

            Console.WriteLine("The program exits normally.");
        }

        static double ProcessFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("The file name must not be empty or null.", nameof(fileName));
            }

            StreamReader? reader = null;
            try
            {
                reader = new StreamReader(fileName);

                string? line = reader.ReadLine();
                if (line == null)
                    throw new InvalidOperationException("The file is empty.");

                int number = int.Parse(line);

                return 100.0 / number;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format error in ProcessFile: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                "The file could not be processed.",
                ex);
            }
            finally
            {
                reader?.Close();
                Console.WriteLine("finally in ProcessFile: StreamReader closed.");
            }
        }
    }
}
