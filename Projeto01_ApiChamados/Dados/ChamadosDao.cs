using Projeto01_ApiChamados.Enum;
using Projeto01_ApiChamados.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto01_ApiChamados.Dados
{
    public class ChamadosDao
    {
        public static IEnumerable<Chamado> GetChamados()
        {
            using (var ctx = new ChamadosEntities())
            {
                return ctx.TBChamados.ToList();
            }
        }

        public static Chamado GetChamado(int id)
        {
            using (var ctx = new ChamadosEntities())
            {
                return ctx.TBChamados.FirstOrDefault(c => c.ChamadoId == id);
            }
        }

        public static StatusChamado InserirChamado(Chamado chamado)
        {
            using (var ctx = new ChamadosEntities())
            {
                if(chamado != null)
                {
                    chamado.Status = (int)StatusChamado.CHAMADO_PENDENTE;
                    ctx.TBChamados.Add(chamado);                                        
                }
                ctx.SaveChanges();
                return StatusChamado.CHAMADO_CADASTRADO;
            }
        }

        public static StatusChamado ResponderChamado(int id, Chamado chamado)
        {
            Chamado chamadodb = GetChamado(id);
            if (chamadodb == null)
            {
                return StatusChamado.CHAMADO_INEXISTENTE;
            }
            using (var ctx = new ChamadosEntities())
            {
                chamadodb.Resposta = chamado.Resposta;
                chamadodb.Status = (int)StatusChamado.CHAMADO_RESPONDIDO;
                ctx.Entry<Chamado>(chamadodb).State = EntityState.Modified;
                ctx.SaveChanges();
                return StatusChamado.CHAMADO_RESPONDIDO;
            }
        }

        public static StatusChamado RemoverChamado(int id)
        {
            Chamado chamado = GetChamado(id);
            if(chamado == null)
            {
                return StatusChamado.CHAMADO_INEXISTENTE;
            }
            using (var ctx = new ChamadosEntities())
            {
                ctx.Entry<Chamado>(chamado).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
            return StatusChamado.CHAMADO_DELETADO;
        }
    }
}