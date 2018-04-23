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
        private Produto _produto;
        private double _pvp;
        private double _entradaInicial;
        private double _residual;
        private int _duracacao;
        private double _taxa;
        private double _financiado;
        private double _mensalidade;

        private string _errorMsg = "";

        public Produto produto { get { return _produto; } }
        public double pvp { get { return _pvp; } }
        public double entradaInicial { get { return _entradaInicial; } }
        public double residual {  get { return _residual; } }
        public int duracacao { get { return _duracacao; } }
        public double taxa {  get { return _taxa; } }
        public double financiado {  get { return _financiado; } }
        public double mensalidade {  get { return _mensalidade; } }

        public string errorMsg {  get { return _errorMsg; } }
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

            this.calculaMensalidade();
        }
        #endregion

        #region Private methods.
        private void calculaMensalidade()
        {
            DueDate dueDate = this.produto == Produto.credito ? DueDate.EndOfPeriod : DueDate.BegOfPeriod;
            this._mensalidade = Financial.Pmt(this.taxa/100/12, this.duracacao, -this.financiado, this.residual, dueDate);
        }
        #endregion
    }
}
