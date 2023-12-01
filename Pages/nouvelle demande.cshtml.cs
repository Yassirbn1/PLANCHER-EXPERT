using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using static DOTNET_DEMO.Pages.FactureModel;
using static DOTNET_DEMO.Pages.Gestion_ProduitModel;


namespace DOTNET_DEMO.Pages
{
    public class Produit
    {
        public int Id { get; set; }
        public string type_couvre_plancher { get; set; } = "";
        public double materiaux { get; set; }
        public double main_oeuvre { get; set; }
    }
    public class nouvelle_demandeModel : PageModel
    {
        public List<Produit>
    GetCouvrePlancher()
        {
            List<Produit>
                typesCouvrePlancher = new List<Produit>
                    ();

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from couvre_plancher; ";     // Requête SQL pour les types de couvre-plancher
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Ajout du type de couvre-plancher à la liste
                                Produit p = new Produit();

                                p.Id = reader.GetInt32(0);
                                p.type_couvre_plancher = reader.GetString(1);
                                p.materiaux = reader.GetDouble(2);
                                p.main_oeuvre = reader.GetDouble(3);

                                typesCouvrePlancher.Add(p);


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return typesCouvrePlancher;
        }


        public List<Produit>
            typesCouvrePlancher = new List<Produit>
                ();


        public void OnGet()
        {
            typesCouvrePlancher = GetCouvrePlancher();

        }
        public double Longueur { get; set; }
        public double Largeur { get; set; }

    }
}
