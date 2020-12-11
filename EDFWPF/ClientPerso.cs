using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDFWPF
{
    public class ClientPerso
    {
        public int IdClient { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public int AncienReleveClient { get; set; }
        public int DernierReleveClient { get; set; }
        public int ConsomationCient { get; set; }

        public ClientPerso(int unId, string unNom, string unPrenom, int unAncienReleve, int unDernierReleve)
        {
            IdClient = unId;
            NomClient = unNom;
            PrenomClient = unPrenom;
            AncienReleveClient = unAncienReleve;
            DernierReleveClient = unDernierReleve;
            ConsomationCient = DernierReleveClient - AncienReleveClient;
        }
    }
}
