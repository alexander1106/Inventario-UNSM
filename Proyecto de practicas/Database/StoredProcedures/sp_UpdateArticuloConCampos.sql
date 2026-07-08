-- Usado por ArticuloRepository.UpdateArticuloConCampos
-- Actualiza un Articulo y reemplaza sus valores de campos dinamicos (ArticuloCamposValores)
CREATE OR ALTER PROCEDURE sp_UpdateArticuloConCampos
(
    @Id                 INT,
    @CodigoPatrimonial  NVARCHAR(100),
    @Nombre             NVARCHAR(200),
    @FechaAdquision     DATE,
    @ValorAdquisitivo   FLOAT,
    @Condicion          NVARCHAR(50),
    @TipoArticuloId     INT,
    @UbicacionId        INT = NULL,
    @Estado             INT,
    @CodigoBarra        NVARCHAR(100) = NULL,
    @Marca              NVARCHAR(100) = NULL,
    @Modelo             NVARCHAR(100) = NULL,
    @NroSerie           NVARCHAR(100) = NULL,
    @OtrasObservaciones NVARCHAR(500) = NULL,
    @TiempoVidaUtil     FLOAT = 0,
    @ClasificacionDepreciacionId INT = NULL,
    @DepreciacionAnual  FLOAT = 0,
    @CamposJSON         NVARCHAR(MAX) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Articulos
    SET
        CodigoPatrimonial = @CodigoPatrimonial,
        Nombre = @Nombre,
        FechaAdquision = @FechaAdquision,
        ValorAdquisitivo = @ValorAdquisitivo,
        Condicion = @Condicion,
        TipoArticuloId = @TipoArticuloId,
        UbicacionId = @UbicacionId,
        Estado = @Estado,
        CodigoBarra = @CodigoBarra,
        Marca = @Marca,
        Modelo = @Modelo,
        NroSerie = @NroSerie,
        OtrasObservaciones = @OtrasObservaciones,
        TiempoVidaUtil = @TiempoVidaUtil,
        ClasificacionDepreciacionId = @ClasificacionDepreciacionId,
        DepreciacionAnual = @DepreciacionAnual
    WHERE Id = @Id;

    IF @CamposJSON IS NOT NULL
       AND LTRIM(RTRIM(@CamposJSON)) <> ''
       AND ISJSON(@CamposJSON) = 1
    BEGIN
        DELETE FROM ArticuloCamposValores WHERE ArticuloId = @Id;

        INSERT INTO ArticuloCamposValores
        (
            ArticuloId,
            CampoArticuloId,
            Valor
        )
        SELECT
            @Id,
            CA.Id,
            J.valor
        FROM OPENJSON(@CamposJSON)
        WITH
        (
            campo NVARCHAR(100),
            valor NVARCHAR(500)
        ) AS J
        INNER JOIN CamposArticulos AS CA
            ON CA.NombreCampo = J.campo
           AND CA.TipoArticuloId = @TipoArticuloId;
    END
END;
