using System;
using PracticeCoding.Abstract.TaxService;
using PracticeCoding.Model.TaxService;

namespace PracticeCoding.Service
{
    public class TaxService
    {
        //Previous code
        public static int GetTaxPayable(int income)
        {
            int tax = 0;
            int levy = 0;

            if (income <= 40000)
            {
                tax = Convert.ToInt32(income * 0.1);
                levy = Convert.ToInt32(income * 0.015);
            }
            else if (income <= 80000)
            {
                tax = Convert.ToInt32(40000 * 0.1) + Convert.ToInt32((income - 40000) * 0.2);
                levy = Convert.ToInt32(income * 0.025);
            }
            else
            {
                tax = Convert.ToInt32(40000 * 0.1) + Convert.ToInt32(40000 * 0.2) +
                      Convert.ToInt32((income - 80000) * 0.3);
                levy = Convert.ToInt32(income * 0.035);
            }

            return tax + levy;
        }

        //My new trial for applying a design pattern
        public static int GetTaxPayable_UsingChainOfResponsibilityPattern(int income)
        {
            //TODO: In real situation, this settup would be done in more initial stage.
            IncomeLevel incomeLevel1 = new IncomeLevel1();
            IncomeLevel2 incomeLevel2 = new IncomeLevel2();
            IncomeLevel3 incomeLevel3 = new IncomeLevel3();
            incomeLevel1.SetNextTaxLevel(incomeLevel2);
            incomeLevel2.SetNextTaxLevel(incomeLevel3);

            return incomeLevel1.GetTaxPayable(income);
        }

        public static int GetTaxPayable_UsingLoop(int income)
        {
            var incomeLevels = new[]
            {
                new { LowerLimit = 0, UpperLimit = 40000, TaxRate = 0.1, LevyRate = 0.015 },
                new { LowerLimit = 40000, UpperLimit = 80000, TaxRate = 0.2, LevyRate = 0.025 },
                new { LowerLimit = 80000, UpperLimit = Int32.MaxValue, TaxRate = 0.3, LevyRate = 0.035 }
            };

            int totalTax = 0;
            var levyRateForThisIncome = 0.0D;

            foreach (var lev in incomeLevels)
            {
                if (income > lev.LowerLimit)
                {
                    var taxableAtThisLevel = Math.Min(lev.UpperLimit - lev.LowerLimit, 
                                                      income - lev.LowerLimit);
                    var taxAtThisLevel = taxableAtThisLevel * lev.TaxRate;
                    totalTax += Convert.ToInt32(taxAtThisLevel);
                    levyRateForThisIncome = lev.LevyRate;
                }                
            }

            return totalTax + Convert.ToInt32((income * levyRateForThisIncome));
        }
    }
}
