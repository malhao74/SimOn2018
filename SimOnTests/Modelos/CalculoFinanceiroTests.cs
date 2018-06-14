using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimOn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOn.Tests
{
    [TestClass()]
    public class CalculoFinanceiroTests
    {
        [TestMethod()]
        public void CalculoFinanceiroTestCredito()
        {
            Produto produto = Produto.credito;
            double pvp = 9900;
            double entradaInicial = 0;
            double residual = 0;
            int duracacao = 18;
            double taxa = 4.836;

            double prestacaoEsperada = 571.30;

            CalculoFinanceiro calculoFinanceiro = new CalculoFinanceiro(produto, pvp, entradaInicial, residual, duracacao, taxa);

            Assert.AreEqual(prestacaoEsperada, calculoFinanceiro.Mensalidade);
        }
    }
}