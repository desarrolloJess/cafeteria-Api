using ApiExamen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        //Variable de ccontexto de BD
        private readonly ExamenContext _baseDatos;

        public ProductosController(ExamenContext baseDatos)
        {
            this._baseDatos = baseDatos;
        }

        //Método GET ListarTareas
        [HttpGet]
        [Route("ListaProductos")]
        public async Task<IActionResult> ListaProductos()
        {
            var listaProductos = await _baseDatos.Productos
            .Include(p => p.Categoria)
            .Select(p => new {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Imagen = p.Imagen,
                Categoria = p.Categoria.Nombre
            })
            .ToListAsync();

            return Ok(listaProductos);
        }

        [HttpGet]
        [Route("ListaProductosPorCategoria")]
        public async Task<IActionResult> ListaProductosPorCategoria([FromQuery] List<int> categoriaIds)
        {
            var listaProductos = await _baseDatos.Productos
                .Include(p => p.Categoria)
                .Where(p => categoriaIds.Contains(p.CategoriaId))
                .Select(p => new {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Imagen = p.Imagen,
                    Categoria = p.Categoria.Nombre
                })
                .ToListAsync();

            return Ok(listaProductos);
        }

        [HttpGet]
        [Route("ListaProductosLimitados/{limite}")]
        public async Task<IActionResult> ListaProductosLimitados(int limite)
        {
            var listaProductos = await _baseDatos.Productos
                .Include(p => p.Categoria) 
                .Take(limite) 
                .Select(p => new {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Imagen = p.Imagen,
                    Categoria = p.Categoria.Nombre 
                })
                .ToListAsync();

            return Ok(listaProductos);
        }

        [HttpGet]
        [Route("ListaCategorias")]
        public async Task<IActionResult> ListaCategorias()
        {
            var listaCategorias = await _baseDatos.Categorias
                .Select(c => new {
                    id = c.Id,
                    nombre = c.Nombre
                })
                .ToListAsync();

            return Ok(listaCategorias);
        }



    }
}
