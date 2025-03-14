﻿using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class UsuarioCadastroModel(string nome, string link, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.UsuarioCadastro, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string Link { get; } = link;
}


