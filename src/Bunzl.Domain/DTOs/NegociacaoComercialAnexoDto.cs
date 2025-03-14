using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.DTOs;

public class NegociacaoComercialAnexoDto
{
	public IFormFile Arquivo { get; set; }
	public string Nome { get; set; } 
    public string Tipo { get; set; }
	public string? Observacao { get; set; }
}