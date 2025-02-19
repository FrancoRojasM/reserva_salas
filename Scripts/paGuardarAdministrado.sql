USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Casilla].[paGuardarAdministrado]    Script Date: 20/06/2024 05:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
		ALTER PROCEDURE [Casilla].[paGuardarAdministrado]
			@pIdAdministrado int output
			,@pIdPersona int = NULL
			,@pIdUsuario int = NULL
			,@pEmailNotificacion varchar(100) = NULL
			,@pNumeroCelularNotificacion varchar(100) = NULL
			,@pAsientoElectronico varchar(100) = NULL
			,@pPartidaElectronica varchar(100) = NULL
			,@pActivo bit = NULL

			,@pCodigoTelefonoConfirmacion varchar(10) = NULL
			,@pCodigoCorreoConfirmacion varchar(10) = NULL

			,@pCodigoCorreoValidar varchar(10) = NULL
			,@pCodigoTelefonoValidar varchar(10) = NULL


			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(4000) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			SET NOCOUNT ON; 
			BEGIN TRY
			/*---AGREGAR ACA LAS RESTRICCIONES ANTES DE GUARDAR--*/
				--DECLARE @vCantidad int=0
				--DECLARE @vSubMensaje varchar(100)=''				
				--IF @pIdAdministrado=0
				--BEGIN
				--	SELECT @vCantidad=COUNT(*) from Casilla.Administrado where EstadoAuditoria=1
				--	SET @vSubMensaje='LOS DATOS QUE INTENTA GUARDAR YA EXISTEN'
				--END
				--ELSE
				--BEGIN
				--	SELECT @vCantidad=COUNT(*) from Casilla.Administrado where EstadoAuditoria=1 AND IdAdministrado<>@pIdAdministrado
				--	SET @vSubMensaje='LOS DATOS QUE INTENTA ACTUALIZAR YA EXISTEN'
				--END
				--IF @vCantidad>0 
				--BEGIN					
				--	SET @DescripcionMensaje =@vSubMensaje
				--	SET @CodigoMensaje=1
				--	RETURN
				--END
			DECLARE @EmailNotificacionValidacion VARCHAR(100),@CodigoCorreoValidacion VARCHAR(6) ,@NumeroCelularValidacion  VARCHAR(100) ,@CodigoTelefonoValidacion VARCHAR(6) 
			
			SELECT
			@EmailNotificacionValidacion=EmailNotificacionValidacion
			,@CodigoCorreoValidacion=CodigoCorreoValidacion
			,@NumeroCelularValidacion=NumeroCelularValidacion 
			,@CodigoTelefonoValidacion=CodigoTelefonoValidacion
			FROM Casilla.Administrado WHERE IdAdministrado=@pIdAdministrado


			IF @pEmailNotificacion<>@EmailNotificacionValidacion OR  @CodigoCorreoValidacion<>@pCodigoCorreoConfirmacion
			BEGIN
				SET @DescripcionMensaje ='CORREO O CODIGO DE VERIFICACIÓN NO VALIDO'
				SET @CodigoMensaje=1
				RETURN

			END

			IF @pNumeroCelularNotificacion<>@NumeroCelularValidacion OR  @CodigoTelefonoValidacion<>@pCodigoTelefonoConfirmacion
			BEGIN
				SET @DescripcionMensaje ='CORREO O CODIGO DE VERIFICACIÓN NO VALIDO'
				SET @CodigoMensaje=1
				RETURN

			END
			
			BEGIN TRANSACTION			
			IF @pIdAdministrado=0
			BEGIN
				INSERT INTO  Casilla.Administrado
				( 
					IdPersona
				,IdUsuario
				,EmailNotificacion
				,NumeroCelularNotificacion
				,AsientoElectronico
				,PartidaElectronica
				,Activo
				,IdUsuarioCreacionAuditoria
				)
				VALUES 
				(
									@pIdPersona
				,@pIdUsuario
				,@pEmailNotificacion
				,@pNumeroCelularNotificacion
				,@pAsientoElectronico
				,@pPartidaElectronica
				,@pActivo
				,@pIdUsuarioAuditoria
				)
					set @pIdAdministrado=SCOPE_IDENTITY()  
					SET @DescripcionMensaje ='SE INSERTÓ CORRECTAMENTE EL REGISTRO'
					SET @CodigoMensaje=0
				END
				ELSE
				BEGIN


					UPDATE  Casilla.Administrado SET 
								
				    EmailNotificacion=@pEmailNotificacion
				    ,NumeroCelularNotificacion=@pNumeroCelularNotificacion
				    ,CodigoTelefonoConfirmacion=@pCodigoTelefonoConfirmacion
					,CodigoCorreoConfirmacion =@pCodigoCorreoConfirmacion 

				    ,IdUsuarioActualizacionAuditoria=@pIdUsuarioAuditoria,FechaActualizacionAuditoria=getdate()
				WHERE IdAdministrado=@pIdAdministrado
					set @pIdAdministrado=@pIdAdministrado 
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Casilla.paGuardarAdministrado',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD, SUCEDIÓ UN ERROR EN LA BASE DE DATOS ['+@ERROR_MESSAGE+']'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
			END CATCH

