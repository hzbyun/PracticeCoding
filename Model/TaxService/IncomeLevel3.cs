using System;
using PracticeCoding.Abstract.TaxService;

namespace PracticeCoding.Model.TaxService
{
    public class IncomeLevel3 : IncomeLevel
    {
        public IncomeLevel3() : base(80000, Int32.MaxValue, 0.3, 0.035)
        {

        }
    }
}
