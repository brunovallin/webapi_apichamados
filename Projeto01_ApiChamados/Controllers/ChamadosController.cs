using Projeto01_ApiChamados.Dados;
using Projeto01_ApiChamados.Enum;
using Projeto01_ApiChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projeto01_ApiChamados.Controllers
{
    public class ChamadosController : ApiController
    {        
        //HTTP Get - Lista todos os chamados
        public IEnumerable<Chamado> GetChamados()
        {
            return ChamadosDao.GetChamados();
        }

        //HTTP Get - Busca um chamado
        public Chamado GetChamado(int id)
        {
            return ChamadosDao.GetChamado(id);
        }

        public HttpResponseMessage PostChamado(Chamado chamado)
        {
            StatusChamado status = ChamadosDao.InserirChamado(chamado);
            if (status == StatusChamado.CHAMADO_CADASTRADO)
            {
                var response = Request.CreateResponse<Chamado>(HttpStatusCode.OK,chamado);
                string uri = Url.Link("DefaultApi", new {id = chamado.ChamadoId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                string msg;

                switch (status)
                {
                    case StatusChamado.CHAMADO_PENDENTE:
                        msg = "Chamado encontra-se pendente";break;
                    case StatusChamado.CHAMADO_RESPONDIDO:
                        msg = "Este chamado já foi atendido"; break;
                    default:
                        msg = "Ocorreu um erro inesperado";break;
                }
                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = msg
                };
                throw new HttpResponseException(erro);
            }
        }

        public HttpResponseMessage PutChamado(int id, Chamado chamado)
        {
            StatusChamado statusChamado = ChamadosDao.ResponderChamado(id, chamado);
            if (statusChamado == StatusChamado.CHAMADO_RESPONDIDO)
            {
                var response = Request.CreateResponse<Chamado>(HttpStatusCode.OK, chamado);
                string uri = Url.Link("DefaultApi", new { id = chamado.ChamadoId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                string msg;

                switch (statusChamado)
                {
                    case StatusChamado.CHAMADO_INEXISTENTE:
                        msg = "Chamado não existe"; break;
                    case StatusChamado.CHAMADO_PENDENTE:
                        msg = "Chamado encontra-se pendente";break;
                    default:
                        msg = "Ocorreu um erro inesperado";break;
                }

                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = msg
                };
                throw new HttpResponseException(erro);
            }
        }

        //public HttpResponseMessage DeleteChamado(int id)
        //{
        //    StatusChamado status = ChamadosDao.RemoverChamado(id);
        //    if(status == StatusChamado.CHAMADO_DELETADO)
        //    {
        //        var response = Request.CreateResponse(HttpStatusCode.OK);
        //        string uri = Url.Link
        //    }
        //}
    }
}
