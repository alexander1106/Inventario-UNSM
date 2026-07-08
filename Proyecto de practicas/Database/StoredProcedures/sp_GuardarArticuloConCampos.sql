    -- Usado por ArticuloRepository.GuardarArticuloConCampos
-- Inserta un Articulo y sus valores de campos dinamicos (ArticuloCamposValores)
CREATE OR ALTER PROCEDURE sp_GuardarArticuloConCampos
(
    @QRCodeBase64       NVARCHAR(MAX) = NULL,
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

    DECLARE @ArticuloId INT;

    INSERT INTO Articulos
    (
        CodigoPatrimonial,
        Nombre,
        FechaAdquision,
        ValorAdquisitivo,
        Condicion,
        TipoArticuloId,
        UbicacionId,
        Estado,
        CodigoBarra,
        Marca,
        Modelo,
        NroSerie,
        OtrasObservaciones,
        TiempoVidaUtil,
        ClasificacionDepreciacionId,
        DepreciacionAnual,
        ValorActual
    )
    VALUES
    (
        @CodigoPatrimonial,
        @Nombre,
        @FechaAdquision,
        @ValorAdquisitivo,
        @Condicion,
        @TipoArticuloId,
        @UbicacionId,
        @Estado,
        @CodigoBarra,
        @Marca,
        @Modelo,
        @NroSerie,
        @OtrasObservaciones,
        @TiempoVidaUtil,
        @ClasificacionDepreciacionId,
        @DepreciacionAnual,
        0
    );

    SET @ArticuloId = SCOPE_IDENTITY();

    IF @CamposJSON IS NOT NULL
       AND LTRIM(RTRIM(@CamposJSON)) <> ''
       AND ISJSON(@CamposJSON) = 1
    BEGIN
        INSERT INTO ArticuloCamposValores
        (
            ArticuloId,
            CampoArticuloId,
            Valor
        )
        SELECT
            @ArticuloId,
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
