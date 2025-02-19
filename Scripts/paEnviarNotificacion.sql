USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Casilla].[paEnviarNotificacion]    Script Date: 20/06/2024 03:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
		ALTER PROCEDURE [Casilla].[paEnviarNotificacion]
			@pIdNotificacion int = NULL
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(200) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS
			
			BEGIN TRY
			--VALIDACION no volever a ENVIAR LA MISMA NOTIFICACION

			 IF (SELECT COUNT(*) FROM Casilla.Notificacion WHERE NotificacionEnviada=1 AND IdNotificacion=@pIdNotificacion)>0
			 BEGIN
				SET @DescripcionMensaje ='NO PUEDE VOLVER A ENVIAR ESTA NOTIFICACION YA QUE YA FUE ENVIADA A SU DESTINAARIO'
				SET @CodigoMensaje=1
				RETURN
			 END
			BEGIN TRANSACTION
			SET NOCOUNT ON; 
				UPDATE  Casilla.Notificacion
				SET NotificacionEnviada=1,
				IdUsuarioNotificador=@pIdUsuarioAuditoria,
				FechaHoraNotificacionEnviada=getdate() 
				WHERE IdNotificacion=@pIdNotificacion
				SET @DescripcionMensaje ='SE ENVIÓ CORRECTAMENTE LA NOTIFICACIÓN'
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Casilla.paEnviarNotificacion',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD SUCEDIÓ UN ERROR EN LA BASE DE DATOS'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE ,@pIdUsuarioAuditoria                
			END CATCH
