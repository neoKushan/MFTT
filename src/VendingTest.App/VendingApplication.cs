namespace VendingTest.App
{
    using System;
    using System.Linq;
    using Core.Models;
    using Microsoft.Extensions.Logging;

    public class VendingApplication
    {
        private MenuMode menuMode = MenuMode.Main;
        private bool quit = false;
        private readonly ILogger<VendingApplication> logger;
        private readonly VendingMachine machine;

        public VendingApplication(ILogger<VendingApplication> logger, VendingMachine machine)
        {
            this.logger = logger;
            this.machine = machine;
        }

        public void Run()
        {
            this.logger.LogDebug($"Application started");

            do
            {
                this.WriteScreen();
                var selection = Console.ReadKey();
                this.ProcessInput(selection.KeyChar);

            } while (!this.quit);

            this.logger.LogDebug("Application Terminated");
        }

        private void ProcessInput(char selectionKeyChar)
        {
            switch (this.menuMode)
            {
                case MenuMode.Main:
                    this.ProcessMainMenu(selectionKeyChar);
                    break;
                case MenuMode.InsertCoin:
                    this.ProcessInsertCoinMenu(selectionKeyChar);
                    break;
                case MenuMode.ViewProducts:
                    this.ProcessViewProductsMenu(selectionKeyChar);
                    break;
                case MenuMode.Vend:
                    this.ProcessViewProductsMenu(selectionKeyChar);
                    break;
                case MenuMode.Refund:
                    this.ProcessRefundMenu(selectionKeyChar);
                    break;
                default:
                    break;
            }
        }

        private void ProcessRefundMenu(in char selectionKeyChar) => throw new NotImplementedException();

        private void ProcessViewProductsMenu(in char selectionKeyChar) => throw new NotImplementedException();

        private void ProcessInsertCoinMenu(char selectionKeyChar)
        {
            var enteredValue = (int)char.GetNumericValue(selectionKeyChar);

            if (enteredValue > 0 && enteredValue < 9)
            {
                this.InsertCoin(enteredValue);
            }

            if (selectionKeyChar == 'r' || selectionKeyChar == 'R')
            {
                this.menuMode = MenuMode.Main;
            }
        }

        private void InsertCoin(int coinIndex)
        {
            var coinList = ValidCoin.List().ToList();
            if (coinIndex >= coinList.Count)
            {
                return;
            }

            var insertedCoin = new InsertedCoin()
            {
                Diameter = coinList[coinIndex].Diameter, Weight = coinList[coinIndex].Weight
            };
            this.machine.InsertCoin(insertedCoin);
        }

        private void ProcessMainMenu(char selectionKeyChar)
        {
            switch (selectionKeyChar)
            {
                case '1':
                    this.menuMode = MenuMode.InsertCoin;
                    break;
                case '2':
                    this.menuMode = MenuMode.ViewProducts;
                    break;
                case '3':
                    this.menuMode = MenuMode.Vend;
                    break;
                case '4':
                    this.menuMode = MenuMode.Refund;
                    break;
                case 'q':
                case 'Q':
                    this.quit = true;
                    break;
            }
        }

        private void WriteScreen()
        {
            Console.Clear();
            Console.WriteLine($"Vending Display: {this.machine.CoinDisplay}");
            Console.WriteLine("-----------------------");
            switch (this.menuMode)
            {
                case MenuMode.Main:
                    this.WriteMainMenu();
                    break;
                case MenuMode.InsertCoin:
                    this.WriteInsertCoinMenu();
                    break;
                case MenuMode.ViewProducts:
                    this.WriteViewProductsMenu();
                    break;
                case MenuMode.Vend:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine();
            Console.Write("Selection: ");
        }

        private void WriteViewProductsMenu() => throw new NotImplementedException();

        private void WriteMainMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("");
            Console.WriteLine("1. Insert Coin");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Vend Products");
            Console.WriteLine("4. Refund Coins");
            Console.WriteLine("");
            Console.WriteLine("Q. Quit");
        }

        private void WriteInsertCoinMenu()
        {
            Console.WriteLine("What Coin would you like to insert?");
            Console.WriteLine("");
            var index = 1;
            foreach (var coin in ValidCoin.List())
            {
                Console.WriteLine(coin == ValidCoin.Unknown
                    ? $"{index}. Some weird object you found in your pocket"
                    : $"{index}. {coin.Name}");

                index++;
            }
            Console.WriteLine("");
            Console.WriteLine("R. Return");
            Console.WriteLine("Q. Quit");
        }
    }
}
