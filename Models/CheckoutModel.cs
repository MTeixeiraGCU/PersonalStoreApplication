using System.Collections.Generic;
using System.ComponentModel;

namespace PersonalStoreApplication.Models
{
    /// <summary>
    /// This class model is used as a wrapper for all the information used during user checkout.
    /// </summary>
    public class CheckoutModel
    {
        //The user's cart items.
        public List<CartItemDTO> CartList { get; set; }

        //The user's saved addresses.
        public List<Address> Addresses { get; set; }

        //The payment type for this transaction.
        [DisplayName("Payment Type:")]
        public PaymentType Payment { get; set; }
    }

    /// <summary>
    /// This enumeration represents a payment type for product purchases.
    /// </summary>
    public enum PaymentType
    {
        Credit = 0,
        Debit = 1
    }
}
