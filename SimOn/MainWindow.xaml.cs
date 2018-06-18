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
    public partial class MainWindow : Window
    {
        #region Declaracao de variaveis
        // Variavel necessaria para actualizar UI dentro de uma tarefa assincrona
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

        private void WindowSimOn_Loaded(object sender, RoutedEventArgs e)
        {
            //rbXml.IsChecked = true;


            //SetDataSource();
            //List<Marca> marcas = DataLayerXml.GetMarcas();
            //BuscarPreencheMarcas();
        }

        // Escolheu uma marca, vai buscar os modelos associados à marca.
        private void CbMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                ActualizaStatusRato("A procurar os modelos disponiveis...");
                var taskFeetchedMarcasModelos = Task.Run(() =>
                {
                    Marca marca = (Marca)e.AddedItems[0];
                    List<MarcaModelo> modelos = DataLayer.GetModelos(DataSource, marca);
                    CarregaModelos(modelos);
                });
            }
        }

        // Escolheu um modelo, vai buscar as versões associadas ao modelo.
        private void CbModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                ActualizaStatusRato("A procurar as versões disponiveis...");
                var taskFeetchedVersoes = Task.Run(() =>
                {
                    MarcaModelo modelo = (MarcaModelo)e.AddedItems[0];
                    List<MarcaModeloVersao> versoes = DataLayer.GetVersoes(DataSource, modelo);
                    CarregaVersoes(versoes);
                });
            }
        }

        // Escolheu uma versao, vai o preço da mesma.
        private void CbVersoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                ActualizaStatusRato("A procurar pvp...");
                var taskFeetchedViatura = Task.Run(() =>
                {
                    MarcaModeloVersao versao = (MarcaModeloVersao)e.AddedItems[0];                    
                    Viatura viatura = DataLayer.GetViatura(DataSource, versao);
                    ActualizaPreco(viatura);
                });
            }
        }

        // Quando um dos radiobuttons da datasource e carregado, define a datasource a usar.
        private void Rb_Checked(object sender, RoutedEventArgs e)
        {
            ClearScreen();

            SetDataSource();
            BuscarPreencheMarcas();
        }
        #endregion

        #region Metodos privados
        private void CarregaMarcas(List<Marca> fetchedMarcas)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                cbMarcas.DisplayMemberPath = "DescricaoMarca";
                cbMarcas.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedMarcas });
                ActualizaStatusRato();
            }), fetchedMarcas);
        }

        private void CarregaModelos(List<MarcaModelo> fetchedMarcaModelos)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                cbModelos.DisplayMemberPath = "DescricaoModelo";
                cbModelos.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedMarcaModelos });
                ActualizaStatusRato();
            }), fetchedMarcaModelos);
        }

        private void CarregaVersoes(List<MarcaModeloVersao> fetchedVersoes)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                cbVersoes.DisplayMemberPath = "DescricaoVersao";
                cbVersoes.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedVersoes });
                ActualizaStatusRato();
            }), fetchedVersoes);
        }

        private void ActualizaPreco(Viatura viatura)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                txtPreco.Text = viatura.PrecoNovo.ToString();
                ActualizaStatusRato();
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

        private void ActualizaStatusRato(string mensagemStatus = "")
        {
            // Este evento é disparado no load do form e nessa altura a lblStatus ainda não existe.
            if (lblStatus != null)
            {
                lblStatus.Content = mensagemStatus;
            }
            if (mensagemStatus == "")
            {
                Cursor = Cursors.Arrow;
            }
            else
            {
                Cursor = Cursors.Wait;
            }
        }

        private void BuscarPreencheMarcas()
        {
            ActualizaStatusRato("A procurar as marcas disponiveis...");
            var taskFetchedMarcas = Task.Run(() =>
            {
                List<Marca> marcas = DataLayer.GetMarcas(DataSource);
                CarregaMarcas(marcas);
            });
        }

        private void ClearScreen()
        {
            cbMarcas.ItemsSource = null;
            cbModelos.ItemsSource = null;
            cbVersoes.ItemsSource = null;
            cbMarcas.Items.Clear();
            cbModelos.Items.Clear();
            cbVersoes.Items.Clear();

            txtPreco.Text = "";
            txtDuracao.Text = "";
            txtEntradaInicial.Text = "";
            txtMensalidade.Text = "";
            txtResidual.Text = "";
            txtTaxa.Text = "";
        }
        #endregion
    }
}