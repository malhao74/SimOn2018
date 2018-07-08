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
using System.Globalization;

[assembly: CLSCompliant(true)]
namespace SimOn
{
    public partial class MainWindow : Window
    {
        #region Fields & properties
        // File necessary to update the UI within a asynchronous task
        private readonly SynchronizationContext synchronizationContext;
        private DataSource DataSource { get; set; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
        }

        #region Events
        private void ButCalcular_Click(object sender, RoutedEventArgs e)
        {
            double vehiclePrice = txtPreco.ToDouble();
            double downPayment = txtEntradaInicial.ToDouble();
            int numberMonths = Convert.ToInt32(this.txtDuracao.Text, CultureInfo.CurrentCulture);
            double rate = Convert.ToDouble(this.txtTaxa.Text, CultureInfo.CurrentCulture);
            double residual = txtResidual.ToDouble();

            FinancialCalculation calculation = new FinancialCalculation(FinancialProduct.Credit,
                                                                vehiclePrice,
                                                                downPayment,
                                                                residual,
                                                                numberMonths,
                                                                rate);
            txtMensalidade.FromDouble(calculation.MonthlyPayment);
        }

        /// <summary>
        /// When a user chooses a brand, it fetches the models associated with that specific brand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                UpdateStatus("A procurar os modelos disponiveis...");
                
                Task.Run(() =>
                {
                    Brand marca = (Brand)e.AddedItems[0];
                    List<Model> modelos = DataLayer.FetchModels(DataSource, marca);
                    UpdateCbModels(modelos);
                });
            }
        }

        /// <summary>
        /// If the model changes, it updates the versions available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                UpdateStatus("A procurar as versões disponiveis...");

                Task.Run(() =>
                {
                    Model modelo = (Model)e.AddedItems[0];
                    List<Version> versoes = DataLayer.FetchVersions(DataSource, modelo);
                    UpdateCbVersions(versoes);
                });
            }
        }

        /// <summary>
        /// When a new version is chosen, it updates the price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbVersoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                UpdateStatus("A procurar pvp...");

                Task.Run(() =>
                {
                    Version versao = (Version)e.AddedItems[0];                    
                    Car viatura = DataLayer.FetchCar(DataSource, versao);
                    UpdatePrice(viatura);
                });
            }
        }

        /// <summary>
        /// Clears the information on the form, defines the data source based on the radio button pressed and load the brand available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rb_Checked(object sender, RoutedEventArgs e)
        {
            ClearScreen();

            SetDataSource();
            FetchUpdateBrands();
        }
        #endregion

        #region Private methods
        private void UpdateCbBrands(List<Brand> fechedBrands)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                cbMarcas.DisplayMemberPath = "BrandDescription";
                cbMarcas.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fechedBrands });
                UpdateStatus();
            }), fechedBrands);
        }

        private void UpdateCbModels(List<Model> fetchedModels)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                cbModelos.DisplayMemberPath = "ModelDescription";
                cbModelos.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedModels });
                UpdateStatus();
            }), fetchedModels);
        }

        private void UpdateCbVersions(List<Version> fetchedVersions)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                cbVersoes.DisplayMemberPath = "VersionDescription";
                cbVersoes.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = fetchedVersions });
                UpdateStatus();
            }), fetchedVersions);
        }

        private void UpdatePrice(Car car)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                txtPreco.FromDouble(car.Price);
                UpdateStatus();
            }), car);
        }

        /// <summary>
        /// Defines the data source based on the radio button pressed
        /// </summary>
        private void SetDataSource()
        {
            if (rbExcel.IsChecked == true)
            {
                DataSource = DataSource.Excel;
            }
            else if (rbXml.IsChecked == true)
            {
                DataSource = DataSource.Xml;
            }
            else
            {
                DataSource = DataSource.FireBase;
            }
        }

        private void UpdateStatus(string statusMessage = "")
        {
            if (lblStatus != null)
            {
                lblStatus.Content = statusMessage;
            }
            if (String.IsNullOrEmpty(statusMessage))
            {
                Cursor = Cursors.Arrow;
                IsEnabled = true;
            }
            else
            {
                Cursor = Cursors.Wait;
                IsEnabled = false;
            }
        }

        private void FetchUpdateBrands()
        {
            UpdateStatus("A procurar as marcas disponiveis...");
            Task.Run(() =>
            {
                List<Brand> marcas = DataLayer.FetchBrands(DataSource);
                UpdateCbBrands(marcas);
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