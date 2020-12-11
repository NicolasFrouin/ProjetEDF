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
    /// Logique d'interaction pour FrmConnexion.xaml
    /// </summary>
    public partial class FrmConnexion : Window
    {
        edfEntities gst;
        public FrmConnexion()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "")
            {
                txtErreur.Text = "Veuillez saisir le login";
                MessageBox.Show("Veuillez saisir le login");
            }
            else if (txtMotDePasse.Password == "")
            {
                txtErreur.Text = "Veuillez saisir le mot de passe";
                MessageBox.Show("Veuillez saisir le mot de passe");
            }
            else
            {
                var leControleur = gst.controleur.ToList().Find(c => c.login == txtLogin.Text && c.mdp == txtMotDePasse.Password);
                if (leControleur == null)
                {
                    txtErreur.Text = "Vos identifiants sont incorrects";
                    MessageBox.Show("Le contrôleur n'existe pas");
                }
                else if (leControleur.statut == "admin")
                {
                    FrmAdmin frm = new FrmAdmin();
                    frm.Show();
                }
                else
                {
                    FrmControleur frm = new FrmControleur(leControleur);
                    frm.Show();
                }
            }
        }
    }
}
