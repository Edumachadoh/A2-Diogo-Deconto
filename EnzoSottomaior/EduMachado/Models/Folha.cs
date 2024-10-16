namespace EnzoSottomaior;

public class Folha 
{
     public int Id { get; set; }
    public string? Nome { get; set; }
    public double Valor  { get; set; }
    public int quantidade { get; set; }

    public int mes { get; set; }
    public int ano { get; set; }

    public double SalarioBruto  { get; set; }
    public double ImpostoIrrf  { get; set; }
    public double ImpostoInss  { get; set; }
    public double ImpostoFgts  { get; set; }
    public double SalarioLiquido  { get; set; }
        
    public int FuncionarioId { get; set; }

    public Funcionario? Funcionario { get; set; }
}
