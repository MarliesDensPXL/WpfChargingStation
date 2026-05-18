using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChargingStation.Models;

namespace ChargingStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Customer> _customers;
        List<LicensePlate> _licensePlates;
        Random rng = new Random();
        
        public MainWindow()
        {
            InitializeComponent();

            LoadData();

            foreach (Customer customer in _customers)
            {
                customerComboBox.Items.Add(customer);
            }            

         }

        private void LoadData()
        {
            _customers = new List<Customer>();
            _licensePlates = new List<LicensePlate>();
            Customer customer;
            LicensePlate plate;

            customer = new Customer() { Id = 1, Name = "John Doe" };
            _customers.Add(customer);
            plate = new LicensePlate() { Customer = customer, Plate = "2-ABC-123", Mileage = 12514 };
            _licensePlates.Add(plate);

            customer = new Customer() { Id = 2, Name = "Marcha Uber" };
            _customers.Add(customer);
            plate = new LicensePlate() { Customer = customer, Plate = "1-SFR-854", Mileage = 64258 };
            _licensePlates.Add(plate);

            customer = new Customer() { Id = 3, Name = "Stefanie Rovers" };
            _customers.Add(customer);
            plate = new LicensePlate() { Customer = customer, Plate = "2-HTB-487", Mileage = 458 };
            _licensePlates.Add(plate);
            plate = new LicensePlate() { Customer = customer, Plate = "ROVERS", Mileage = 43125 };
            _licensePlates.Add(plate);
            plate = new LicensePlate() { Customer = customer, Plate = "911-TURBO", Mileage = 8468 };
            _licensePlates.Add(plate);

            customer = new Customer() { Id = 4, Name = "Alex DeWitt" };
            _customers.Add(customer);
            plate = new LicensePlate() { Customer = customer, Plate = "2-KOZ-527", Mileage = 125658 };
            _licensePlates.Add(plate);
        }

        public void FillLicensePlateListBox()
        {
            Customer selectedCustomer = (Customer)customerComboBox.SelectedItem;

            if (selectedCustomer == null)
            {
                return;
            }

            licensePlateListBox.Items.Clear();
            foreach (LicensePlate plate in _licensePlates) //de hele lijst met nummerplaten doorlopen. Als er een plaat gekoppeld is aan de geselecteerde customer, plaat toevoegen.
            {
                if (plate.Customer == selectedCustomer)
                {
                    licensePlateListBox.Items.Add(plate);
                }    
            }
        }

        public void OnCustomerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillLicensePlateListBox();
        }

        public void EnableStartbutton()
        {
            LicensePlate selectedPlate = (LicensePlate)licensePlateListBox.SelectedItem;
            int.TryParse(mileageTextBox.Text, out int newMileage);

            if (newMileage > selectedPlate.Mileage && ((defaultRadioButton.IsChecked == true) || (fastRadioButton.IsChecked == true) || (superRadioButton.IsChecked == true)))
            {

                startButton.IsEnabled = true;
            }
        }

        private void OnMileageTextChanged(object sender, TextChangedEventArgs e)
        {
            EnableStartbutton();
        }

        private void OnDefaultRadioButtonIsChecked(object sender, RoutedEventArgs e)
        {
            EnableStartbutton();
        }

        private void OnFastRadioButtonIsChecked(object sender, RoutedEventArgs e)
        {
            EnableStartbutton();
        }

        private void OnSuperRadioButtonIsChecked(object sender, RoutedEventArgs e)
        {
            EnableStartbutton();
        }

        private void OnLicensePlateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LicensePlate selectedPlate = (LicensePlate)licensePlateListBox.SelectedItem;

            if (selectedPlate == null)
            {
                return;
            }

            chargeSessionsTextBlock.Text= selectedPlate.ShowChargeSessions();

            mileageTextBox.Clear();
            
        }

        private void OnStartButtonClicked(object sender, RoutedEventArgs e)
        {
            LicensePlate selectedPlate = (LicensePlate)licensePlateListBox.SelectedItem;

            int.TryParse(mileageTextBox.Text, out int result);
            result = selectedPlate.Mileage;

            int selectedIndex = licensePlateListBox.SelectedIndex;

            FillLicensePlateListBox();

            licensePlateListBox.SelectedIndex = selectedIndex;

            startButton.IsEnabled = false;
            endButton.IsEnabled = true;
            chargingImage.Visibility = Visibility.Visible;
            customerComboBox.IsEnabled = false;
            licensePlateListBox.IsEnabled = false;
            mileageTextBox.IsEnabled = false;
        }

        private void OnEndButtonClicked(object sender, RoutedEventArgs e)
        {
            LicensePlate selectedPlate = (LicensePlate)licensePlateListBox.SelectedItem;

            endButton.IsEnabled = false;
            chargingImage.Visibility = Visibility.Hidden;
            int powerConsumed = rng.Next(40, 71);
            float totalPrice = PriceChargingSession(powerConsumed);

            ChargeSession chargeSession = new ChargeSession(powerConsumed, DateTime.Now, totalPrice);

            selectedPlate.ChargeSessions.Add(chargeSession);
            chargeSessionsTextBlock.Text = selectedPlate.ShowChargeSessions();

            customerComboBox.IsEnabled = true;
            licensePlateListBox.IsEnabled = true;
            mileageTextBox.IsEnabled = true;

        }

        private float PriceChargingSession(int powerConsumed)
        {
            float totalPrice =0;
            
            if (defaultRadioButton.IsChecked == true)
            {
                 totalPrice = (powerConsumed * 0.4f);
            }
            else if (fastRadioButton.IsChecked == true)
            {
                totalPrice = (powerConsumed * 0.6f);
            }
            else if (superRadioButton.IsChecked == true)
            {
                totalPrice = (powerConsumed * 0.9f);
            }

            return totalPrice;
        
        }
    }

}