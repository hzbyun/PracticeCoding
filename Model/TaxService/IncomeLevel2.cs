using System;
using PracticeCoding.Abstract.TaxService;

namespace PracticeCoding.Model.TaxService
{
    public class IncomeLevel2 : IncomeLevel
    {
        public IncomeLevel2() : base(40000, 80000, 0.2, 0.025)
        {
        }
    }
}
