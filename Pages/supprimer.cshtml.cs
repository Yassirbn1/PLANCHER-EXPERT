using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static DOTNET_DEMO.Pages.Pages_nouvelle_demande;

namespace DOTNET_DEMO.Pages
{
    public class supprimerModel : PageModel
    {
        public string messageErreur = "";
        public Produit p1 = new Produit();
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "DELETE FROM CouvrePlancher WHERE Id=@Idp1;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Idp1", id);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Bien supprim�");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            Response.Redirect("/Gestion Produit");
        }

    }
}

