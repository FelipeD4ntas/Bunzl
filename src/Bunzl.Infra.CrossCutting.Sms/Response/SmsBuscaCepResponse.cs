using System.Xml.Serialization;

namespace Bunzl.Infra.CrossCutting.Sms.Response;

[XmlRoot("buscarcep")]
public class SmsBuscaCepResponse
{
    [XmlElement("quantidade")]
    public int Quantidade { get; set; }

    [XmlElement("id_envio")]
    public int IdEnvio { get; set; }

    [XmlElement("resultado")]
    public int Resultado { get; set; }

    [XmlElement("resultado_txt")]
    public string? ResultadoTxt { get; set; }
}
