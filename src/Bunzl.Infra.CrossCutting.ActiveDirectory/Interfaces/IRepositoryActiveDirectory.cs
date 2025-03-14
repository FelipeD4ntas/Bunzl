namespace Bunzl.Infra.CrossCutting.ActiveDirectory.Interfaces;

public interface IRepositoryActiveDirectory
{
    bool EmailExists(string email);
    bool ValidarCredencial(string email, string senha);
}