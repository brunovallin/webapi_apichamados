using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto01_ApiChamados.Enum
{
    public enum StatusChamado
    {
        CHAMADO_INEXISTENTE = 0,
        CHAMADO_CADASTRADO,
        CHAMADO_PENDENTE,
        CHAMADO_RESPONDIDO,
        CHAMADO_JA_EXISTENTE,
        CHAMADO_DELETADO
    }
}