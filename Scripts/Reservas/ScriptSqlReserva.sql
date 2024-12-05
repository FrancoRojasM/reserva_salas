 		IF OBJECT_ID('Reservas.[paGuardarReserva]') IS NOT NULL	DROP PROC Reservas.[paGuardarReserva] 
		GO
		CREATE PROCEDURE Reservas.[paGuardarReserva]
			@pIdReserva int output
			,@pIdSolicitud int = NULL
			,@pIdSala int = NULL
			,@pFechaDesdeReserva varchar(10) = NULL
			,@pFechaHastaReserva varchar(10) = NULL
			,@pHoraInicio varchar(10) = NULL
			,@pHoraFin varchar(10) = NULL
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
				IF @pIdReserva=0
				BEGIN
					SELECT @vCantidad=COUNT(*) from Reservas.Reserva where EstadoAuditoria=1
					SET @vSubMensaje='LOS DATOS QUE INTENTA GUARDAR YA EXISTEN'
				END
				ELSE
				BEGIN
					SELECT @vCantidad=COUNT(*) from Reservas.Reserva where EstadoAuditoria=1 AND IdReserva<>@pIdReserva
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
			IF @pIdReserva=0
			BEGIN
				INSERT INTO  Reservas.Reserva
				( 
					IdSolicitud
				,IdSala
				,FechaDesdeReserva
				,FechaHastaReserva
				,HoraInicio
				,HoraFin
				,Observaciones
				,IdUsuarioCreacionAuditoria
				)
				VALUES 
				(
									@pIdSolicitud
				,@pIdSala
				,@pFechaDesdeReserva
				,@pFechaHastaReserva
				,@pHoraInicio
				,@pHoraFin
				,@pObservaciones
				,@pIdUsuarioAuditoria
				)
					set @pIdReserva=SCOPE_IDENTITY()  
					SET @DescripcionMensaje ='SE INSERTÓ CORRECTAMENTE EL REGISTRO'
					SET @CodigoMensaje=0
				END
				ELSE
				BEGIN
					UPDATE  Reservas.Reserva SET 
								IdSolicitud=@pIdSolicitud
				    ,IdSala=@pIdSala
				    ,FechaDesdeReserva=@pFechaDesdeReserva
				    ,FechaHastaReserva=@pFechaHastaReserva
				    ,HoraInicio=@pHoraInicio
				    ,HoraFin=@pHoraFin
				    ,Observaciones=@pObservaciones
				    ,IdUsuarioActualizacionAuditoria=@pIdUsuarioAuditoria,FechaActualizacionAuditoria=getdate()
				WHERE IdReserva=@pIdReserva
					set @pIdReserva=@pIdReserva 
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paGuardarReserva',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD, SUCEDIÓ UN ERROR EN LA BASE DE DATOS ['+@ERROR_MESSAGE+']'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
			END CATCH

		GO
		IF OBJECT_ID('Reservas.[paEliminarReserva]') IS NOT NULL	DROP PROC Reservas.[paEliminarReserva] 
		GO
		CREATE PROCEDURE Reservas.[paEliminarReserva]
			@pIdReserva int = NULL
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(200) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			
			BEGIN TRY
			BEGIN TRANSACTION
			SET NOCOUNT ON; 
				UPDATE  Reservas.Reserva SET EstadoAuditoria=0,IdUsuarioActualizacionAuditoria=@pIdUsuarioAuditoria, FechaActualizacionAuditoria=getdate() WHERE IdReserva=@pIdReserva
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paEliminarReserva',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD SUCEDIÓ UN ERROR EN LA BASE DE DATOS'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
			END CATCH
		GO

		IF OBJECT_ID('Reservas.[paObtenerReserva]') IS NOT NULL	DROP PROC Reservas.[paObtenerReserva] 
		GO
		CREATE PROCEDURE Reservas.[paObtenerReserva]
		@pIdReserva INT,
		@pIdUsuarioAuditoria int = NULL	
		AS
		BEGIN
		BEGIN TRY
		SELECT
									
								COALESCE(R.IdSolicitud,0)IdSolicitud,	
															
								COALESCE(R.IdSala,0)IdSala,	
															COALESCE(R.FechaDesdeReserva,'')FechaDesdeReserva,	
														COALESCE(R.FechaHastaReserva,'')FechaHastaReserva,	
														COALESCE(R.HoraInicio,'')HoraInicio,	
														COALESCE(R.HoraFin,'')HoraFin,	
														
								COALESCE(R.Observaciones,0)Observaciones,	
										R.IdReserva
		FROM Reservas.Reserva R 
				WHERE R.EstadoAuditoria=1 and R.IdReserva=@pIdReserva
		END TRY
			BEGIN CATCH					
				DECLARE @ERROR_NUMBER INT
				DECLARE @ERROR_SEVERITY INT
				DECLARE @ERROR_STATE INT
				DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
				DECLARE @ERROR_LINE INT
				DECLARE @ERROR_MESSAGE VARCHAR(MAX)
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paObtenerReserva',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria               
			END CATCH
		END
		GO

		IF OBJECT_ID(N'Reservas.paListarReserva', N'P') IS NOT NULL drop PROCEDURE Reservas.paListarReserva
GO
--EXEC Reservas.paListarReserva 1,null,null,1,10,''
CREATE PROCEDURE Reservas.paListarReserva

	
	@pIdUsuarioAuditoria int,
	@pCampoOrdenado varchar(50),		
	@pTipoOrdenacion varchar(4),
	@pNumeroPagina INT,    
	@pDimensionPagina  INT,
	@pBusquedaGeneral varchar(100)
AS
	BEGIN TRY		
		SELECT
										COALESCE(R.IdSolicitud,0)IdSolicitud,	
																COALESCE(R.IdSala,0)IdSala,	
															COALESCE(R.FechaDesdeReserva,'')FechaDesdeReserva,	
														COALESCE(R.FechaHastaReserva,'')FechaHastaReserva,	
														COALESCE(R.HoraInicio,'')HoraInicio,	
														COALESCE(R.HoraFin,'')HoraFin,	
															COALESCE(R.Observaciones,0)Observaciones,	
										R.IdReserva
		FROM Reservas.Reserva R 
				WHERE R.EstadoAuditoria=1	and (R.IdReserva=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
				ORDER BY IdReserva
		OFFSET (@pNumeroPagina-1)*@pDimensionPagina ROWS
		FETCH NEXT @pDimensionPagina ROWS ONLY

		SELECT COUNT(*) FROM Reservas.Reserva R 		
				WHERE R.EstadoAuditoria=1	and (R.IdReserva=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
			END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paListarReserva',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH
GO
IF OBJECT_ID(N'Reservas.paDescargarReserva', N'P') IS NOT NULL drop PROCEDURE Reservas.paDescargarReserva
GO
--EXEC Reservas.paDescargarReserva 1,null,null,1,10,''
CREATE PROCEDURE Reservas.paDescargarReserva
		
	@pIdUsuarioAuditoria int   	
AS
	BEGIN TRY		
		SELECT
										COALESCE(R.FechaDesdeReserva,'')[FechaDesdeReserva],	
																COALESCE(R.FechaHastaReserva,'')[FechaHastaReserva],	
																COALESCE(R.HoraInicio,'')[HoraInicio],	
																COALESCE(R.HoraFin,'')[HoraFin],	
																	COALESCE(R.Observaciones,0) [Observaciones],	
											R.IdReserva
		FROM Reservas.Reserva R 
				
		WHERE R.EstadoAuditoria=1--	and (R.IdReserva=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
				ORDER BY IdReserva
	
	END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paDescargarReserva',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH
GO

		