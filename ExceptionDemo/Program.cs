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
                Console.WriteLine($"Format error in ProcessFile: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Cannot divide by zero: {ex.Message}");
            }
            // Handle numbers outside the int range with a specific error message.
            catch (OverflowException ex)
            {
                Console.WriteLine($"The number is outside the allowed int range: {ex.Message}");
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

            // Removed the general catch so exceptions keep their original types and reach the matching catch blocks in Main.
            StreamReader? reader = null;
            try
            {
                reader = new StreamReader(fileName);

                string? line = reader.ReadLine();
                if (line == null)
                    throw new InvalidOperationException("The file is empty.");

                int number = int.Parse(line);

                // Double division by zero returns Infinity, so throw explicitly to let Main catch DivideByZeroException.
                if (number == 0)
                {
                    throw new DivideByZeroException("The number in the file cannot be zero.");
                }

                return 100.0 / number;
            }
            //Delete unnecessary message. I did not delete whole catch block because it is the part of the exercise to show how to rethrow an exception.
            catch (FormatException)
            {
                throw;
            }
            finally
            {
                // Close the reader only if it was successfully created.
                if (reader != null)
                {
                    reader.Close();
                    Console.WriteLine("finally in ProcessFile: StreamReader closed.");
                }
            }
        }
    }
}
