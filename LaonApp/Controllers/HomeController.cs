using LaonApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LaonApp.LoanHelper;

namespace LaonApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult App(Loan loan)
        {

            //var loanPayments = new Loan();

            var totalInterest = 0.00m;
            var interestPayment = 0.00m;
            var principalPayment = 0.00m;
            var totalCost = 0.00m;
            var totalPrincipal = 0.00m;

            //var loanTerm = loan.Term * 12;
            var loanHelper = new LoanHelper.LoanUtils();
            totalPrincipal = loan.Amount;

            var monthlyPayment = loanHelper.CalcPayment(loan.Amount, loan.Rate, loan.Term);
            var remainingBalance = loan.Amount;
            

            for (int i = 1; i <= loan.Term; i++)
            {
                
                interestPayment = loanHelper.CalcMonthyInterest(remainingBalance, loan.Rate);
                
                principalPayment = monthlyPayment - interestPayment;
                totalPrincipal += monthlyPayment - interestPayment;

                var loanPayment = new LoanPayment();
                
                loanPayment.Month = i;
                
                loanPayment.MonthlyInterest = interestPayment;
                
                loanPayment.Payment = monthlyPayment;
                
                loanPayment.MonthlyPrincipal = principalPayment;
                
                totalInterest = totalInterest + interestPayment;
                
                loanPayment.TotalInterest = totalInterest;
                
                remainingBalance -= principalPayment;
                
                loanPayment.Balance = remainingBalance;
                
               

                loan.Payments.Add(loanPayment);
            }

            loan.Payment = monthlyPayment;
            
            
            loan.TotalInterest = totalInterest;
            loan.TotalPrincipal = totalPrincipal;
            loan.TotalCost = totalPrincipal + totalInterest;

            return View(loan);
        }
        
        
        // add the post
        public IActionResult App() 
        {
          
            Loan loan = new Loan();

            return View(loan);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
