using System;
using System.Runtime.InteropServices;

namespace PracticeCoding.Abstract.TaxService
{
    public abstract class IncomeLevel
    {
        private int _lowerLimit;
        private int _upperLimit;
        private readonly double _taxRate = 0;
        private readonly double _levyRate = 0;

        //public abstract int GetTaxPayable(int income);
        
        protected IncomeLevel nextIncomeLevel;
        public void SetNextTaxLevel(IncomeLevel incomeLevel)
        {
            this.nextIncomeLevel = incomeLevel;
        }

        protected IncomeLevel(int lowerLimit, int upperLimit, double taxRate, double levyRate)
        {
            this._lowerLimit = lowerLimit;
            this._upperLimit = upperLimit;
            this._taxRate = taxRate;
            this._levyRate = levyRate;
        }

        public int GetTaxPayable(int income)
        {
            if (nextIncomeLevel == null)
            {
                var taxableAtCurrentLevel = income - _lowerLimit;
                return GetTotalTaxByTaxRate(taxableAtCurrentLevel, _taxRate) +
                       GetTotalLevyByLevyRate(income, _levyRate);
            }

            if (income <= _upperLimit)
            {
                var taxableAtCurrentLevel = Math.Min(
                    _upperLimit - _lowerLimit,
                    income - _lowerLimit);
                return GetTotalTaxByTaxRate(taxableAtCurrentLevel, _taxRate) +
                       GetTotalLevyByLevyRate(income, _levyRate);
            }

            var taxPayableAtCurrentLevel = (_upperLimit - _lowerLimit) * _taxRate;
            return Convert.ToInt32(taxPayableAtCurrentLevel) + nextIncomeLevel.GetTaxPayable(income);
        }

        private int GetTotalTaxByTaxRate(int amount, double taxRate)
        {
            return Convert.ToInt32(amount * taxRate);
        }

        private int GetTotalLevyByLevyRate(int amount, double levyRate)
        {
            return Convert.ToInt32(amount * levyRate);
        }
    }
}
