namespace APICatalogoNew.Services;

public interface IMeuServico
{
    string Saudacao(string nome);
}

public class MeuServico : IMeuServico
{
    public string Saudacao(string nome)
    {
        return $"Saudações {nome} \n\n {DateTime.Now:f}";
    }
}
