 		IF OBJECT_ID('Reservas.[paGuardarSolicitud]') IS NOT NULL	DROP PROC Reservas.[paGuardarSolicitud] 
		GO
		CREATE PROCEDURE Reservas.[paGuardarSolicitud]
			@pIdSolicitud int output
			,@pIdCatalogoConsejoRegional int = NULL
			,@pIdCatalogoSecretaria int = NULL
			,@pIdCatalogoComite int = NULL
			,@pNombreEvento varchar(500) = NULL
			,@pNumeroParticipantes int = NULL
			,@pFechaDesde varchar(10) = NULL
			,@pFechaHasta varchar(10) = NULL
			,@pNumeroDias int = NULL
			,@pHoraInicio varchar(10) = NULL
			,@pHoraFin varchar(10) = NULL
			,@pNumeroAuditorios int = NULL
			,@pResponsableEvento varchar(100) = NULL
			,@pTelefonoContacto varchar(20) = NULL
			,@pCorreoContacto varchar(250) = NULL
			,@pObservaciones text = NULL
			,@pIdSalaAsignada int = NULL
			,@pIdCatalogoEstadoSolicitud int = NULL
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(4000) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			SET NOCOUNT ON; 
			BEGIN TRY
			/*---AGREGAR ACA LAS RESTRICCIONES ANTES DE GUARDAR--
				DECLARE @vCantidad int=0
				DECLARE @vSubMensaje varchar(100)=''				
				IF @pIdSolicitud=0
				BEGIN
					SELECT @vCantidad=COUNT(*) from Reservas.Solicitud where EstadoAuditoria=1
					SET @vSubMensaje='LOS DATOS QUE INTENTA GUARDAR YA EXISTEN'
				END
				ELSE
				BEGIN
					SELECT @vCantidad=COUNT(*) from Reservas.Solicitud where EstadoAuditoria=1 AND IdSolicitud<>@pIdSolicitud
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
			IF @pIdSolicitud=0
			BEGIN
				INSERT INTO  Reservas.Solicitud
				( 
					IdCatalogoConsejoRegional
				,IdCatalogoSecretaria
				,IdCatalogoComite
				,NombreEvento
				,NumeroParticipantes
				,FechaDesde
				,FechaHasta
				,NumeroDias
				,HoraInicio
				,HoraFin
				,NumeroAuditorios
				,ResponsableEvento
				,TelefonoContacto
				,CorreoContacto
				,Observaciones
				,IdSalaAsignada
				,IdCatalogoEstadoSolicitud
				,IdUsuarioCreacionAuditoria
				)
				VALUES 
				(
									@pIdCatalogoConsejoRegional
				,@pIdCatalogoSecretaria
				,@pIdCatalogoComite
				,@pNombreEvento
				,@pNumeroParticipantes
				,@pFechaDesde
				,@pFechaHasta
				,@pNumeroDias
				,@pHoraInicio
				,@pHoraFin
				,@pNumeroAuditorios
				,@pResponsableEvento
				,@pTelefonoContacto
				,@pCorreoContacto
				,@pObservaciones
				,@pIdSalaAsignada
				,@pIdCatalogoEstadoSolicitud
				,@pIdUsuarioAuditoria
				)
					set @pIdSolicitud=SCOPE_IDENTITY()  
					SET @DescripcionMensaje ='SE INSERTÓ CORRECTAMENTE EL REGISTRO'
					SET @CodigoMensaje=0
				END
				ELSE
				BEGIN
					UPDATE  Reservas.Solicitud SET 
								IdCatalogoConsejoRegional=@pIdCatalogoConsejoRegional
				    ,IdCatalogoSecretaria=@pIdCatalogoSecretaria
				    ,IdCatalogoComite=@pIdCatalogoComite
				    ,NombreEvento=@pNombreEvento
				    ,NumeroParticipantes=@pNumeroParticipantes
				    ,FechaDesde=@pFechaDesde
				    ,FechaHasta=@pFechaHasta
				    ,NumeroDias=@pNumeroDias
				    ,HoraInicio=@pHoraInicio
				    ,HoraFin=@pHoraFin
				    ,NumeroAuditorios=@pNumeroAuditorios
				    ,ResponsableEvento=@pResponsableEvento
				    ,TelefonoContacto=@pTelefonoContacto
				    ,CorreoContacto=@pCorreoContacto
				    ,Observaciones=@pObservaciones
				    ,IdSalaAsignada=@pIdSalaAsignada
				    ,IdCatalogoEstadoSolicitud=@pIdCatalogoEstadoSolicitud
				    ,IdUsuarioActualizacionAuditoria=@pIdUsuarioAuditoria,FechaActualizacionAuditoria=getdate()
				WHERE IdSolicitud=@pIdSolicitud
					set @pIdSolicitud=@pIdSolicitud 
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paGuardarSolicitud',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD, SUCEDIÓ UN ERROR EN LA BASE DE DATOS ['+@ERROR_MESSAGE+']'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
			END CATCH

		GO
		IF OBJECT_ID('Reservas.[paEliminarSolicitud]') IS NOT NULL	DROP PROC Reservas.[paEliminarSolicitud] 
		GO
		CREATE PROCEDURE Reservas.[paEliminarSolicitud]
			@pIdSolicitud int = NULL
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(200) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			
			BEGIN TRY
			BEGIN TRANSACTION
			SET NOCOUNT ON; 
				UPDATE  Reservas.Solicitud SET EstadoAuditoria=0,IdUsuarioActualizacionAuditoria=@pIdUsuarioAuditoria, FechaActualizacionAuditoria=getdate() WHERE IdSolicitud=@pIdSolicitud
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paEliminarSolicitud',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD SUCEDIÓ UN ERROR EN LA BASE DE DATOS'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
			END CATCH
		GO

		IF OBJECT_ID('Reservas.[paObtenerSolicitud]') IS NOT NULL	DROP PROC Reservas.[paObtenerSolicitud] 
		GO
		CREATE PROCEDURE Reservas.[paObtenerSolicitud]
		@pIdSolicitud INT,
		@pIdUsuarioAuditoria int = NULL	
		AS
		BEGIN
		BEGIN TRY
		SELECT
									
								COALESCE(S.IdCatalogoConsejoRegional,0)IdCatalogoConsejoRegional,	
														COALESCE(C1.Descripcion,'')CatalogoConsejoRegional,	
												
								COALESCE(S.IdCatalogoSecretaria,0)IdCatalogoSecretaria,	
														COALESCE(C2.Descripcion,'')CatalogoSecretaria,	
												
								COALESCE(S.IdCatalogoComite,0)IdCatalogoComite,	
														COALESCE(C3.Descripcion,'')CatalogoComite,	
												COALESCE(S.NombreEvento,'')NombreEvento,	
														
								COALESCE(S.NumeroParticipantes,0)NumeroParticipantes,	
															COALESCE(S.FechaDesde,'')FechaDesde,	
														COALESCE(S.FechaHasta,'')FechaHasta,	
														
								COALESCE(S.NumeroDias,0)NumeroDias,	
															COALESCE(S.HoraInicio,'')HoraInicio,	
														COALESCE(S.HoraFin,'')HoraFin,	
														
								COALESCE(S.NumeroAuditorios,0)NumeroAuditorios,	
															COALESCE(S.ResponsableEvento,'')ResponsableEvento,	
														COALESCE(S.TelefonoContacto,'')TelefonoContacto,	
														COALESCE(S.CorreoContacto,'')CorreoContacto,	
														
								COALESCE(S.Observaciones,0)Observaciones,	
															
								COALESCE(S.IdSalaAsignada,0)IdSalaAsignada,	
															
								COALESCE(S.IdCatalogoEstadoSolicitud,0)IdCatalogoEstadoSolicitud,	
														COALESCE(C4.Descripcion,'')CatalogoEstadoSolicitud,	
							S.IdSolicitud
		FROM Reservas.Solicitud S 
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoConsejoRegional
							LEFT JOIN Reservas.Catalogo C2 ON C2.IdCatalogo =S.IdCatalogoSecretaria
							LEFT JOIN Reservas.Catalogo C3 ON C3.IdCatalogo =S.IdCatalogoComite
							LEFT JOIN Reservas.Catalogo C4 ON C4.IdCatalogo =S.IdCatalogoEstadoSolicitud
					WHERE S.EstadoAuditoria=1 and S.IdSolicitud=@pIdSolicitud
		END TRY
			BEGIN CATCH					
				DECLARE @ERROR_NUMBER INT
				DECLARE @ERROR_SEVERITY INT
				DECLARE @ERROR_STATE INT
				DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
				DECLARE @ERROR_LINE INT
				DECLARE @ERROR_MESSAGE VARCHAR(MAX)
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paObtenerSolicitud',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria               
			END CATCH
		END
		GO

		IF OBJECT_ID(N'Reservas.paListarSolicitud', N'P') IS NOT NULL drop PROCEDURE Reservas.paListarSolicitud
GO
--EXEC Reservas.paListarSolicitud 1,null,null,1,10,''
CREATE PROCEDURE Reservas.paListarSolicitud

	
	@pIdUsuarioAuditoria int,
	@pCampoOrdenado varchar(50),		
	@pTipoOrdenacion varchar(4),
	@pNumeroPagina INT,    
	@pDimensionPagina  INT,
	@pBusquedaGeneral varchar(100)
AS
	BEGIN TRY		
		SELECT
										COALESCE(S.IdCatalogoConsejoRegional,0)IdCatalogoConsejoRegional,	
														COALESCE(C1.Descripcion,'')CatalogoConsejoRegional,	
													COALESCE(S.IdCatalogoSecretaria,0)IdCatalogoSecretaria,	
														COALESCE(C2.Descripcion,'')CatalogoSecretaria,	
													COALESCE(S.IdCatalogoComite,0)IdCatalogoComite,	
														COALESCE(C3.Descripcion,'')CatalogoComite,	
												COALESCE(S.NombreEvento,'')NombreEvento,	
															COALESCE(S.NumeroParticipantes,0)NumeroParticipantes,	
															COALESCE(S.FechaDesde,'')FechaDesde,	
														COALESCE(S.FechaHasta,'')FechaHasta,	
															COALESCE(S.NumeroDias,0)NumeroDias,	
															COALESCE(S.HoraInicio,'')HoraInicio,	
														COALESCE(S.HoraFin,'')HoraFin,	
															COALESCE(S.NumeroAuditorios,0)NumeroAuditorios,	
															COALESCE(S.ResponsableEvento,'')ResponsableEvento,	
														COALESCE(S.TelefonoContacto,'')TelefonoContacto,	
														COALESCE(S.CorreoContacto,'')CorreoContacto,	
															COALESCE(S.Observaciones,0)Observaciones,	
																COALESCE(S.IdSalaAsignada,0)IdSalaAsignada,	
																COALESCE(S.IdCatalogoEstadoSolicitud,0)IdCatalogoEstadoSolicitud,	
														COALESCE(C4.Descripcion,'')CatalogoEstadoSolicitud,	
							S.IdSolicitud
		FROM Reservas.Solicitud S 
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoConsejoRegional
							LEFT JOIN Reservas.Catalogo C2 ON C2.IdCatalogo =S.IdCatalogoSecretaria
							LEFT JOIN Reservas.Catalogo C3 ON C3.IdCatalogo =S.IdCatalogoComite
							LEFT JOIN Reservas.Catalogo C4 ON C4.IdCatalogo =S.IdCatalogoEstadoSolicitud
					WHERE S.EstadoAuditoria=1	and (S.IdSolicitud=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
				ORDER BY IdSolicitud
		OFFSET (@pNumeroPagina-1)*@pDimensionPagina ROWS
		FETCH NEXT @pDimensionPagina ROWS ONLY

		SELECT COUNT(*) FROM Reservas.Solicitud S 		
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoConsejoRegional
							LEFT JOIN Reservas.Catalogo C2 ON C2.IdCatalogo =S.IdCatalogoSecretaria
							LEFT JOIN Reservas.Catalogo C3 ON C3.IdCatalogo =S.IdCatalogoComite
							LEFT JOIN Reservas.Catalogo C4 ON C4.IdCatalogo =S.IdCatalogoEstadoSolicitud
					WHERE S.EstadoAuditoria=1	and (S.IdSolicitud=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
			END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paListarSolicitud',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH
GO
IF OBJECT_ID(N'Reservas.paDescargarSolicitud', N'P') IS NOT NULL drop PROCEDURE Reservas.paDescargarSolicitud
GO
--EXEC Reservas.paDescargarSolicitud 1,null,null,1,10,''
CREATE PROCEDURE Reservas.paDescargarSolicitud
		
	@pIdUsuarioAuditoria int   	
AS
	BEGIN TRY		
		SELECT
								COALESCE(C1.Descripcion,'')[CatalogoConsejoRegional],	
											COALESCE(C2.Descripcion,'')[CatalogoSecretaria],	
											COALESCE(C3.Descripcion,'')[CatalogoComite],	
													COALESCE(S.NombreEvento,'')[NombreEvento],	
																	COALESCE(S.NumeroParticipantes,0) [NumeroParticipantes],	
																	COALESCE(S.FechaDesde,'')[FechaDesde],	
																COALESCE(S.FechaHasta,'')[FechaHasta],	
																	COALESCE(S.NumeroDias,0) [NumeroDias],	
																	COALESCE(S.HoraInicio,'')[HoraInicio],	
																COALESCE(S.HoraFin,'')[HoraFin],	
																	COALESCE(S.NumeroAuditorios,0) [NumeroAuditorios],	
																	COALESCE(S.ResponsableEvento,'')[ResponsableEvento],	
																COALESCE(S.TelefonoContacto,'')[TelefonoContacto],	
																COALESCE(S.CorreoContacto,'')[CorreoContacto],	
																	COALESCE(S.Observaciones,0) [Observaciones],	
															COALESCE(C4.Descripcion,'')[CatalogoEstadoSolicitud],	
							S.IdSolicitud
		FROM Reservas.Solicitud S 
						LEFT JOIN Reservas.Catalogo C1 ON C1.IdCatalogo =S.IdCatalogoConsejoRegional
							LEFT JOIN Reservas.Catalogo C2 ON C2.IdCatalogo =S.IdCatalogoSecretaria
							LEFT JOIN Reservas.Catalogo C3 ON C3.IdCatalogo =S.IdCatalogoComite
							LEFT JOIN Reservas.Catalogo C4 ON C4.IdCatalogo =S.IdCatalogoEstadoSolicitud
					
		WHERE S.EstadoAuditoria=1--	and (S.IdSolicitud=@pBusquedaGeneral OR @pBusquedaGeneral is null OR @pBusquedaGeneral=0)
				ORDER BY IdSolicitud
	
	END TRY
	BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paDescargarSolicitud',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
	 END CATCH
GO

			
			GO
			IF OBJECT_ID(N'Reservas.[paListarComboSolicitud]', N'P') IS NOT NULL drop PROCEDURE Reservas.[paListarComboSolicitud]
			GO
			CREATE PROCEDURE Reservas.[paListarComboSolicitud]
			@pIdUsuarioAuditoria int			
			AS
			BEGIN
				BEGIN TRY
					SELECT
					IdSolicitud,
					NombreEvento	
					FROM
					Reservas.Solicitud
					WHERE EstadoAuditoria=1
				END TRY
				BEGIN CATCH					
					DECLARE @ERROR_NUMBER INT
					DECLARE @ERROR_SEVERITY INT
					DECLARE @ERROR_STATE INT
					DECLARE @ERROR_PROCEDURE VARCHAR(MAX)
					DECLARE @ERROR_LINE INT
					DECLARE @ERROR_MESSAGE VARCHAR(MAX)
					SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Reservas.paListarComboSolicitud',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()				
					EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
				END CATCH
			END
			