using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Enum;
using PayWiseBackend.Domain.Models;
using PayWiseBackend.Infra.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PayWiseBackend.Controllers
{
    [Authorize]
    [Route("/contas")]
    [ApiController]
    public class PixController : ControllerBase
    {
        private readonly PaywiseDbContextSqlite _context;
        private readonly IAuthService _authService;
        private readonly IContaService _contaService;

        public PixController(
            PaywiseDbContextSqlite context,
            IAuthService authService,
            IContaService contaService)
        {
            _context = context;
            _authService = authService;
            _contaService = contaService;
        }

        public object EnumTipoTransacao { get; private set; }
        public object EnumTipoOperacao { get; private set; }

        [HttpPost("pix")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MessageResponse> Pix([FromBody] CreateTransacaoPixDTO requisicao)
        {
            var id = long.Parse(User.Claims.First(x => x.Type == "Id").Value);
            var contaOrigem = _context.Contas.Include(c => c.Cliente)
                .First(c => c.Id == id);
            var contaDestino = _context.Contas.Include(c => c.Cliente)
                .FirstOrDefault(c => c.NumeroConta == requisicao.NumeroContaDestino);

            if (contaDestino == null)
            {
                ModelState.AddModelError("Conta de Destino", "A conta de destino fornecida não existe");
                return BadRequest(ModelState);
            }

            if (contaOrigem == contaDestino)
            {
                ModelState.AddModelError("Contas Correntes", "As contas de origem e destino não podem ser iguais");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if (contaOrigem.Saldo < requisicao.Valor)
            {
                ModelState.AddModelError("Saldo insuficiente", "O saldo disponível na conta de origem é inferior ao valor a ser transferido.");
                return ValidationProblem(ModelState);
            }

            var transacaoContaOrigem = new Transacao
            {
                Descricao = requisicao.Descricao,
                Historico = $"Transferência PIX enviada para {contaDestino.Cliente.Nome}",
                Valor = requisicao.Valor,
                TipoTransacao = EnumTipoTransacao.PIX,
                TipoOperacao = EnumTipoOperacao.Debito,
                ContaId = contaOrigem.Id
            };

            var transacaoContaDestino = new Transacao
            {
                Historico = $"Transferência PIX recebida de {contaOrigem.Cliente.Nome}",
                Valor = requisicao.Valor,
                TipoTransacao = EnumTipoTransacao.PIX,
                TipoOperacao = EnumTipoOperacao.Credito,
                ContaId = contaDestino.Id
            };

            contaOrigem.Saldo -= requisicao.Valor;
            _context.Transacoes.Add(transacaoContaOrigem);

            contaDestino.Saldo += requisicao.Valor;
            _context.Transacoes.Add(transacaoContaDestino);

            _context.SaveChanges();

            return Ok(new MessageResponse("Operação de Transferência PIX concluída com sucesso"));
        }
    }

    public class MessageResponse
    {
    }
}
