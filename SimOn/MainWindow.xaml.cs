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
        #region Declaracao de variaveis
        // Variable needed to update the UI from within the asynchronous task.
        private readonly SynchronizationContext synchronizationContext;
        private DataSource DataSource { get; set; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
        }

        #region Eventos
        // Click do calculo da prestacao
        private void ButCalcular_Click(object sender, RoutedEventArgs e)
        {
            double pvp = txtPreco.GetValue();
            double entradaInicial = txtEntradaInicial.GetValue();
            int duracao = Convert.ToInt32(this.txtDuracao.Text);
            double taxa = Convert.ToDouble(this.txtTaxa.Text);
            double residual = txtResidual.GetValue();

            CalculoFinanceiro calculo = new CalculoFinanceiro(Produto.credito,
                                                                pvp,
                                                                entradaInicial,
                                                                residual,
                                                                duracao,
                                                                taxa);
            txtMensalidade.Text = calculo.Mensalidade.ToString("C");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDataSource();

            lblStatus.Content = "A procurar as marcas disponiveis...";
            var taskFeetchedMarcas = Task.Run(() =>
            {
                List<Marca> marcas = DataLayer.GetMarcas(DataSource);
                CarregaMarcas(marcas);
            });

            Cursor = Cursors.Wait;
        }

        // Escolheu uma marca, vai buscar os modelos associados à marca.
        private void CbMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                lblStatus.Content = "A procurar os modelos disponiveis...";
                var taskFeetchedMarcasModelos = Task.Run(() =>
                {
                    Marca marca = (Marca)e.AddedItems[0];
                    List<MarcaModelo> modelos = DataLayer.GetModelos(DataSource, marca);
                    CarregaModelos(modelos);
                });

                Cursor = Cursors.Wait;
            }
        }

        // Escolheu um modelo, vai buscar as versões associadas ao modelo.
        private void CbModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                this.lblStatus.Content = "A procurar as versões disponiveis...";
                var taskFeetchedVersoes = Task.Run(() =>
                {
                    MarcaModelo modelo = (MarcaModelo)e.AddedItems[0];
                    List<MarcaModeloVersao> versoes = DataLayer.GetVersoes(DataSource, modelo);
                    CarregaVersoes(versoes);
                });

                this.Cursor = Cursors.Wait;

            }
        }

        // Escolheu uma versao, vai o preço da mesma.
        private void CbVersoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                this.lblStatus.Content = "A procurar pvp...";
                var taskFeetchedViatura = Task.Run(() =>
                {
                    MarcaModeloVersao versao = (MarcaModeloVersao)e.AddedItems[0];                    
                    Viatura viatura = DataLayer.GetViatura(DataSource, versao);
                    this.ActualizaPreco(viatura);
                });
                this.Cursor = Cursors.Wait;
            }
        }

        // Quando um dos radiobuttons da datasource e carregado, define a datasource a usar.
        private void Rb_Checked(object sender, RoutedEventArgs e)
        {
            SetDataSource();
        }
        #endregion

        #region Metodos privados
        private void CarregaMarcas(List<Marca> fetchedMarcas)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.cbMarcas.DisplayMemberPath = "DescricaoMarca";
                this.cbMarcas.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedMarcas });
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), fetchedMarcas);
        }

        private void CarregaModelos(List<MarcaModelo> fetchedMarcaModelos)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.cbModelos.DisplayMemberPath = "DescricaoModelo";
                this.cbModelos.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedMarcaModelos });
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), fetchedMarcaModelos);
        }

        private void CarregaVersoes(List<MarcaModeloVersao> fetchedVersoes)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.cbVersoes.DisplayMemberPath = "DescricaoVersao";
                this.cbVersoes.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedVersoes });
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), fetchedVersoes);
        }

        private void ActualizaPreco(Viatura viatura)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                this.txtPreco.Text = viatura.PrecoNovo.ToString();
                this.Cursor = Cursors.Arrow;
                this.lblStatus.Content = "";
            }), viatura);
        }

        // Define a datasource a usar com base nos radiobuttons.
        private void SetDataSource()
        {
            if (rbExcel.IsChecked == true)
            {
                DataSource = DataSource.Excel;
            }
            else if (rbXml.IsChecked == true)
            {
                DataSource = DataSource.XML;
            }
            else
            {
                DataSource = DataSource.FireBase;
            }
        }
        #endregion
    }
}