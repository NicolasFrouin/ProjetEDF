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
using System.Windows.Shapes;

namespace EDFWPF
{
    /// <summary>
    /// Logique d'interaction pour FrmAdmin.xaml
    /// </summary>
    public partial class FrmAdmin : Window
    {
        edfEntities gst;
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
            lstControleurs.ItemsSource = gst.controleur.ToList();
        }

        private void lstControleurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstClients.ItemsSource = from c in gst.client.ToList()
                                     where c.idcontroleur == (lstControleurs.SelectedItem as controleur).id
                                     select new ClientPerso(c.identifiant, c.nom, c.prenom, c.ancienReleve, c.dernierReleve);
        }

        private void btnInsertionControleur_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomControleur.Text == "")
            {
                MessageBox.Show("Veuillez saisir le nom du contrôleur");
            }
            else if (txtPrenomControleur.Text == "")
            {
                MessageBox.Show("Veuillez saisir le prénom du contrôleur");
            }
            else
            {
                var monLogin = txtPrenomControleur.Text.Substring(0,1).ToLower() + txtNomControleur.Text.Substring(0,1).ToLower();
                gst.controleur.Add(new controleur { id = gst.controleur.ToList().Last().id + 1, nom = txtNomControleur.Text, prenom = txtPrenomControleur.Text, statut = "ctrl", login = monLogin, mdp = monLogin + "123" });
                gst.SaveChanges();
                txtNomControleur.Text = "";
                txtPrenomControleur.Text = "";
                lstControleurs.ItemsSource = gst.controleur.ToList();
            }
        }

        private void btnInsertionClient_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomClient.Text == "")
            {
                MessageBox.Show("Veuillez saisir le nom du client");
            }
            else if (txtPrenomClient.Text == "")
            {
                MessageBox.Show("Veuillez saisir le prénom du client");
            }
            else if (lstControleurs.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un contrôleur");
            }
            else
            {
                gst.client.Add(new client { identifiant = gst.client.ToList().Last().identifiant + 1, nom = txtNomClient.Text, prenom = txtPrenomClient.Text, ancienReleve = 0, dernierReleve = 0, idcontroleur = (lstControleurs.SelectedItem as controleur).id });
                gst.SaveChanges();
                txtNomClient.Text = "";
                txtPrenomClient.Text = "";
                lstClients.ItemsSource = from c in gst.client.ToList()
                                         where c.idcontroleur == (lstControleurs.SelectedItem as controleur).id
                                         select new ClientPerso(c.identifiant, c.nom, c.prenom, c.ancienReleve, c.dernierReleve);
            }
        }
    }
}
