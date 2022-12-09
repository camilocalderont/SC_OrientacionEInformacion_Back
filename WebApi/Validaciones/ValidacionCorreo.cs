using System.Text.RegularExpressions;

namespace WebApi.Validaciones
{
    public class ValidacionCorreo
    {
        public bool ValidarEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+[^@\s]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

    }
}
