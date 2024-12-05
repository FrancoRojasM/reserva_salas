CREATE TABLE [Casilla].[Verificacion](
	[IdVerificacion] [int] IDENTITY(1,1) NOT NULL,
	[Correo] varchar(50),
	[CorreoVerificacion] varchar(50),
	[Telefono] varchar(50),
	[TelefonoVerificacion] varchar(50),
	Verificado int,
	[IdUsuarioCreacionAuditoria] int NULL,
	[FechaCreacionAuditoria] datetime NULL,
	[IdUsuarioActualizacionAuditoria] int NULL,
	[FechaActualizacionAuditoria] datetime NULL,
	[EstadoAuditoria] int 
	)
	GO

	CREATE PROCEDURE [Casilla].[paGenerarCodigoCorreoConfirmacionLogin]
			 @pIdVerificacion int output
			,@pCorreo varchar(50)
			,@pCorreoVerificacion varchar(50)
			,@pTelefono varchar(50)
			,@pTelefonoVerificacion varchar(50)
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(4000) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS

			SET NOCOUNT ON; 
			BEGIN TRY
				DECLARE @vCantidad int=0
				DECLARE @vSubMensaje varchar(100)=''
			BEGIN TRANSACTION	

			IF @pIdVerificacion=0
			BEGIN
				INSERT INTO Casilla.Verificacion
				( 
				Correo,
				CorreoVerificacion,
				Telefono,
				TelefonoVerificacion,
				IdUsuarioCreacionAuditoria,
				FechaCreacionAuditoria,
				EstadoAuditoria
				)
				VALUES 
				(
				@pCorreo,
				@pCorreoVerificacion,
				@pTelefono,
				@pTelefonoVerificacion,
				@pIdUsuarioAuditoria,
				getdate(),
				1
				)
					set @pIdVerificacion=SCOPE_IDENTITY()  
					SET @DescripcionMensaje ='SE INSERTÓ CORRECTAMENTE EL REGISTRO'
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='[Casilla].[paGuardarValidacionCasilla]',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD, SUCEDIÓ UN ERROR EN LA BASE DE DATOS ['+@ERROR_MESSAGE+']'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
			END CATCH
	GO

		CREATE PROCEDURE [Casilla].[paGenerarCodigoCorreoValidacionLogin]
			 @pIdVerificacion int output
			,@pCorreo varchar(50)
			,@pCorreoVerificacion varchar(50)
			,@pTelefono varchar(50)
			,@pTelefonoVerificacion varchar(50)
			,@pIdUsuarioAuditoria int = NULL
			,@DescripcionMensaje varchar(4000) OUTPUT
			,@CodigoMensaje int OUTPUT		
			AS

			SET NOCOUNT ON; 
			BEGIN TRY
				DECLARE @vCantidad int=0
				DECLARE @vSubMensaje varchar(100)=''
				IF @pIdVerificacion=0
				BEGIN
					SELECT @vCantidad=1
					SET @vSubMensaje='DEBE ENVIAR CODIGOS DE VERIFICACION A SU CORREO Y TELEFONO MOVIL'
				END
				
				IF @vCantidad>0 
				BEGIN					
					SET @DescripcionMensaje =@vSubMensaje
					SET @CodigoMensaje=1
					RETURN
				END



			BEGIN TRANSACTION	

			IF @pIdVerificacion<>0
			BEGIN

			DECLARE @vCorreoValidacion varchar(50)
			DECLARE @vCodigoCorreoValidacion varchar(50)
			DECLARE @vTelefonoValidacion varchar(50)
			DECLARE @vCodigoTelefonoValidacion varchar(50)

			SELECT 
			@vCorreoValidacion=Correo
			,@vCodigoCorreoValidacion=CorreoVerificacion
			,@vTelefonoValidacion= Telefono
			,@vCodigoTelefonoValidacion=TelefonoVerificacion
			FROM Casilla.Verificacion WHERE IdVerificacion=@pIdVerificacion

			IF (@vCorreoValidacion=@pCorreo AND @vCodigoCorreoValidacion=@pCorreoVerificacion AND @vTelefonoValidacion=@pTelefono AND @vCodigoTelefonoValidacion=@pTelefonoVerificacion )
			BEGIN 

				UPDATE Casilla.Verificacion SET Verificado=1 WHERE IdVerificacion=@pIdVerificacion

			END
			
			
				set @pIdVerificacion=SCOPE_IDENTITY()  
				SET @DescripcionMensaje ='SE INSERTÓ CORRECTAMENTE EL REGISTRO'
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='[Casilla].[paGuardarValidacionCasilla]',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR NO SE PUDO PROCESAR LA SOLICITUD, SUCEDIÓ UN ERROR EN LA BASE DE DATOS ['+@ERROR_MESSAGE+']'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnTablaLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE,@pIdUsuarioAuditoria                 
			END CATCH