USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Casilla].[paListarCategoriaMisNotificaciones]    Script Date: 18/06/2024 04:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC Casilla.paListarCategoriaMisNotificaciones 11,''
ALTER PROCEDURE [Casilla].[paListarCategoriaMisNotificaciones]	
	@pIdUsuarioAuditoria int,
	@pBusquedaGeneral varchar(100)
AS
	BEGIN TRY		
	--SELECT * FROM RecursoHumano.visEmpleadoPerfilPersona
		SELECT		
		COALESCE(N.IdCatalogoCategoria,0)IdCatalogoCategoria,	
		COALESCE(C2.Descripcion,'')CatalogoCategoria,	
		COALESCE(C2.Detalle,'')DetalleCategoria,	
		COUNT(N.IdCatalogoCategoria)CantidadCatalogoCategoriaRecibidas,
		SUM(case when N.NotificacionRecibida=1 then 0 else 1 end) CantidadCatalogoCategoria
	--	N.NotificacionRecibida
		FROM Casilla.Notificacion N 
		INNER JOIN General.Area AR ON AR.IdArea=N.IdAreaNotificador
		INNER JOIN Casilla.Administrado A ON A.IdAdministrado =N.IdAdministradoNotificado
		INNER JOIN General.Persona P1 ON P1.IdPersona =A.IdPersona
		INNER JOIN Casilla.Catalogo C2 ON C2.IdCatalogo =N.IdCatalogoCategoria
		WHERE N.EstadoAuditoria=1
		AND N.NotificacionEnviada=1		
		AND A.IdUsuario =@pIdUsuarioAuditoria
		GROUP BY N.IdCatalogoCategoria,C2.Descripcion,C2.Detalle--,N.NotificacionRecibida
		ORDER BY C2.Descripcion
	END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Casilla.paListarNotificacion',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH



	-- select * FROM Casilla.Notificacion N 