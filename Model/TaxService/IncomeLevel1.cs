using System;
using PracticeCoding.Abstract.TaxService;

namespace PracticeCoding.Model.TaxService
{
    public class IncomeLevel1 : IncomeLevel
    {
        public IncomeLevel1() : base(0, 40000, 0.1, 0.015)
        {
            
        }
    }

}
