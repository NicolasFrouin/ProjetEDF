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
    /// Logique d'interaction pour FrmControleur.xaml
    /// </summary>
    public partial class FrmControleur : Window
    {
        controleur monControleur;
        edfEntities gst;
        public FrmControleur(controleur leControleur)
        {
            InitializeComponent();
            monControleur = leControleur;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
            lstClients.ItemsSource = from c in gst.client.ToList()
                                     where c.idcontroleur == monControleur.id
                                     select new ClientPerso(c.identifiant, c.nom, c.prenom, c.ancienReleve, c.dernierReleve);
        }

        private void btnInsertionReleve_Click(object sender, RoutedEventArgs e)
        {
            int releve;
            if (lstClients.SelectedItem == null)
            {
                MessageBox.Show("Veuillez saisir un client");
            }
            else if (txtNouveauReleve.Text == "")
            {
                MessageBox.Show("Veuillez saisir un nouveau relevé");
            }

            try
            {
                releve = Convert.ToInt16(txtNouveauReleve.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Que des chiffres merci");
                return;
            }

            if (Convert.ToInt16(txtNouveauReleve.Text) < (lstClients.SelectedItem as ClientPerso).DernierReleveClient)
            {
                MessageBox.Show("Impossible de consommer moins que le dernier relevé");
            }
            else
            {
                var monClient = gst.client.ToList().Find(c => c.identifiant == (lstClients.SelectedItem as ClientPerso).IdClient);
                monClient.ancienReleve = monClient.dernierReleve;
                monClient.dernierReleve = Convert.ToInt16(txtNouveauReleve.Text);
                gst.SaveChanges();
                txtNouveauReleve.Text = "";
                lstClients.ItemsSource = from c in gst.client.ToList()
                                         where c.idcontroleur == monControleur.id
                                         select new ClientPerso(c.identifiant, c.nom, c.prenom, c.ancienReleve, c.dernierReleve);
            }
        }
    }
}
