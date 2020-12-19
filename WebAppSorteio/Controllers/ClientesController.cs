using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAppSorteio.Models.Contexto;
using WebAppSorteio.Models.Entidades;

namespace WebAppSorteio.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Contexto _contexto;

        public ClientesController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ViewData["NumeroSorteado"] = cliente.NumeroSorteado;
                ViewData["Nome"] = cliente.Nome;
            }

            return View(cliente);
        }

        [HttpGet]
        public List<Cliente> VerClientes()
        {
            return _contexto.Clientes.ToList();
        }

        [HttpPost]
        public IActionResult PostarCliente(Cliente cliente)
        {
            //Caso o modelo for válido, adiciona o cliente ao banco
            if (ModelState.IsValid)
            {
                cliente.NumeroSorteado = SortearNumero();
                _contexto.Clientes.Add(cliente);
                _contexto.SaveChanges();
                return RedirectToAction("Index", cliente);
            }
            return View();
        }

        public int SortearNumero()
        {
            //Cria uma lista com os números sorteados
            List<int> sorteados = NumerosSorteados();

            //Gera número aleatório
            Random rnd = new Random();
            int numSorteado = rnd.Next(0, 99999);

            //Busca um número ainda não sorteado
            while (sorteados.Contains(numSorteado) == true)
            {
                numSorteado = rnd.Next(0, 99999);
            }

            return numSorteado;
        }

        //Retorna os números sorteados anteriormente
        public List<int> NumerosSorteados()
        {
            List<Cliente> clientes = _contexto.Clientes.ToList();
            List<int> numerosSorteados = new List<int>();

            //Adiciona os números sorteados de todos os clientes na lista de inteiros
            foreach (Cliente cliente in clientes)
            {
                numerosSorteados.Add(cliente.NumeroSorteado);
            }
            return numerosSorteados;
        }

        //Cria o arquivo de texto e faz o download
        public IActionResult DownloadTXT(string NumeroSorteado)
        {
            Cliente cliente = EncontrarCliente(Int32.Parse(NumeroSorteado));
            using (TextWriter File = new StreamWriter(@"C:\Users\ferna\Desktop\NumeroSorteado.txt"))
            {
                File.WriteLine("Obrigado, " + cliente.Nome + "! Por ter solicitado o número " + cliente.NumeroSorteado + ".");
            }
            return RedirectToAction("Index", cliente);
        }

        //Encontra o cliente pelo numero sorteado
        public Cliente EncontrarCliente(int NumeroSorteado)
        {
            List<Cliente> clientes = VerClientes();

            Cliente cliente = new Cliente();

            //Adiciona todos os números sorteados a lista
            foreach (Cliente c in clientes)
            {
                if (c.NumeroSorteado == NumeroSorteado)
                {
                    cliente = c;
                }
            }
            return cliente;
        }
    }
}