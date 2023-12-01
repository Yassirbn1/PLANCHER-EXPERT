using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using static DOTNET_DEMO.Pages.nouvelle_demandeModel;

namespace DOTNET_DEMO.Pages
{
    public class FactureModel : PageModel
    {
        public Demande d = new Demande();
        public double superficie;
        public double prixTotal;
        public string nomProduit="";
        public double prixMat;
        public double prixMainOv;
        public double CoutParM2;
        public bool valid = false;



        public void OnGet()
        {
        }

        public void OnPost()
        {
            valid = true;
            // R�cup�rez les valeurs du formulaire
            d.Longueur = Convert.ToDouble(Request.Form["longueur"]);
            d.Largeur = Convert.ToDouble(Request.Form["largeur"]);
            d.TypeCouvrePlancher = Request.Form["nomProduit"];
            d.TVA = 15;
            Produit p = new Produit();
            p = GetCouvrePlancherByName(d.TypeCouvrePlancher);
            d.CoutParM2 = p.materiaux + p.main_oeuvre;
            prixMat = p.materiaux;
            prixMainOv = p.main_oeuvre;
            nomProduit = p.type_couvre_plancher;
            superficie = d.Largeur * d.Longueur;
            prixTotal = superficie * (p.materiaux + p.main_oeuvre) - (superficie * (p.materiaux + p.main_oeuvre)) * 15 / 100;
            // Ins�rer les donn�es dans la base de donn�es
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Utilisez une commande param�tr�e pour �viter les attaques par injection SQL
                    string sql = "INSERT INTO Demande (Longueur, Largeur, TypeCouvrePlancherId, CoutParM2, TVA) VALUES (@Longueur, @Largeur, @TypeCouvrePlancher, @CoutParM2, @TVA)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Longueur", d.Longueur);
                        cmd.Parameters.AddWithValue("@Largeur", d.Largeur);
                        cmd.Parameters.AddWithValue("@TypeCouvrePlancher", d.TypeCouvrePlancher);
                        cmd.Parameters.AddWithValue("@CoutParM2", d.CoutParM2);
                        cmd.Parameters.AddWithValue("@TVA", d.TVA);

                        cmd.ExecuteNonQuery(); // Ex�cutez la commande d'insertion
                    }
                }

                
            }
            catch (Exception ex)
            {
                // G�rez les exceptions
                Console.WriteLine("waaaaaaaaaaaa : " + ex);
               
            }
        }




        public Produit GetCouvrePlancherByName(string name)
        {
            Produit p = new Produit();

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from couvre_plancher where type_couvre_plancher="+"'"+name+"'";     // Requ�te SQL pour les types de couvre-plancher
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Ajout du type de couvre-plancher � la liste
                                p.Id = reader.GetInt32(0);
                                p.type_couvre_plancher = reader.GetString(1);
                                p.materiaux = reader.GetDouble(2);
                                p.main_oeuvre = reader.GetDouble(3);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return p;


        }


        // Mod�le pour repr�senter une demande
        public class Demande
        {
            public double Longueur { get; set; }
            public double Largeur { get; set; }
            public string TypeCouvrePlancher { get; set; } = "";
            public double CoutParM2 { get; set; }
            public double TVA { get; set; }
        }


    }
}
