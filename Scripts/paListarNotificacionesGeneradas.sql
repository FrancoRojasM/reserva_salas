USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Casilla].[paListarNotificacionesGeneradas]    Script Date: 20/06/2024 06:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC Casilla.paListarNotificacionesGeneradas 1,null,null,1,10,''
ALTER PROCEDURE [Casilla].[paListarNotificacionesGeneradas]

	@pIdArea int,
	@pIdUsuarioAuditoria int,
	@pCampoOrdenado varchar(50),		
	@pTipoOrdenacion varchar(4),
	@pNumeroPagina INT,    
	@pDimensionPagina  INT,
	@pBusquedaGeneral varchar(100)
AS
	BEGIN TRY		
		SELECT
		COALESCE(N.IdAdministradoNotificado,0)IdAdministradoNotificado,	
		COALESCE(P1.NombreCompleto,'') NombreCompletoPersonaNotificada,	
		COALESCE(N.IdCatalogoCategoria,0)IdCatalogoCategoria,	
		COALESCE(C2.Descripcion,'')CatalogoCategoria,	
		COALESCE(N.AsuntoNotificacion,'')AsuntoNotificacion,	
		COALESCE(N.MensajeNotificacion,'')MensajeNotificacion,	
		N.FechaHoraNotificacionEnviada,
		N.NotificacionEnviada,
		N.FechaHoraRecepcionNotificacion,
		N.NotificacionRecibida,
		N.MensajeNotificacionHtml,
		N.IdNotificacion,
		N.IdPeriodo,
		N.IdAreaNotificador,
			N.NumeroNotificacion
			,N.FechaHoraRegistroNotificacion
			,CONCAT('N',RIGHT(CONCAT('00000',N.NumeroNotificacion),5),'-',N.IdPeriodo) NombreNumeroNotificacion
			,(SELECT COUNT(*) FROM Casilla.NotificacionArchivo WHERE EstadoAuditoria=1 AND IdNotificacion=N.IdNotificacion) CantidadArchivos
		,AR.NombreArea
		,A.EmailNotificacion

		FROM Casilla.Notificacion N 
		INNER JOIN General.Area AR ON AR.IdArea=N.IdAreaNotificador
		INNER JOIN Casilla.Administrado A ON A.IdAdministrado =N.IdAdministradoNotificado
		INNER JOIN General.Persona P1 ON P1.IdPersona =A.IdPersona
		LEFT JOIN Casilla.Catalogo C2 ON C2.IdCatalogo =N.IdCatalogoCategoria
		WHERE N.EstadoAuditoria=1	
		and N.IdAreaNotificador=@pIdArea
		AND N.NotificacionEnviada=0
		and (N.IdNotificacion=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
		ORDER BY IdNotificacion desc
		OFFSET (@pNumeroPagina-1)*@pDimensionPagina ROWS
		FETCH NEXT @pDimensionPagina ROWS ONLY

		SELECT COUNT(*) FROM Casilla.Notificacion N 		
		INNER JOIN General.Area AR ON AR.IdArea=N.IdAreaNotificador
						LEFT JOIN Casilla.Catalogo C1 ON C1.IdCatalogo =N.IdCatalogoCategoria
		WHERE N.EstadoAuditoria=1	
		AND N.NotificacionEnviada=0
		and N.IdAreaNotificador=@pIdArea
		and (N.IdNotificacion=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
			END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Casilla.paListarNotificacionesGeneradas',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH
