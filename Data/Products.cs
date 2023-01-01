using System.Globalization;
namespace Coffee_Project.Data
{
    public class Products
    {
        public int Id { get; set; }
        public DrinkType Drink { get; set; }
        public decimal price { get; set; }
    }


    public enum DrinkType
    {
            Coffe,
            Tea
    }
}
