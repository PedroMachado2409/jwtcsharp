using DTO;
using jwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
    {
        // Controller para a entidade Produto
        [ApiController]
        [Route("api/produtos")] // Define a rota base para este controller
        [Authorize] // Adiciona autenticação a todos os endpoints deste controller
        public class ProdutoController : ControllerBase
        {
            private readonly IProdutoService _produtoService;
            private readonly ILogger<ProdutoController> _logger;

            public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
            {
                _produtoService = produtoService;
                _logger = logger;
            }

            // Endpoint para obter todos os produtos
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)] //Documenta o codigo de retorno 200
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<IEnumerable<ProdutoResponseDTO>>> ObterTodosProdutos()
            {
                try
                {
                    var produtos = await _produtoService.ObterTodosProdutos();
                    // Converte a lista de Produto para uma lista de ProdutoDTO
                    var produtosDTO = produtos.Select(p => new ProdutoResponseDTO
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Descricao = p.Descricao,
                        Preco = p.Preco,
                        QuantidadeEstoque = p.QuantidadeEstoque,
                        DataCadastro = p.DataCadastro
                    });
                    return Ok(produtosDTO);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao obter todos os produtos.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                }
            }

            // Endpoint para obter um produto por Id
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<ProdutoResponseDTO>> ObterProdutoPorId(int id)
            {
                try
                {
                    var produto = await _produtoService.ObterProdutoPorId(id);
                    if (produto == null)
                    {
                        return NotFound(new { message = "Produto não encontrado." }); //retorna objeto json
                    }
                    // Converte o Produto para ProdutoDTO
                    var produtoDTO = new ProdutoResponseDTO
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        Preco = produto.Preco,
                        QuantidadeEstoque = produto.QuantidadeEstoque,
                        DataCadastro = produto.DataCadastro
                    };
                    return Ok(produtoDTO);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao obter o produto com ID {Id}.", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                }
            }

            // Endpoint para adicionar um novo produto
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)] //Retorna 201 e o produto criado
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<ProdutoResponseDTO>> AdicionarProduto([FromBody] CriarProdutoDTO criarProdutoDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Retorna os erros de validação
                }

                try
                {
                    // Converte o CriarProdutoDTO para a entidade Produto
                    var produto = new Produto
                    {
                        Nome = criarProdutoDTO.Nome,
                        Descricao = criarProdutoDTO.Descricao,
                        Preco = criarProdutoDTO.Preco,
                        QuantidadeEstoque = criarProdutoDTO.QuantidadeEstoque
                    };

                    var novoProduto = await _produtoService.AdicionarProduto(produto);
                    // Converte o Produto para ProdutoDTO
                    var novoProdutoDTO = new ProdutoResponseDTO
                    {
                        Id = novoProduto.Id,
                        Nome = novoProduto.Nome,
                        Descricao = novoProduto.Descricao,
                        Preco = novoProduto.Preco,
                        QuantidadeEstoque = novoProduto.QuantidadeEstoque,
                        DataCadastro = novoProduto.DataCadastro
                    };
                    //return CreatedAtAction(nameof(ObterProdutoPorId), new { id = novoProduto.Id }, novoProduto); //retorna o produto criado
                    return StatusCode(StatusCodes.Status201Created, novoProdutoDTO); //retorna 201
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao adicionar um novo produto.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                }
            }

            // Endpoint para atualizar um produto existente
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> AtualizarProduto(int id, [FromBody] AtualizarProdutoDTO atualizarProdutoDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != atualizarProdutoDTO.Id)
                {
                    return BadRequest(new { message = "O ID do produto na rota deve corresponder ao ID no corpo da requisição." });
                }

                try
                {
                    // Converte o AtualizarProdutoDTO para a entidade Produto
                    var produto = new Produto
                    {
                        Id = atualizarProdutoDTO.Id,
                        Nome = atualizarProdutoDTO.Nome,
                        Descricao = atualizarProdutoDTO.Descricao,
                        Preco = atualizarProdutoDTO.Preco,
                        QuantidadeEstoque = atualizarProdutoDTO.QuantidadeEstoque
                    };
                    var produtoAtualizado = await _produtoService.AtualizarProduto(id, produto);
                    if (produtoAtualizado == null)
                    {
                        return NotFound(new { message = "Produto não encontrado." });
                    }
                    // Converte o Produto para ProdutoDTO
                    var produtoAtualizadoDTO = new ProdutoResponseDTO
                    {
                        Id = produtoAtualizado.Id,
                        Nome = produtoAtualizado.Nome,
                        Descricao = produtoAtualizado.Descricao,
                        Preco = produtoAtualizado.Preco,
                        QuantidadeEstoque = produtoAtualizado.QuantidadeEstoque,
                        DataCadastro = produtoAtualizado.DataCadastro
                    };
                    return Ok(produtoAtualizadoDTO);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o produto com ID {Id}.", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                }
            }

            // Endpoint para remover um produto
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)] //204 quando deleta e não retorna nada
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> RemoverProduto(int id)
            {
                try
                {
                    var resultado = await _produtoService.RemoverProduto(id);
                    if (!resultado)
                    {
                        return NotFound(new { message = "Produto não encontrado." });
                    }
                    return NoContent(); //retorna 204
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao remover o produto com ID {Id}.", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                }
            }
        }
    }