using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Bunzl.Infra.CrossCutting.Extensions;

public static class StringExtensions
{
    public static string EncryptPassword(this string password)
    {
        if (string.IsNullOrEmpty(password))
            return string.Empty;

        password += "c120cd0e-194e-442c-b2e7-9ddf0b0b3ac0";
        var passwordTmp = password;
        var data = MD5.HashData(Encoding.Default.GetBytes(passwordTmp));
        var sbString = new StringBuilder();
        foreach (var t in data)
            sbString.Append(t.ToString("x2"));

        return sbString.ToString();
    }

    public static string MascaraEmail(this string email)
    {
        var emailPartes = email.Split('@');
        if (emailPartes.Length != 2)
            return string.Empty;

        var usuario = emailPartes[0];
        var dominio = emailPartes[1];
        var primeiraLetra = usuario.Substring(0, 1);

        if (usuario.Length <= 2)
            return $"{primeiraLetra}****@{dominio}";

        return $"{primeiraLetra}*****{usuario.Substring(usuario.Length - 1, 1)}@{dominio}";
    }

    public static string? MascaraTelefone(this string telefone)
    {
        var ultimosQuatros = telefone.Length > 4 ? telefone.Substring(telefone.Length - 4) : telefone;
        var telefoneComMascara = new Regex(@"\d").Replace(telefone, "*");

        return $"{telefoneComMascara.Substring(0, telefoneComMascara.Length - 4)}{ultimosQuatros}";
    }

    public static string ApenasNumeros(this string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

        return string.Empty;
    }
}