using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DOTNET_DEMO.Pages
{
    public class ContactModel : PageModel
    {
        private string a = "";
        public bool hasContact = false; //pour v�rifier si un message � �t� envoy�e
        public string name = ""; //les donn�es de l'utilisateur
        public string email = "";
        public string msg = "";

        public string A
        {
            get { return a; }
            set { a = value; }
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            hasContact = true;
            name = Request.Form["name"];
            email = Request.Form["email"];
            msg = Request.Form["msg"];

            Console.WriteLine(name);
            Console.WriteLine(email);
            Console.WriteLine(msg);

        }
    }
}
