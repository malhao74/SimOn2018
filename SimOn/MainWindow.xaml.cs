using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimOn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variable definition.
        // Variable needed to update the UI from within the asynchronous task.
        private readonly SynchronizationContext synchronizationContext;
        private DataSource DataSource { get {
                if (rbExcel.IsChecked == true)
                { return DataSource.Excel; }
                else if (rbXml.IsChecked == true)
                { return DataSource.XML; }
                else
                { return DataSource.FireBase; }
            } }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;           
        }

        #region Events.
        // Button to calculate the instalment.
        private void ButCalcular_Click(object sender, RoutedEventArgs e)
        {
            double pvp = Convert.ToDouble(this.txtPreco.Text);
            double entradaInicial = Convert.ToDouble(this.txtEntradaInicial.Text);
            int duracao = Convert.ToInt32(this.txtDuracao.Text);
            double taxa = Convert.ToDouble(this.txtTaxa.Text);
            double residual = Convert.ToDouble(this.txtResidual.Text);
            
            CalculoFinanceiro calculo = new CalculoFinanceiro(Produto.credito,
                                                                pvp,
                                                                entradaInicial,
                                                                residual,
                                                                duracao,
                                                                taxa);
            this.txtMensalidade.Text = calculo.Mensalidade.ToString("C");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   
            this.lblStatus.Content = "A procurar as marcas disponiveis...";
            var taskFeetchedMarcas = Task.Run(() =>
            {
                List<Marca> marcas = DataLayer.GetMarcas();
                CarregaMarcas(marcas);
            });

            this.Cursor = Cursors.Wait;
        }

        private void CbMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                this.lblStatus.Content = "A procurar os modelos disponiveis...";
                var taskFeetchedMarcasModelos = Task.Run(() =>
                {
                    Marca marca = (Marca)e.AddedItems[0];
                    List<MarcaModelo> modelos = DataLayer.GetModelos(marca);
                    CarregaModelos(modelos);
                });

                this.Cursor = Cursors.Wait;
            }
        }

        private void CbModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                this.lblStatus.Content = "A procurar as versões disponiveis...";
                var taskFeetchedVersoes = Task.Run(() =>
                {
                    MarcaModelo modelo = (MarcaModelo)e.AddedItems[0];
                    List<MarcaModeloVersao> versoes = DataLayer.GetVersoes(modelo);
                    CarregaVersoes(versoes);
                });

                this.Cursor = Cursors.Wait;

            }
        }

        private void CbVersoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                this.lblStatus.Content = "A procurar pvp...";
                var taskFeetchedViatura = Task.Run(() =>
                {
                    MarcaModeloVersao versao = (MarcaModeloVersao)e.AddedItems[0];
                    Viatura viatura = DataLayer.GetViatura(versao);
                    this.ActualizaPreco(viatura);
                });
                this.Cursor = Cursors.Wait;
            }
        }
        #endregion

        #region Private methods.
        private void CarregaMarcas(List<Marca> feetchedMarcas)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.cbMarcas.DisplayMemberPath = "descricaoMarca";
                this.cbMarcas.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = feetchedMarcas });
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), feetchedMarcas);
        }

        private void CarregaModelos(List<MarcaModelo> feetchedMarcaModelos)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.cbModelos.DisplayMemberPath = "descricaoModelo";
                this.cbModelos.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = feetchedMarcaModelos });
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), feetchedMarcaModelos);
        }

        private void CarregaVersoes(List<MarcaModeloVersao> feetchedVersoes)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.cbVersoes.DisplayMemberPath = "descricaoVersao";
                this.cbVersoes.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = feetchedVersoes });
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), feetchedVersoes);
        }

        private void ActualizaPreco(Viatura viatura)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.txtPreco.Text = viatura.precoNovo.ToString();
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), viatura);
        }

        private void Rb_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}