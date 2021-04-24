using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaonApp.LoanHelper
{
    public class LoanUtils
    {
        public decimal CalcPayment (decimal amount, decimal rate,int term)
        {
            var rateD = Convert.ToDouble(rate/1200);
            var amountD = Convert.ToDouble(amount);
            var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -term));

            return Convert.ToDecimal(paymentD);
        }

        public decimal CalcMonthyRate(decimal rate)
        {
            return rate / 1200m;
        }

        public decimal CalcMonthyInterest(decimal balance, decimal rate)
        {
            return balance * (rate / 1200);
        }

       
    }
    
}
