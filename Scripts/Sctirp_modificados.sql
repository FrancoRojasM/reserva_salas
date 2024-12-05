ALTER PROCEDURE [Seguridad].[paGuardarNuevaCuentaCasilla]
			@pIdUsuario INT OUTPUT,
			@pCatalogoTipoDocumento int,
			@pNumeroDocumento varchar(12),
			@pNombres varchar(50),
			@pApellidoPaterno varchar(50),
			@pApellidoMaterno varchar(50),
			@pPais int,
			@pEmail varchar(100),
			@pNombreUsuario varchar(100),
			@pFechaNacimiento varchar(10),
			@pSexo int,
			@pLugarNacimiento varchar(100),
			@pNumeroCelular varchar(9),
			@pPassword varchar(30),
			@pIdUsuarioAuditoria int = NULL,
			@DescripcionMensaje varchar(4000) OUTPUT,
			@CodigoMensaje int OUTPUT		
			AS
			
			BEGIN TRY
			
			DECLARE @vCantidad int=0
			DECLARE @vEspacios varchar (500)
			SET @pNombreUsuario=@pEmail

			IF COALESCE(@pEmail,'')=''
			BEGIN
				SET @DescripcionMensaje ='NO HA INGRESADO CORECTAMENTE EL EMAIL'
				SET @CodigoMensaje=1
				RETURN
			END
			IF COALESCE(@pCatalogoTipoDocumento,0)=0
			BEGIN
				SET @DescripcionMensaje ='NO HA SELECCIONADO CORRECTAMENTE EL TIPO DE DOCUMENTO'
				SET @CodigoMensaje=1
				RETURN
			END
			IF COALESCE(@pCatalogoTipoDocumento,0)=0
			BEGIN
				SET @DescripcionMensaje ='NO HA SELECCIONADO CORRECTAMENTE EL TIPO DE DOCUMENTO'
				SET @CodigoMensaje=1
				RETURN
			END
			IF COALESCE(@pNumeroDocumento,'')=''
			BEGIN
				SET @DescripcionMensaje ='NO HA ESCRITO CORRECTAMENTE EL NUMERO DE DOCUMENTO'
				SET @CodigoMensaje=1
				RETURN
			END
			IF COALESCE(@pCatalogoTipoDocumento,0)<>14
			BEGIN
				
				IF COALESCE(@pNombres,'')=''
				BEGIN
					SET @DescripcionMensaje ='NO HA ESCRITO CORRECTAMENTE LOS NOMBRES Y/O APELLIDOS'
					SET @CodigoMensaje=1
					RETURN
				END
			END
			IF COALESCE(@pCatalogoTipoDocumento,0)<>14
			BEGIN
				
				IF COALESCE(@pApellidoPaterno,'')='' OR COALESCE(@pApellidoMaterno,'')='' 
				BEGIN
					SET @DescripcionMensaje ='NO HA ESCRITO CORRECTAMENTE LOS APELLIDOS'
					SET @CodigoMensaje=1
					RETURN
				END
			END

			--IF(SELECT count(*) FROM General.Persona p
			--INNER JOIN Seguridad.Usuario U ON U.IdPersona=p.IdPersona
			--WHERE P.IdCatalogoTipoDocumentoPersonal=@pCatalogoTipoDocumento
			--AND P.NumeroDocumento=CASE WHEN @pCatalogoTipoDocumento=2 
			--					THEN RIGHT('00000000'+@pNumeroDocumento,8) 
			--					ELSE 
			--						CASE WHEN @pCatalogoTipoDocumento=14 
			--						THEN RIGHT('00000000000'+@pNumeroDocumento,11) 
			--						ELSE RIGHT('00000000000'+@pNumeroDocumento,12)  
			--						END 
			--					END 
			--AND P.EstadoAuditoria=1 AND U.EstadoAuditoria=1 AND U.EsInstitucion=0)>0
			--BEGIN
			--	SET @DescripcionMensaje ='YA EXISTE UN USUARIO REGISTRADO CON EL TIPO Y NUMERO DE DOCUMENTO SELECCIONADO'
			--	SET @CodigoMensaje=1
			--	RETURN
			--END

			
			SET NOCOUNT ON; 
			--VERIFICAR LA EXISTENCIA DE LA PERSONA, SI EXISTE ENTONCES SOLO ACTUALIZAR LOS DATOS DE LO CONTRARIO CREAR UNO NUEVO
			DECLARE @cantidad int=0
			DECLARE @IdPersona int=0
--			select * from General.Catalogo
			BEGIN TRANSACTION
				SELECT @cantidad=count(*) FROM General.Persona
				WHERE IdCatalogoTipoDocumentoPersonal=@pCatalogoTipoDocumento
				AND NumeroDocumento=CASE WHEN @pCatalogoTipoDocumento=2 
									THEN RIGHT('00000000'+@pNumeroDocumento,8) 
									ELSE 
										CASE WHEN @pCatalogoTipoDocumento=14 
										THEN RIGHT('00000000000'+@pNumeroDocumento,11) 
										ELSE RIGHT('00000000000'+@pNumeroDocumento,12)  
										END 
									END 
				AND EstadoAuditoria=1
				if @cantidad=0			
				BEGIN			
					INSERT INTO General.Persona (
						   NombreCompleto
						  ,Nombres
						  ,ApellidoPaterno
						  ,ApellidoMaterno		  			 
						  ,FechaNacimiento
						  ,IdPaisNacimiento
						  ,LugarNacimiento
						  ,Sexo
						  ,IdUbigeo
						  ,Longitud
						  ,Latitud
						  ,IdCatalogoTipoPersona
						  ,Direccion
						  ,NumeroCelular
						  ,TelefonoFijo
						  ,IdCatalogoTipoDocumentoPersonal
						  ,NumeroDocumento
					)
					VALUES(						 
						  CASE WHEN @pCatalogoTipoDocumento=14 THEN @pNombres ELSE @pNombres +' '+@pApellidoPaterno + ' '+@pApellidoMaterno END
						  ,@pNombres
						  ,@pApellidoPaterno
						  ,@pApellidoMaterno		  			 
						  ,''
						  ,145
						  ,''
						  ,0
						  ,0
						  ,''
						  ,''
						  ,CASE WHEN @pCatalogoTipoDocumento=14 THEN 10 ELSE 9 END
						  ,''
						  ,''
						  ,''
						  ,@pCatalogoTipoDocumento
						  ,CASE WHEN @pCatalogoTipoDocumento=2 THEN RIGHT('00000000'+@pNumeroDocumento,8) ELSE CASE WHEN @pCatalogoTipoDocumento=14 THEN RIGHT('00000000000'+@pNumeroDocumento,11) ELSE RIGHT('00000000000'+@pNumeroDocumento,12) END END
					)
					SET @IdPersona=SCOPE_IDENTITY()
				END
				ELSE
				BEGIN
					SELECT @IdPersona=IdPersona FROM General.Persona WHERE IdCatalogoTipoDocumentoPersonal=@pCatalogoTipoDocumento AND NumeroDocumento=CASE WHEN @pCatalogoTipoDocumento=2 THEN RIGHT('00000000'+@pNumeroDocumento,8) ELSE CASE WHEN @pCatalogoTipoDocumento=3 THEN RIGHT('00000000000'+@pNumeroDocumento,12) ELSE @pNumeroDocumento END END AND EstadoAuditoria=1
				END
				INSERT INTO  Seguridad.Usuario
				( 
				 IdPersona
				,IdCatalogoTipoUsuario
				,Logueo
				,Clave
				,Bloqueado
				,Email			
				,Verificado
				,IdUsuarioCreacionAuditoria
				,EsInstitucion
				)
				VALUES 
				(
				 @IdPersona
				,4
				,@pNombreUsuario
				,@pPassword
				,0
				,@pEmail
				,1
				,1
				,0
				)
				SET @pIdUsuario=SCOPE_IDENTITY()  
				--CREAR EL ADMINISTRADO
				INSERT INTO Casilla.Administrado(IdPersona, IdUsuario,EmailNotificacion, NumeroCelularNotificacion)
				VALUES(@IdPersona, @pIdUsuario, @pEmail, @pNumeroCelular)
				--SE ASUME QUE TODO AQUEL QUE LLEGUE ACA ES UN USUARIO NUEVO EN TAL SENTIDO SE LE DEBE ASIGNAR EL PERFIL DE SEGURIDAD DE USUARIO Y PERFIL DE MIS NOTIFICACIONES
				INSERT INTO Seguridad.UsuarioPerfil(IdUsuario, IdPerfil,IdUsuarioCreacionAuditoria,FechaCreacionAuditoria)
				VALUES(@pIdUsuario,11,1,GETDATE())--11-PERFIL CAMBIAR CONTRASEÑA
			
				INSERT INTO Seguridad.UsuarioPerfil(IdUsuario, IdPerfil,IdUsuarioCreacionAuditoria,FechaCreacionAuditoria)
				VALUES(@pIdUsuario,14,1,GETDATE())

				INSERT INTO Seguridad.UsuarioPerfil(IdUsuario, IdPerfil,IdUsuarioCreacionAuditoria,FechaCreacionAuditoria)
				VALUES(@pIdUsuario,18,1,GETDATE())
			
				SET @DescripcionMensaje ='FELICIDADES!!!, SE CREÓ CORRECTAMENTE LA CUENTA Y SE ENVIÓ UN EMAIL A SU BANDEJA DE ENTRADA, TAMBIEN REVISE SU CARPETA DE SPAM'
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
				SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE='Seguridad.paGuardarUsuario',@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
				SET @DescripcionMensaje ='ERROR: NO SE PUDO CREAR LA CUENTA'
				SET @CodigoMensaje=@ERROR_NUMBER
				EXEC Seguridad.paGuardarErroresEnLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  'Seguridad.paGuardarUsuario',@ERROR_LINE,@ERROR_MESSAGE                
			END CATCH
