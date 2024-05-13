using System;
using Stripe;

namespace authentication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Authetication
            StripeConfiguration.ApiKey = "sk_test_51PEjznCOhNPMeOlAEwI4cILm9DIZ5dA5Y7VciTgJl5aOpl4pxaL8lJi1gj4vjjpOharIIk1J7VWiZytH887bPh6M00ysnoC12r";

            //Create a Product
            var product = new ProductCreateOptions
            {
                Name = "Car Wash",
            };

            var serviceProduct = new ProductService();
            serviceProduct.Create(product);

            //Create a price
            var price = new PriceCreateOptions
            {
                Currency = "usd",
                UnitAmount = 3000,
                Product = serviceProduct.List().Data[0].Id,
                
            };

            var servicePrice = new PriceService();
            servicePrice.Create(price);

            //Create a payment intent
            var paymentIntent = new PaymentIntentCreateOptions
            {
                Amount = servicePrice.List().Data[0].UnitAmount,
                Currency = "usd",
                Customer = "cus_Q4ttTADSzfyF4T",
                Description = serviceProduct.List().Data[0].Name,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };

            var payments = new PaymentIntentService();
            payments.Create(paymentIntent);

            var lisPayments = payments.List();
            foreach (var payment in lisPayments)
            {
                Console.WriteLine(payment);
            }

        }
    }
}


