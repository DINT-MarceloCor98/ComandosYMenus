using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace ComandosYMenus
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> lista;
        string itemSeleccionado;
        public MainWindow()
        {
            
            lista = new ObservableCollection<string>();
            InitializeComponent();
            Hora.Text = DateTime.Now.ToLongTimeString();
            itemSeleccionado = "";
            listaElementos.DataContext = lista;
            VaciarButton.Command = MisComandos.Clear;
            PegarButton.Command = ApplicationCommands.Paste;
            CopiarButton.Command = ApplicationCommands.Copy;
            crearTemporizador();
        }

        public void crearTemporizador()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Hora.Text = DateTime.Now.ToLongTimeString();
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lista.Add("Item añadido a las " + DateTime.Now.ToLongTimeString());            
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lista.Count < 10;
        }

        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lista.Clear();
        }

        private void Clear_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lista.Count > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void Salir_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Salir_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            itemSeleccionado = listaElementos.SelectedItem.ToString();            
        }

        private void Copy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = listaElementos.SelectedItem != null;
        }

        private void Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lista.Add(itemSeleccionado);
            itemSeleccionado = "";
        }

        private void Paste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            
            e.CanExecute = itemSeleccionado != "";
        }


    }
}
