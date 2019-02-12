namespace SimpleBanking.ConsoleApp
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using static SimpleBanking.ConsoleApp.Models;

    class Program
    {
        static readonly string baseUrl = "http://localhost:54784";
        static HttpClient client = new HttpClient();

        static async Task<SecurityToken> Authenticate(Login login)
        {
            var response = await client.PostAsJsonAsync($"/user/authenticate", new { Username = login.UserName, Password = login.Password });
            var token = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SecurityToken>(token);
        }

        static async Task<TransactionResult> BalanceAsync()
        {
            var response = await client.GetAsync($"/account/balance");
            return await DeserializeResponseContent(response);
        }

        static async Task<TransactionResult> DepositAsync(TransactionInput transactionDetails)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/account/deposit", transactionDetails);
            return await DeserializeResponseContent(response);
        }

        static async Task<TransactionResult> WithdrawAsync(TransactionInput transactionDetails)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/account/withdraw", transactionDetails);
            return await DeserializeResponseContent(response);
        }

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }        

        static async Task RunAsync()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Simple Transaction Processing");

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var login = ReadLoginDetails();
                var accessToken = await Authenticate(login);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken.auth_token);
                Console.WriteLine("\n Login Successfull.");

                DisplayMenu();

                string key;
                while ((key = Console.ReadKey().KeyChar.ToString()) != "4")
                {
                    int.TryParse(key, out int keyValue);

                    switch (keyValue)
                    {
                        case 1:
                            await ShowBalance();
                            break;
                        case 2:
                            await MakeTransaction(TransactionType.Deposit);
                            break;
                        case 3:
                            await MakeTransaction(TransactionType.Withdrawal);
                            break;
                    }

                    Console.Write("Enter the option (number): ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("App interrupted.");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("App closed.");
            }

            Console.ReadLine();
        }

        static string ReadAccountNumber()
        {
            Console.WriteLine();
            Console.Write("Enter the account Number: ");
            var accountNumber = Console.ReadLine();
            return accountNumber;
        }

        static Login ReadLoginDetails()
        {
            Console.WriteLine();
            Console.Write("Enter the user name: ");
            var username = Console.ReadLine();
            Console.Write("Enter the password: ");
            var password = Console.ReadLine();
            return new Login() { UserName = username, Password = password };
        }

        static void DisplayMenu()
        {            
            Console.WriteLine();
            Console.WriteLine("1. Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Close app (X)");
            Console.WriteLine();
            Console.Write("Enter the option (number): ");
        }

        static async Task ShowBalance()
        {
            var transactionResult = await BalanceAsync();

            Console.WriteLine();
            Console.WriteLine("Balance");
            Console.WriteLine();

            if(transactionResult.Balance != null)
            {
                Console.WriteLine($"Account No: {transactionResult.AccountNumber}");
                Console.WriteLine($"Balance: {transactionResult.Balance}");
                Console.WriteLine($"Currency: {transactionResult.Currency}");
            }
            else
            {
                Console.WriteLine($"Status: Transaction failed");
                Console.WriteLine($"Message: {transactionResult.Message}");
            }

            Console.WriteLine();
        }

        static async Task MakeTransaction(TransactionType transactionType)
        {
            Console.WriteLine();

            Console.Write("Enter the Amount: ");
            var transactionAmount = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(transactionType.ToString());
            Console.WriteLine();

            var transactionInput = new TransactionInput() {
                TransactionType = transactionType,
                Amount = Math.Round(Convert.ToDecimal(transactionAmount), 2)
            };

            var transactionResult = new TransactionResult();
            if (transactionType == TransactionType.Deposit)
            {
                transactionResult = await DepositAsync(transactionInput);
            }
            else if(transactionType == TransactionType.Withdrawal)
            {
                transactionResult = await WithdrawAsync(transactionInput);
            }

            if(transactionResult.IsSuccessful)
            {
                Console.WriteLine($"Status: {transactionResult.Message}");
                Console.WriteLine($"Account No: {transactionResult.AccountNumber}");
                Console.WriteLine($"Current Balance: {transactionResult.Balance}");
                Console.WriteLine($"Currency: {transactionResult.Currency}");                                
            }
            else
            {
                Console.WriteLine($"Status: Transaction failed");
                Console.WriteLine($"Message: {transactionResult.Message}");               
            }

            Console.WriteLine();
        }

        static async Task<TransactionResult> DeserializeResponseContent(HttpResponseMessage response)
        {
            var transactionResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TransactionResult>(transactionResult);
        }
    }
}
