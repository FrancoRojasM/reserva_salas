USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Casilla].[paEliminarAdministrado]    Script Date: 20/06/2024 03:26:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
		ALTER PROCEDURE [Casilla].[paGenerarCodigoCorreoConfirmacion]
			@pIdAdministrado int = NULL
			,@pClaveConfirmnacion varchar(100) = NULL
			,@pEmailNotificacionValidacion varchar(100)
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(200) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			
			BEGIN TRY
			BEGIN TRANSACTION
			SET NOCOUNT ON; 
				UPDATE  Casilla.Administrado SET CodigoCorreoValidacion=@pClaveConfirmnacion,EmailNotificacionValidacion=@pEmailNotificacionValidacion,CodigoCorreoConfirmacion=NULL WHERE IdAdministrado=@pIdAdministrado
				SET @DescripcionMensaje ='SE CREO Y ENVIO CORRECTAMENTE EL CODIGO DE VERIFICACION'
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Casilla.paGenerarCodigoCorreoConfirmacion',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD SUCEDIÓ UN ERROR EN LA BASE DE DATOS'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
			END CATCH
			GO
			ALTER PROCEDURE [Casilla].[paGenerarCodigoTelefonoConfirmacion]
			@pIdAdministrado int = NULL
			,@pClaveConfirmnacion varchar(100) = NULL
			,@pNumeroCelularValidacion varchar(100)
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(200) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			
			BEGIN TRY
			BEGIN TRANSACTION
			SET NOCOUNT ON; 
				UPDATE  Casilla.Administrado SET CodigoTelefonoValidacion=@pClaveConfirmnacion,NumeroCelularValidacion=@pNumeroCelularValidacion,CodigoTelefonoConfirmacion=NULL WHERE IdAdministrado=@pIdAdministrado
				SET @DescripcionMensaje ='SE CREO Y ENVIO CORRECTAMENTE EL CODIGO DE VERIFICACION'
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Casilla.paGenerarCodigoTelefonoConfirmacion',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD SUCEDIÓ UN ERROR EN LA BASE DE DATOS'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
			END CATCH
