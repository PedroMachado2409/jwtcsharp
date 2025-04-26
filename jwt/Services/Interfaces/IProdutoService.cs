using jwt.Data;
using jwt.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
    {
        // Interface para o serviço de produtos
        public interface IProdutoService
        {
            Task<IEnumerable<Produto>> ObterTodosProdutos();
            Task<Produto> ObterProdutoPorId(int id);
            Task<Produto> AdicionarProduto(Produto produto);
            Task<Produto> AtualizarProduto(int id, Produto produto);
            Task<bool> RemoverProduto(int id);
        }

    }
