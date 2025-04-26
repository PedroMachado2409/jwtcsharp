using jwt.Data;
using jwt.Models;
using Microsoft.EntityFrameworkCore;
using Services;

public class ProdutoService : IProdutoService
        {
            private readonly AppDbContext _context;

            public ProdutoService(AppDbContext context)
            {
                _context = context;
            }

            // Obtém todos os produtos do banco de dados
            public async Task<IEnumerable<Produto>> ObterTodosProdutos()
            {
                return await _context.Produtos.ToListAsync();
            }

            // Obtém um produto por Id do banco de dados
            public async Task<Produto> ObterProdutoPorId(int id)
            {
                return await _context.Produtos.FindAsync(id);
            }

            // Adiciona um novo produto ao banco de dados
            public async Task<Produto> AdicionarProduto(Produto produto)
            {
                produto.DataCadastro = DateTime.UtcNow; // Garante que a data de cadastro está correta
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return produto;
            }

            // Atualiza um produto existente no banco de dados
            public async Task<Produto> AtualizarProduto(int id, Produto produto)
            {
                var produtoExistente = await _context.Produtos.FindAsync(id);
                if (produtoExistente == null)
                {
                    return null; // Retorna null se o produto não existir
                }

                // Atualiza as propriedades do produto existente
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Descricao = produto.Descricao;
                produtoExistente.Preco = produto.Preco;
                produtoExistente.QuantidadeEstoque = produto.QuantidadeEstoque;

                _context.Entry(produtoExistente).State = EntityState.Modified; //informa ao EF que a entidade foi modificada
                await _context.SaveChangesAsync();
                return produtoExistente;
            }

            // Remove um produto do banco de dados
            public async Task<bool> RemoverProduto(int id)
            {
                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    return false; // Retorna false se o produto não existir
                }

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return true;
            }
        }