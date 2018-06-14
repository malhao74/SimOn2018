using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace SimOn
{
    public enum Produto {  credito, leasing }
    /// <summary>
    /// Class that implements financial calculations.
    /// </summary>
    class CalculoFinanceiro
    {
        #region Variable definition.
        private readonly Produto _produto;
        private readonly double _pvp;
        private readonly double _entradaInicial;
        private readonly double _residual;
        private readonly int _duracacao;
        private readonly double _taxa;
        private readonly double _financiado;
        private double _mensalidade;

        private readonly string _errorMsg = "";

        public Produto Produto { get { return _produto; } }
        public double Pvp { get { return _pvp; } }
        public double EntradaInicial { get { return _entradaInicial; } }
        public double Residual {  get { return _residual; } }
        public int Duracacao { get { return _duracacao; } }
        public double Taxa {  get { return _taxa; } }
        public double Financiado {  get { return _financiado; } }
        public double Mensalidade {  get { return _mensalidade; } }

        public string ErrorMsg {  get { return _errorMsg; } }
        #endregion

        #region Constructors.
        public CalculoFinanceiro() { }

        public CalculoFinanceiro(Produto produto, double pvp, double entradaInicial, double residual, int duracacao, double taxa)
        {
            this._produto = produto;
            this._pvp = pvp;
            this._entradaInicial = entradaInicial;
            this._residual = residual;
            this._duracacao = duracacao;
            this._taxa = taxa;

            this._financiado = this._pvp - this._entradaInicial;

            this.CalculaMensalidade();
        }
        #endregion

        #region Private methods.
        private void CalculaMensalidade()
        {
            DueDate dueDate = this.Produto == Produto.credito ? DueDate.EndOfPeriod : DueDate.BegOfPeriod;
            this._mensalidade = Financial.Pmt(this.Taxa/100/12, this.Duracacao, -this.Financiado, this.Residual, dueDate);
        }
        #endregion
    }
}
