using System.Collections.Generic;
using System.ComponentModel;

namespace PersonalStoreApplication.Models
{
    public class CheckoutModel
    {
        public List<CartItemDTO> CartList { get; set; }
        public List<Address> Addresses { get; set; }

        [DisplayName("Payment Type:")]
        public PaymentType Payment { get; set; }
    }

    public enum PaymentType
    {
        Credit = 0,
        Debit = 1
    }
}
