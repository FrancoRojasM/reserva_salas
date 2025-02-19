USE [BDCASILLA]
GO
/****** Object:  StoredProcedure [Seguridad].[paObtenerDatosUsuarioAutenticadoLdap]    Script Date: 18/06/2024 05:08:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [Seguridad].[paObtenerDatosUsuarioAutenticadoLdap]
			@pLogueo VARCHAR(25),
			@pIp varchar(200)	
		AS

		BEGIN TRY	
			DECLARE  @pIdUsuario INT=0
			SELECT top 1 @pIdUsuario=U.IdUsuario	FROM Seguridad.Usuario U LEFT JOIN General.Persona P ON P.IdPersona= U.IdPersona WHERE U.Logueo=@pLogueo and U.EstadoAuditoria=1

			--IF COALESCE(@pIdUsuario,0)<>0
			--BEGIN
			--	EXEC Seguridad.paguardarUsuariosConectadosAlSistema @pIdUsuario ,@pIp,'',''
			--END

			declare @NumeroContrasena int

			--select @NumeroContrasena=count(*) FROM Seguridad.UsuarioDetalle where IdUsuario=@pIdUsuario and EstadoAuditoria=1
					   
			SELECT  TOP 1
			U.IdCatalogoTipoUsuario,
			U.Bloqueado,
			coalesce(U.IdPersona,0)IdPersona,
			U.IdUsuario,
			U.Verificado,
			U.Logueo,
			coalesce(P.NombreCompleto,'')NombreCompleto,
			coalesce(P.NumeroDocumento,'')NumeroDocumento,			
			case when COALESCE(U.RutaFoto,'') ='' then CASE WHEN COALESCE(P.Sexo,0)=0 then 'sinfotoH.jpg' else 'sinfotoM.jpg' end else U.RutaFoto end RutaFoto,			
			case when COALESCE(U.RutaFoto,'') ='' then CASE WHEN COALESCE(P.Sexo,0)=0 then 'sinfotoH.jpg' else 'sinfotoM.jpg' end else U.RutaFoto end RutaArchivoFoto,		
			coalesce(p.Sexo,0)Sexo,
		--	coalesce(P.NumeroRuc,'')NumeroRuc	,
		--	@NumeroContrasena CambioContrasena	,
			coalesce(U.EsInstitucion,0)	EsInstitucion,
			coalesce(P.NumeroCelular,'')NumeroCelular,
			coalesce(P.TelefonoFijo,'')TelefonoFijo,
			coalesce(P.Direccion,'')Direccion,
			coalesce(u.Email,'')Email,
			coalesce(P.IdCatalogoTipoDocumentoPersonal,0)IdCatalogoTipoDocumentoPersonal,
			A.IdAdministrado
			FROM Seguridad.Usuario U 
			INNER JOIN [Casilla].[Administrado] A ON A.IdPersona=U.IdPersona AND A.IdUsuario=U.IdUsuario
			LEFT JOIN General.Persona P ON P.IdPersona= U.IdPersona
			where U.Logueo IN(@pLogueo,@pLogueo+'@pronis.gob.pe') and U.EstadoAuditoria=1



		END TRY
		BEGIN CATCH
			DECLARE @ERROR_NUMBER INT, @ERROR_SEVERITY INT,@ERROR_STATE INT,@ERROR_LINE INT,@ERROR_PROCEDURE VARCHAR(MAX)	,@ERROR_MESSAGE VARCHAR(MAX)	
			SELECT @ERROR_NUMBER=ERROR_NUMBER() , @ERROR_SEVERITY=ERROR_SEVERITY() , @ERROR_STATE=ERROR_STATE() , @ERROR_PROCEDURE=ERROR_PROCEDURE(),@ERROR_LINE=ERROR_LINE(),@ERROR_MESSAGE=ERROR_MESSAGE()	
			EXEC Seguridad.paGuardarErroresEnLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE ,  @ERROR_PROCEDURE,@ERROR_LINE,@ERROR_MESSAGE                
		END CATCH
