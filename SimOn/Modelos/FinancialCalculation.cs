using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace SimOn
{
    public enum FinancialProduct {  Credit, Leasing }
    /// <summary>
    /// Class that implements financial calculations.
    /// </summary>
    public class FinancialCalculation
    {
        #region Fields & properties
        private readonly FinancialProduct _product;
        private readonly double _vehiclePrice;
        private readonly double _downPayment;
        private readonly double _residual;
        private readonly int _numberMonths;
        private readonly double _rate;
        private readonly double _financedAmount;
        private double _monthlyPayment;

        public FinancialProduct Product { get { return _product; } }
        public double VehiclePrice { get { return _vehiclePrice; } }
        public double DownPayment { get { return _downPayment; } }
        public double Residual {  get { return _residual; } }
        public int NumberMonths { get { return _numberMonths; } }
        public double Rate {  get { return _rate; } }
        public double FinancedAmount {  get { return _financedAmount; } }
        public double MonthlyPayment {  get { return _monthlyPayment; } }

        #endregion

        public FinancialCalculation() { }

        public FinancialCalculation(FinancialProduct product, double vehiclePrice, double downPayment, double residual, int numberMonths, double rate)
        {
            _product = product;
            _vehiclePrice = vehiclePrice;
            _downPayment = downPayment;
            _residual = residual;
            _numberMonths = numberMonths;
            _rate = rate;

            _financedAmount = _vehiclePrice - _downPayment;

            MonthlyPaymentCalculation();
        }

        private void MonthlyPaymentCalculation()
        {
            try
            {
                DueDate dueDate = this.Product == FinancialProduct.Credit ? DueDate.EndOfPeriod : DueDate.BegOfPeriod;
                _monthlyPayment = Math.Round(Financial.Pmt(Rate / 100 / 12, NumberMonths, -FinancedAmount, Residual, dueDate), 2);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }
    }
}
