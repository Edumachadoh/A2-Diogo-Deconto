using Microsoft.AspNetCore.Mvc;
using EnzoSottomaior;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();


app.MapGet("/", () => "API de Produtos");

//POST: /api/funcionario/cadastrar
app.MapPost("/api/funcionario/cadastrar", ([FromBody] Funcionario funcionario, 
[FromServices] AppDataContext ctx) =>
{
    ctx.Funcionarios.Add(funcionario);
    ctx.SaveChanges();
    return Results.Created("", funcionario);
});

//GET: /api/funcionario/listar
app.MapGet("/api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
{

    if (ctx.Funcionarios.Any())
    {
        return Results.Ok(ctx.Funcionarios.ToList());
    }
    return Results.NotFound();
});


//POST: /api/folha/cadastrar/
app.MapPost("/api/folha/cadastrar/", ([FromBody] Folha folha,
    [FromServices] AppDataContext ctx) =>
{
   folha.SalarioBruto = folha.Valor * folha.quantidade;
   if (folha.SalarioBruto <= 1903.98) {
        folha.ImpostoIrrf = 0;
   }
   else if (folha.SalarioBruto >= 1903.99 && folha.SalarioBruto <= 2826.65) {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.075) - 142.80; 
   }
   else if (folha.SalarioBruto >= 2826.66 && folha.SalarioBruto <= 3751.05) {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.15) - 354.80; 
   }
   else if (folha.SalarioBruto >= 3751.06 && folha.SalarioBruto <= 4664.68) {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.225) - 636.13; 
   }
   else if (folha.SalarioBruto >= 4664.68) {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.275) - 869.36; 
   }

    if(folha.SalarioBruto <= 1693.72){
        folha.ImpostoInss = folha.SalarioBruto * 0.08;
    }
    else if(folha.SalarioBruto >= 1693.73 && folha.SalarioBruto <= 2822.90){
        folha.ImpostoInss = folha.SalarioBruto * 0.09;
    }
    else if(folha.SalarioBruto >= 2822.91 && folha.SalarioBruto <= 5645.80){
        folha.ImpostoInss = folha.SalarioBruto * 0.11;
    }
    else if(folha.SalarioBruto >= 5645.81){
        folha.ImpostoInss = 621.03;
    }

    folha.ImpostoFgts = folha.SalarioBruto * 0.08;

    folha.SalarioLiquido = folha.SalarioBruto - folha.ImpostoInss - folha.ImpostoIrrf;

    Funcionario? funcionario = ctx.Funcionarios.Find(folha.FuncionarioId);
    
    if (funcionario == null) {
       return Results.NotFound();
    }

    folha.Funcionario = funcionario;

    ctx.Folhas.Add(folha);
    ctx.SaveChanges();
    return Results.Created("", folha);
});

//GET: /api/folha/listar
app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
{

    if (ctx.Folhas.Any())
    {
        return Results.Ok(ctx.Folhas.ToList());
    }
    return Results.NotFound();
});

/*
//POST: /api/produto/cadastrar/param_nome
app.MapPost("/api/produto/cadastrar", ([FromBody] Produto produto,
    [FromServices] AppDataContext ctx) =>
{
    ctx.Produtos.Add(produto);
    ctx.SaveChanges();
    return Results.Created("", produto);
});

//DELETE: /api/produto/deletar/{id}
app.MapDelete("/api/produto/deletar/{id}", ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto == null)
    {
        return Results.NotFound();
    }
    ctx.Produtos.Remove(produto);
    ctx.SaveChanges();
    return Results.Ok(produto);
});
*/

app.Run();