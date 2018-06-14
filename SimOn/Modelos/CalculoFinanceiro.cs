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
    public class CalculoFinanceiro
    {
        #region Declaracao de variaveis
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

        #region Inits
        public CalculoFinanceiro() { }

        public CalculoFinanceiro(Produto produto, double pvp, double entradaInicial, double residual, int duracacao, double taxa)
        {
            _produto = produto;
            _pvp = pvp;
            _entradaInicial = entradaInicial;
            _residual = residual;
            _duracacao = duracacao;
            _taxa = taxa;

            _financiado = _pvp - _entradaInicial;

            CalculaMensalidade();
        }
        #endregion

        #region Metodos privados
        private void CalculaMensalidade()
        {
            DueDate dueDate = this.Produto == Produto.credito ? DueDate.EndOfPeriod : DueDate.BegOfPeriod;
            _mensalidade = Math.Round(Financial.Pmt(Taxa/100/12, Duracacao, -Financiado, Residual, dueDate),2);
        }
        #endregion
    }
}
