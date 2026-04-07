using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter username> ");
        var username = Console.ReadLine();

        Console.Write("Enter password> ");
        StringBuilder passwordBuilder = new StringBuilder();
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key != ConsoleKey.Enter)
            {
                passwordBuilder.Append(keyInfo.KeyChar);
                Console.Write("*");
            }
            if (keyInfo.Key == ConsoleKey.Backspace && passwordBuilder.Length > 0)
            {
                passwordBuilder.Remove(passwordBuilder.Length - 1, 1);
                Console.Write("\b \b");
            }
        } while (keyInfo.Key != ConsoleKey.Enter);
        Console.WriteLine(passwordBuilder);


        try
        {
            using (var conn = new HttpClient())
            {
                var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{passwordBuilder}\"}}", Encoding.UTF8, "application/json");

                var response = await conn.PostAsync("https://localhost:7023/User/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"\nToken: {token}");
                }
                else
                {
                    Console.WriteLine($"\nLogin failed: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR");
        }
    }
}
