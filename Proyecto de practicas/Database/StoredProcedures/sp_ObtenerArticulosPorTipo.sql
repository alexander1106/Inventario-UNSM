-- Usado por ArticuloRepository.GetArticulosPivotPorTipoAsync
-- Devuelve los artículos de un TipoArticuloId con sus campos dinámicos
-- (CamposArticulos / ArticuloCamposValores) pivotados como columnas.
CREATE OR ALTER PROCEDURE sp_ObtenerArticulosPorTipo
    @TipoArticuloId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @cols NVARCHAR(MAX);
    DECLARE @sql NVARCHAR(MAX);

    SELECT @cols = STRING_AGG(QUOTENAME(NombreCampo), ',')
    FROM CamposArticulos
    WHERE TipoArticuloId = @TipoArticuloId;

    IF @cols IS NULL
    BEGIN
        SELECT
            a.Id,
            a.CodigoPatrimonial,
            a.CodigoBarra,
            a.Nombre,
            a.FechaAdquision,
            a.ValorAdquisitivo,
            a.Condicion,
            a.TipoArticuloId,
            a.UbicacionId,
            a.Marca,
            a.Modelo,
            a.NroSerie,
            a.Medidas,
            a.Color,
            a.TiempoVidaUtil,
            a.DepreciacionAnual,
            a.ValorActual,
            a.Estado
        FROM Articulos a
        WHERE a.TipoArticuloId = @TipoArticuloId;
        RETURN;
    END

    SET @sql = N'
        SELECT
            a.Id,
            a.CodigoPatrimonial,
            a.CodigoBarra,
            a.Nombre,
            a.FechaAdquision,
            a.ValorAdquisitivo,
            a.Condicion,
            a.TipoArticuloId,
            a.UbicacionId,
            a.Marca,
            a.Modelo,
            a.NroSerie,
            a.Medidas,
            a.Color,
            a.TiempoVidaUtil,
            a.DepreciacionAnual,
            a.ValorActual,
            a.Estado,
            ' + @cols + N'
        FROM Articulos a
        LEFT JOIN (
            SELECT acv.ArticuloId, ca.NombreCampo, acv.Valor
            FROM ArticuloCamposValores acv
            INNER JOIN CamposArticulos ca ON ca.Id = acv.CampoArticuloId
            WHERE ca.TipoArticuloId = @TipoArticuloIdParam
        ) AS src
        PIVOT (
            MAX(Valor) FOR NombreCampo IN (' + @cols + N')
        ) AS pvt ON pvt.ArticuloId = a.Id
        WHERE a.TipoArticuloId = @TipoArticuloIdParam;';

    EXEC sp_executesql @sql, N'@TipoArticuloIdParam INT', @TipoArticuloIdParam = @TipoArticuloId;
END
