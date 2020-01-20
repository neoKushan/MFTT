namespace VendingTest.Core.Models
{
    using System.Collections.Generic;

    public class ValidCoin
    {
        // Data sourced from https://mdmetric.com/tech/coinmeasure.htm
        public static ValidCoin Cent { get; } = new ValidCoin("Cent", 0.01m, 2.5f, 19.05f);
        public static ValidCoin Nickel { get; } = new ValidCoin("Nickel", 0.05m, 5.0f, 21.21f);
        public static ValidCoin Dime { get; } = new ValidCoin("Dime", 0.10m, 2.268f, 17.91f);
        public static ValidCoin Quarter { get; } = new ValidCoin("Quarter", 0.25m, 5.67f, 24.26f);
        public static ValidCoin HalfDollar { get; } = new ValidCoin("Half Dollar", 0.50m, 11.34f, 30.61f);
        public static ValidCoin Dollar { get; } = new ValidCoin("Dollarydoo", 1m, 8.1f, 26.5f);

        public static ValidCoin Unknown { get; } = new ValidCoin("Unknown", 0, 0f, 0f);

        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public float Weight { get; private set; }
        public float Diameter { get; private set; }

        private ValidCoin(string name, decimal value, float weight, float diameter)
        {
            this.Name = name;
            this.Value = value;
            this.Weight = weight;
            this.Diameter = diameter;
        }

        public static IEnumerable<ValidCoin> List() => new[] { Cent, Nickel, Dime, Quarter, HalfDollar, Dollar };
    }


}
