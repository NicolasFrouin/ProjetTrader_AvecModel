using System;
using System.Collections.Generic;
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

namespace TraderWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bourseEntities gst;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new bourseEntities();
            lstTraders.ItemsSource = gst.trader.ToList();
        }

        private void lstTraders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<acheter> lesAchats = gst.acheter.ToList().FindAll(a => a.numTrader == (lstTraders.SelectedItem as trader).idTrader);
            lstActions.ItemsSource = lesAchats;
            txtTotalPortefeuille.Text = lesAchats.Sum(a => a.prixAchat * a.quantite).ToString();


            IEnumerable<action> actionsNonPossedees = new List<action>();
            List<action> toutesLesActions = new List<action>();
            toutesLesActions = gst.action.ToList();



            List<action> actionsTrader = new List<action>();

            //actionsTrader = gst.action.ToList().FindAll(a => (a.acheter as acheter).trader.idTrader == (lstTraders.SelectedItem as trader).idTrader);




            //foreach (action a in toutesLesActions)
            //{
            //    foreach (acheter acheter in lstActions.Items)
            //    {
            //        if (a.idAction == acheter.numAction)
            //        {
            //            break;
            //        }
            //        if (!actionsNonPossedees.Contains(a))
            //        {
            //            actionsNonPossedees.Add(a);
            //        }
            //    }
            //}
            //lstActionsNonPossedees.ItemsSource = actionsNonPossedees;

            lstActionsNonPossedees.ItemsSource = toutesLesActions.SkipWhile(a => lesAchats.FindAll(ac => ac.numAction == a.idAction).Contains(a.acheter as acheter)); 
            //lstActionsNonPossedees.ItemsSource = toutesLesActions.FindAll(a => );
        }

        private void lstActions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            imgAction.Source = new BitmapImage(new Uri("", UriKind.RelativeOrAbsolute));
            if (lstActions.SelectedItem != null)
            {
                if ((lstActions.SelectedItem as acheter).prixAchat > gst.action.ToList().Find(a => a.idAction == (lstActions.SelectedItem as acheter).numAction).coursReel)
                {
                    imgAction.Source = new BitmapImage(new Uri("/Images/Bas.png", UriKind.RelativeOrAbsolute));
                }
                else if ((lstActions.SelectedItem as acheter).prixAchat == gst.action.ToList().Find(a => a.idAction == (lstActions.SelectedItem as acheter).numAction).coursReel)
                {
                    imgAction.Source = new BitmapImage(new Uri("/Images/Moyen.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    imgAction.Source = new BitmapImage(new Uri("/Images/Stonks.jpg", UriKind.RelativeOrAbsolute));
                }
            }
            //imgAction.Source = new BitmapImage(new Uri("/Images/Haut.png", UriKind.RelativeOrAbsolute));
        }

        private void btnVendre_Click(object sender, RoutedEventArgs e)
        {
            if (lstTraders.SelectedItem == null)
            {
                MessageBox.Show("Un trader !");
            }
            else if (lstActions.SelectedItem == null)
            {
                MessageBox.Show("Une action !");
            }
            else if (txtQuantiteVendue.Text == "")
            {
                MessageBox.Show("Une quantité !");
            }
            else if (Convert.ToInt16(txtQuantiteVendue.Text) > (lstActions.SelectedItem as acheter).quantite) // on va mettre que des chiffres
            {
                MessageBox.Show("Trop !");
            }
            else
            {
                if (Convert.ToInt16(txtQuantiteVendue.Text) < (lstActions.SelectedItem as acheter).quantite)
                {
                    gst.acheter.Find((lstActions.SelectedItem as acheter).numAction); // pas fini
                }
            }
        }

        private void btnAcheter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
