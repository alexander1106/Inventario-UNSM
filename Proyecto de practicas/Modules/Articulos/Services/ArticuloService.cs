using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MiniExcelLibs;
using Proyecto_de_practicas.Modules.Articulos.DTO;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Modules.Articulos.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _repo;
        private readonly IMapper _mapper;

        public ArticuloService(IArticuloRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<string> UpdateArticuloConCampos(int id, ArticuloConCamposRequest request)
        {
            return await _repo.UpdateArticuloConCampos(id, request);
        }
        public async Task<List<ArticuloDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<ArticuloDto>>(entities);
        }

        public async Task<ArticuloDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ArticuloDto>(entity);
        }

        public async Task<ArticuloDto> AddAsync(ArticuloDto dto)
        {
            var entity = _mapper.Map<Articulo>(dto);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<ArticuloDto>(result);
        }

        public async Task<ArticuloDto> UpdateAsync(int id, ArticuloDto dto)
        {
            var entity = _mapper.Map<Articulo>(dto);
            entity.Id = id;
            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<ArticuloDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<ArticuloDto>> GetByTipoArticuloIdAsync(int tipoArticuloId)
        {
            var entities = await _repo.GetByTipoArticuloIdAsync(tipoArticuloId);
            return _mapper.Map<List<ArticuloDto>>(entities);
        }
       
        public async Task<List<ArticuloDto>> GetByUbicacionIdAsync(int ubicacionId)
        {
            var entities = await _repo.GetByUbicacionIdAsync(ubicacionId);
            return _mapper.Map<List<ArticuloDto>>(entities);
        }

        public async Task<ArticuloDto?> GetByCodigoCortoAsync(string codigoCorto)
        {
            var entity = await _repo.GetByCodigoCortoAsync(codigoCorto);
            return entity == null ? null : _mapper.Map<ArticuloDto>(entity);
        }

        /*
        public async Task<string> UpdateArticuloConCampos(ArticuloDto request)
        {
            return await _repo.UpdateArticuloConCampos(request);
        }
        */
        public async Task<string> GuardarArticuloConCampos(ArticuloConCamposRequest request)
        {
            return await _repo.GuardarArticuloConCampos(request);
        }
        

        public async Task<List<CampoArticuloDto>> GetCamposPorTipoArticuloAsync(int tipoArticuloId)
        {
            // Llama al repositorio para mantener la separación de responsabilidades
            return await _repo.GetCamposPorTipoArticuloAsync(tipoArticuloId);
        }
        public async Task<List<ArticuloDto>> GetAllConCamposAsync()
        {
            return await _repo.GetAllConCamposAsync();
        }


        public async Task<List<Dictionary<string, object>>> GetArticulosPivotPorTipoAsync(int tipoArticuloId)
        {
            return await _repo.GetArticulosPivotPorTipoAsync(tipoArticuloId);
        }

        public async Task<string> ProcesarCargaMasivaExcelAsync(IFormFile archivo, int ubicacionId)
        {
            int insertados = 0;
            int errores = 0;

            using (var stream = archivo.OpenReadStream())
            {
                // 🔥 CLAVE 1: Agregamos useHeaderRow: true para que use los títulos reales en vez de 'A', 'B', 'C'
                var filas = stream.Query(useHeaderRow: true).ToList();

                foreach (IDictionary<string, object> fila in filas)
                {
                    try
                    {
                        // Función interna de extracción inteligente y limpia
                        string ObtenerValor(string columnaExacta)
                        {
                            var llave = fila.Keys.FirstOrDefault(k => k.Trim().Equals(columnaExacta, StringComparison.OrdinalIgnoreCase));
                            return llave != null ? fila[llave]?.ToString()?.Trim() ?? string.Empty : string.Empty;
                        }

                        // 🎯 CLAVE 2: Mapeamos con los nombres exactos que vimos en tu consola de debug
                        string codigoPatrimonial = ObtenerValor("codigo_patrimonial");
                        string codigoBarra = ObtenerValor("codigo_barra");
                        string descripcion = ObtenerValor("descripcion");
                        string estadoConserv = ObtenerValor("estado_conserv");
                        string fecha_adquisicion = ObtenerValor("fecha_adquisicion");
                        string valorNetoTexto = ObtenerValor("valor_neto");

                        // Campos complementarios que vienen en tu log por si deseas guardarlos de una vez:
                        string marca = ObtenerValor("marca");
                        string modelo = ObtenerValor("modelo");
                        string nroSerie = ObtenerValor("nro_serie");
                        string color = ObtenerValor("color");
                        string medidas = ObtenerValor("medidas");
                        string cuentaMayor = ObtenerValor("mayor");
                        string subCuenta = ObtenerValor("sub_cta");
                        string hvalor_inicial = ObtenerValor("hvalor_inicial");
                        string hdepr_inicial = ObtenerValor("hdepr_inicial");
                        string hdepr_ajustada = ObtenerValor("hdepr_ajustada");
                        string hdepr_ejercicio = ObtenerValor("hdepr_ejercicio");

                        // 🧮 Conversión limpia del valor decimal
                        decimal valorNetoNumerico = 0;
                        if (!string.IsNullOrEmpty(valorNetoTexto))
                        {
                            // Cambiamos comas por puntos si el servidor maneja formato inglés
                            string limpio = valorNetoTexto.Replace("S/.", "").Replace("$", "").Replace(",", ".").Trim();
                            decimal.TryParse(limpio, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valorNetoNumerico);
                        }

                        // 📝 Construimos el Request bajo tus reglas de negocio
                        var nuevoArticulo = new ArticuloConCamposRequest
                        {
                            // Lo que está en descripción pasa como Nombre de la BD
                            Nombre = !string.IsNullOrEmpty(descripcion) ? descripcion : "Artículo Sin Descripción",

                            CodigoPatrimonial = codigoPatrimonial,
                            CodigoBarra = codigoBarra,
                            Condicion = !string.IsNullOrEmpty(estadoConserv) ? estadoConserv : "Bueno",
                            FechaAdquision = DateTime.Parse(fecha_adquisicion),

                            // El valor neto se registra en ambos campos
                            ValorAdquisitivo = valorNetoNumerico,
                            ValorNeto = valorNetoNumerico,

                            // Automatizaciones solicitadas
                            TipoArticuloId = 100, // Tipo "Otros"
                            UbicacionId = ubicacionId,
                            VidaUtil = 0,         // Forzado a 0 en todos
                            Estado = 1,

                            // Mapeos técnicos adicionales rescatados del excel
                            Marca = !string.IsNullOrEmpty(marca) ? marca : null,
                            Modelo = !string.IsNullOrEmpty(modelo) ? modelo : null,
                            NroSerie = !string.IsNullOrEmpty(nroSerie) ? nroSerie : null,
                            Color = !string.IsNullOrEmpty(color) ? color : null,
                            Medidas = !string.IsNullOrEmpty(medidas) ? medidas : null,
                            Mayor = !string.IsNullOrEmpty(cuentaMayor) ? cuentaMayor : null,
                            SubCta = !string.IsNullOrEmpty(subCuenta) ? subCuenta : null,
                            HValorInicial = decimal.Parse(hvalor_inicial),
                            HDeprInicial = decimal.Parse(hdepr_inicial),
                            HDeprAjustada = decimal.Parse(hdepr_ajustada),
                            HDeprEjercicio = decimal.Parse(hdepr_ejercicio),

                            // Lista vacía permitida por el DTO
                            CamposValores = new List<CampoValorDto>()
                        };

                        // 🚀 Ejecutamos el guardado
                        await GuardarArticuloConCampos(nuevoArticulo);
                        insertados++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar registro individual: {ex.Message}");
                        errores++;
                    }
                }
            }

            return $"Carga masiva finalizada. Éxito: {insertados} registros procesados. Errores: {errores}.";
        }

    }
}
