using System.Collections.Generic;

namespace PersonalStoreApplication.Models
{
    public class CheckoutModel
    {
        public List<CartItemDTO> CartList { get; set; }
        public List<Address> Addresses { get; set; }
        public PaymentType Payment { get; set; }
    }

    public enum PaymentType
    {
        Credit = 0
    }
}
