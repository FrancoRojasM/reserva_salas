 		IF OBJECT_ID('Reservas.[paGuardarSala]') IS NOT NULL	DROP PROC Reservas.[paGuardarSala] 
		GO
		CREATE PROCEDURE Reservas.[paGuardarSala]
			@pIdSala int output
			,@pNombre varchar(200) = NULL
			,@pAforo int = NULL
			,@pIdCatalogoPiso int = NULL
			,@pObservaciones text = NULL
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(4000) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			SET NOCOUNT ON; 
			BEGIN TRY
			/*---AGREGAR ACA LAS RESTRICCIONES ANTES DE GUARDAR--
				DECLARE @vCantidad int=0
				DECLARE @vSubMensaje varchar(100)=''				
				IF @pIdSala=0
				BEGIN
					SELECT @vCantidad=COUNT(*) from Reservas.Sala where EstadoAuditoria=1
					SET @vSubMensaje='LOS DATOS QUE INTENTA GUARDAR YA EXISTEN'
				END
				ELSE
				BEGIN
					SELECT @vCantidad=COUNT(*) from Reservas.Sala where EstadoAuditoria=1 AND IdSala<>@pIdSala
					SET @vSubMensaje='LOS DATOS QUE INTENTA ACTUALIZAR YA EXISTEN'
				END
				IF @vCantidad>0 
				BEGIN					
					SET @DescripcionMensaje =@vSubMensaje
					SET @CodigoMensaje=1
					RETURN
				END
			*/
			BEGIN TRANSACTION			
			IF @pIdSala=0
			BEGIN
				INSERT INTO  Reservas.Sala
				( 
					Nombre
				,Aforo
				,IdCatalogoPiso
				,Observaciones
				,IdUsuarioCreacionAuditoria
				)
				VALUES 
				(
									@pNombre
				,@pAforo
				,@pIdCatalogoPiso
				,@pObservaciones
				,@pIdUsuarioAuditoria
				)
					set @pIdSala=SCOPE_IDENTITY()  
					SET @DescripcionMensaje ='SE INSERTÓ CORRECTAMENTE EL REGISTRO'
					SET @CodigoMensaje=0
				END
				ELSE
				BEGIN
					UPDATE  Reservas.Sala SET 
								Nombre=@pNombre
				    ,Aforo=@pAforo
				    ,IdCatalogoPiso=@pIdCatalogoPiso
				    ,Observaciones=@pObservaciones
				    ,IdUsuarioActualizacionAuditoria=@pIdUsuarioAuditoria,FechaActualizacionAuditoria=getdate()
				WHERE IdSala=@pIdSala
					set @pIdSala=@pIdSala 
				SET @DescripcionMensaje ='SE ACTUALIZÓ CORRECTAMENTE EL REGISTRO'
				SET @CodigoMensaje=0
			END	
			COMMIT TRANSACTION
			END TRY
			BEGIN CATCH		
				ROLLBACK TRANSACTION
				DECLARE @ERROR_NUMBER INT
				DECLARE @ERROR_SEVERITY INT
				DECLARE @ERROR_STATE INT
				DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
				DECLARE @ERROR_LINE INT
				DECLARE @ERROR_MESSAGE VARCHAR(MAX)
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paGuardarSala',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD, SUCEDIÓ UN ERROR EN LA BASE DE DATOS ['+@ERROR_MESSAGE+']'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
			END CATCH

		GO
		IF OBJECT_ID('Reservas.[paEliminarSala]') IS NOT NULL	DROP PROC Reservas.[paEliminarSala] 
		GO
		CREATE PROCEDURE Reservas.[paEliminarSala]
			@pIdSala int = NULL
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(200) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			
			BEGIN TRY
			BEGIN TRANSACTION
			SET NOCOUNT ON; 
				UPDATE  Reservas.Sala SET EstadoAuditoria=0,IdUsuarioActualizacionAuditoria=@pIdUsuarioAuditoria, FechaActualizacionAuditoria=getdate() WHERE IdSala=@pIdSala
				SET @DescripcionMensaje ='SE ELIMINÓ CORRECTAMENTE EL REGISTRO'
				SET @CodigoMensaje=0
			COMMIT TRANSACTION
			END TRY
			BEGIN CATCH		
				ROLLBACK TRANSACTION
				DECLARE @ERROR_NUMBER INT
				DECLARE @ERROR_SEVERITY INT
				DECLARE @ERROR_STATE INT
				DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
				DECLARE @ERROR_LINE INT
				DECLARE @ERROR_MESSAGE VARCHAR(MAX)
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paEliminarSala',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD SUCEDIÓ UN ERROR EN LA BASE DE DATOS'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
			END CATCH
		GO

		IF OBJECT_ID('Reservas.[paObtenerSala]') IS NOT NULL	DROP PROC Reservas.[paObtenerSala] 
		GO
		CREATE PROCEDURE Reservas.[paObtenerSala]
		@pIdSala INT,
		@pIdUsuarioAuditoria int = NULL	
		AS
		BEGIN
		BEGIN TRY
		SELECT
									COALESCE(S.Nombre,'')Nombre,	
														
								COALESCE(S.Aforo,0)Aforo,	
															
								COALESCE(S.IdCatalogoPiso,0)IdCatalogoPiso,	
														COALESCE(C1.Descripcion,'')CatalogoPiso,	
												
								COALESCE(S.Observaciones,0)Observaciones,	
										S.IdSala
		FROM Reservas.Sala S 
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoPiso
					WHERE S.EstadoAuditoria=1 and S.IdSala=@pIdSala
		END TRY
			BEGIN CATCH					
				DECLARE @ERROR_NUMBER INT
				DECLARE @ERROR_SEVERITY INT
				DECLARE @ERROR_STATE INT
				DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
				DECLARE @ERROR_LINE INT
				DECLARE @ERROR_MESSAGE VARCHAR(MAX)
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paObtenerSala',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria               
			END CATCH
		END
		GO

		IF OBJECT_ID(N'Reservas.paListarSala', N'P') IS NOT NULL drop PROCEDURE Reservas.paListarSala
GO
--EXEC Reservas.paListarSala 1,null,null,1,10,''
CREATE PROCEDURE Reservas.paListarSala

	
	@pIdUsuarioAuditoria int,
	@pCampoOrdenado varchar(50),		
	@pTipoOrdenacion varchar(4),
	@pNumeroPagina INT,    
	@pDimensionPagina  INT,
	@pBusquedaGeneral varchar(100)
AS
	BEGIN TRY		
		SELECT
									COALESCE(S.Nombre,'')Nombre,	
															COALESCE(S.Aforo,0)Aforo,	
																COALESCE(S.IdCatalogoPiso,0)IdCatalogoPiso,	
														COALESCE(C1.Descripcion,'')CatalogoPiso,	
													COALESCE(S.Observaciones,0)Observaciones,	
										S.IdSala
		FROM Reservas.Sala S 
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoPiso
					WHERE S.EstadoAuditoria=1	and (S.IdSala=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
				ORDER BY IdSala
		OFFSET (@pNumeroPagina-1)*@pDimensionPagina ROWS
		FETCH NEXT @pDimensionPagina ROWS ONLY

		SELECT COUNT(*) FROM Reservas.Sala S 		
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoPiso
					WHERE S.EstadoAuditoria=1	and (S.IdSala=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
			END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paListarSala',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH
GO
IF OBJECT_ID(N'Reservas.paDescargarSala', N'P') IS NOT NULL drop PROCEDURE Reservas.paDescargarSala
GO
--EXEC Reservas.paDescargarSala 1,null,null,1,10,''
CREATE PROCEDURE Reservas.paDescargarSala
		
	@pIdUsuarioAuditoria int   	
AS
	BEGIN TRY		
		SELECT
										COALESCE(S.Nombre,'')[Nombre],	
																	COALESCE(S.Aforo,0) [Aforo],	
															COALESCE(C1.Descripcion,'')[CatalogoPiso],	
														COALESCE(S.Observaciones,0) [Observaciones],	
											S.IdSala
		FROM Reservas.Sala S 
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoPiso
					
		WHERE S.EstadoAuditoria=1--	and (S.IdSala=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
				ORDER BY IdSala
	
	END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paDescargarSala',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH
GO

			
			GO
			IF OBJECT_ID(N'Reservas.[paListarComboSala]', N'P') IS NOT NULL drop PROCEDURE Reservas.[paListarComboSala]
			GO
			CREATE PROCEDURE Reservas.[paListarComboSala]
			@pIdUsuarioAuditoria int			
			AS
			BEGIN
				BEGIN TRY
					SELECT
					IdSala,
					Nombre	
					FROM
					Reservas.Sala
					WHERE EstadoAuditoria=1
				END TRY
				BEGIN CATCH					
					DECLARE @ERROR_NUMBER INT
					DECLARE @ERROR_SEVERITY INT
					DECLARE @ERROR_STATE INT
					DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
					DECLARE @ERROR_LINE INT
					DECLARE @ERROR_MESSAGE VARCHAR(MAX)
					SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paListarComboSala',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()				
					EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
				END CATCH
			END
			